using Ar.Common;
using Ar.IServices;
using Ar.Repository;
using AR.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Services
{
    /// <summary>
    /// 钱包信息
    /// </summary>
    public class UseWalletService : IUseWalletService
    {
        //获取充值页面数据
        public RechargePage GetRechargePage(string userCode)
        {
            RechargePage rechargePage = new RechargePage();
            IList<UseWallet> list = GetUseWallet(userCode);
            var totalMoney = list.Sum(p => p.DonationAmount + p.AccountPrincipal);
            var pechargeMoney = list.Sum(p => p.AccountPrincipal);
            var donationAmount = list.Sum(p => p.DonationAmount);
            rechargePage.TotalMoney = totalMoney;
            rechargePage.PechargeMoney = pechargeMoney;
            rechargePage.DonationAmount = donationAmount;
            IRechargeTypeService _service = new RechargeTypeService();
            rechargePage.RechargeTypeList = _service.GetRechargeTypeList();
            return rechargePage;
        }

       public  IList<UseWallet> GetUseWalletList()
        {
            IList<UseWallet> list = DapperSqlHelper.FindToList<UseWallet>("select * from [dbo].[UseWallet] where  Status=1", null, false);
            return list;
        }
        public IList<UseWallet> GetUseWallet(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            IList<UseWallet> list = DapperSqlHelper.FindToList<UseWallet>("select * from [dbo].[UseWallet] where UserCode=@userCode and Status=1 order by Sort", paras, false);
            foreach(var w in list)
            {
                w.TotalAmount = w.AccountPrincipal + w.DonationAmount;
            }
            return list;
        }

        /// <summary>
        /// 判断钱包的钱是否够
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public bool ExistMoney(string userCode, decimal money)
        {
            bool result = false;
            IList<UseWallet> list = GetUseWallet(userCode);
            decimal tempTotal = 0;
            foreach (var w in list)
            {
                LogHelper.WriteLog("w.AccountPrincipal " +w.AccountPrincipal);
                if (w.AccountPrincipal > 0)
                {
                    var ratio = decimal.Parse(w.Ratio);
                    tempTotal = tempTotal+ decimal.Parse((w.AccountPrincipal + w.DonationAmount).ToString());
                    LogHelper.WriteLog("w.tempTotal " + tempTotal);
                }
            }
            LogHelper.WriteLog("w.tempTotal " + tempTotal);
            if (tempTotal >= money)
            {
                result = true;
            }
            return result;

        }
        public bool UpdateData(string userCode, decimal money)
        {
            IRecordsOfConsumptionService _recordsOfConsumption = new RecordsOfConsumptionService();
            decimal total = 0;
            decimal? kouchumoney = 0m;
            IList<UseWallet> list = GetUseWallet(userCode);
            list= list.OrderBy(t => t.Sort).ToList();
            decimal? recordsdonationAmount = 0m;
            decimal? recordsaccountPrincipal = 0m;
            foreach (var w in list)
            {
                if (w.AccountPrincipal > 0)
                {
                    var ratio = decimal.Parse(w.Ratio);
                    var tempTotal = decimal.Parse((w.AccountPrincipal + w.DonationAmount).ToString());
                    total = total + tempTotal;
                   var tempmoney = money - total;
                    decimal? donationAmount = 0;
                    decimal? accountPrincipal = 0;
                    if (tempmoney >= 0)
                    {
                      
                        donationAmount = 0;
                        accountPrincipal = 0;
                        kouchumoney = w.AccountPrincipal + w.DonationAmount;
                    }
                    else
                    {
                        donationAmount = w.DonationAmount-((money- kouchumoney) -((money - kouchumoney) * (1 - ratio)));
                        accountPrincipal = w.AccountPrincipal -(money- kouchumoney) * (1 - ratio) ;
                        recordsdonationAmount= (money  - (money* (1 - ratio)));
                        recordsaccountPrincipal = money* (1 - ratio);
                    }
                    DynamicParameters paras = new DynamicParameters();
                    paras.Add("@WalletCode", w.WalletCode, System.Data.DbType.String);
                    paras.Add("@DonationAmount", donationAmount, System.Data.DbType.Decimal);
                    paras.Add("@AccountPrincipal", accountPrincipal, System.Data.DbType.Decimal);
                    string sql = "update [dbo].[UseWallet] set AccountPrincipal=@AccountPrincipal,DonationAmount=@DonationAmount where WalletCode=@WalletCode";
                    int i = DapperSqlHelper.ExcuteNonQuery<UseWallet>(sql, paras, false);
                }
                if (total >= money)
                {
                    _recordsOfConsumption.InsertRecore("0", userCode, money, "", recordsdonationAmount, recordsaccountPrincipal, false);
                    return true;
                }
            }
          
            return true;
        }

        public bool InsertUseWallet(UseWallet wallet)
        {
            string sql = "";
            DynamicParameters parasuser = new DynamicParameters();
            parasuser.Add("@userCode", wallet.UserCode, System.Data.DbType.String);
            var tempWallet = DapperSqlHelper.FindOne<UseWallet>("SELECT MAX(WalletCode) WalletCode,MAX(Sort) Sort FROM [dbo].[UseWallet]where UserCode=@userCode ", parasuser, false);
                if (tempWallet != null)
                {
                //wallet.WalletCode = Guid.NewGuid().ToString();
                if (tempWallet.Sort == null)
                {
                    wallet.Sort = 1;
                }
                else
                {
                    wallet.Sort = tempWallet.Sort + 1;
                }
                }
                else
                {
                    wallet.WalletCode = "1";
                    wallet.Sort = 1;
                }

                sql = @"INSERT INTO [dbo].[UseWallet]([WalletCode],[UserCode] ,[AccountPrincipal]  ,[DonationAmount] ,
            [Ratio]  ,[Status] ,[Sort]) 
            VALUES  ( @WalletCode, 
                      @UserCode, 
                      @AccountPrincipal, 
                      @DonationAmount, 
                      @Ratio, 
                      1, 
                      @Sort)";

          
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@WalletCode", wallet.WalletCode, System.Data.DbType.String);
            paras.Add("@UserCode", wallet.UserCode, System.Data.DbType.String);
            paras.Add("@AccountPrincipal", wallet.AccountPrincipal, System.Data.DbType.String);
            paras.Add("@DonationAmount", wallet.DonationAmount, System.Data.DbType.String);
            paras.Add("@Ratio", wallet.Ratio, System.Data.DbType.String);
            paras.Add("@Sort", wallet.Sort, System.Data.DbType.Int32);
            int i = DapperSqlHelper.ExcuteNonQuery<UseWallet>(sql, paras, false);
            return true;
        }

        public object GetUseWalletInfoByUserCode(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            decimal? totalAmount = 0;
            decimal? accountPrincipal = 0;
            decimal? donationAmount = 0;
            IList<UseWallet> list = DapperSqlHelper.FindToList<UseWallet>("select * from [dbo].[UseWallet] where UserCode=@userCode and Status=1 order by Sort", paras, false);
            foreach (var w in list)
            {
                accountPrincipal = w.AccountPrincipal + accountPrincipal;
                donationAmount = donationAmount + w.DonationAmount;
                w.TotalAmount = w.AccountPrincipal + w.DonationAmount;
                totalAmount = totalAmount + w.TotalAmount;
            }
            return new { accountPrincipal = accountPrincipal, donationAmount = donationAmount, totalAmount = totalAmount };
        }


        public UseWallet GetUseWalletCountMoney(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            decimal? totalAmount = 0;
            decimal? accountPrincipal = 0;
            decimal? donationAmount = 0;
            string Ratio = "1";
            IList<UseWallet> list = DapperSqlHelper.FindToList<UseWallet>("select * from [dbo].[UseWallet] where UserCode=@userCode and Status=1 order by Sort", paras, false);
            foreach (var w in list)
            {
                accountPrincipal = w.AccountPrincipal + accountPrincipal;
                donationAmount = donationAmount + w.DonationAmount;
                w.TotalAmount = w.AccountPrincipal + w.DonationAmount;
                totalAmount = totalAmount + w.TotalAmount;
                
            }
            decimal? totalAmounttemp = 0;
            foreach (var x in list)
            {

                totalAmounttemp = x.AccountPrincipal + x.DonationAmount;
                if (totalAmounttemp != 0)
                {
                    Ratio = x.Ratio;
                    break;
                }
            }

            return new UseWallet(){ AccountPrincipal = accountPrincipal,
                DonationAmount = donationAmount,
                TotalAmount = totalAmount,
                Ratio= Ratio
            };
        }
    }
}
