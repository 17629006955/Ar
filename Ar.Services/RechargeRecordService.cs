using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;
using System.Transactions;
using Ar.Common;

namespace Ar.Services
{
    public class RechargeRecordService : IRechargeRecordService
    {
        public RechargeRecord GetRechargeRecordByCode(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@rechargeRecord", code, System.Data.DbType.String);
            RechargeRecord rechargeRecord = DapperSqlHelper.FindOne<RechargeRecord>("select * from [dbo].[RechargeRecord] where RechargeRecordCode=@rechargeRecord", paras, false);
            return rechargeRecord;
        }

        public IList<RechargeRecord> GetRechargeRecordList()
        {
            DynamicParameters paras = new DynamicParameters();
            IList<RechargeRecord> list = DapperSqlHelper.FindToList<RechargeRecord>(@"select * from [dbo].[RechargeRecord]", null, false);
            return list;
        }
        public IList<RechargeTypeshow> GetRechargeRecordListByUserCode(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            IList<RechargeTypeshow> list = DapperSqlHelper.FindToList<RechargeTypeshow>(@"  SELECT   
                       CASE WHEN a.RechargeTypeCode='0' THEN '0' ELSE b.RechargeTypeCode END RechargeTypeCode,
                      CASE WHEN a.RechargeTypeCode='0' THEN '充值' ELSE  b.RechargeTypeName END RechargeTypeName,
                      CASE WHEN a.RechargeTypeCode='0' THEN a.RecordsMoney ELSE b.Money END Money,
                       CASE WHEN a.RechargeTypeCode='0' THEN 0 ELSE b.DonationAmount END DonationAmount
                        ,a.CreateTime
                                from [dbo].[RecordsOfConsumption] a  LEFT JOIN [dbo].[RechargeType] b 
			                    ON a.RechargeTypeCode=b.RechargeTypeCode
                                AND  b.Status=1 WHERE  a.IsRecharging=1
            AND a.UserCode=@userCode", paras, false);
            return list;
        }

        public bool Recharge(string typeCode,string userCode, decimal? money = 0,string storeCode="")
        {
            IRechargeTypeService s = new RechargeTypeService();
            IRecordsOfConsumptionService cs = new RecordsOfConsumptionService();
            IUseWalletService us = new UseWalletService();
            IFinancialStatementsService _financialStatementsService = new FinancialStatementService();
            var type=s.GetRechargeTypeByCode(typeCode);
            var explain = "";
            decimal? donationAmount = 0;
            decimal? ratio = 0;
            if (money > 0 && type==null)
            {
                typeCode = "0";
                ratio = 0;
                explain = "充值类型:任意金额,本金：" + money;
            }
            else
            {
                donationAmount = type.DonationAmount;
                money = type.Money;
                ratio = donationAmount/ (money + donationAmount);
                explain = "充值类型" + type.RechargeTypeName + ",本金：" + money + ",赠送：+" + donationAmount;
            }
            UseWallet wallet = new UseWallet()
            {
                WalletCode = Guid.NewGuid().ToString(),
                UserCode = userCode,
                AccountPrincipal = money,
                DonationAmount = donationAmount,
                Ratio = decimal.Round(decimal.Parse(ratio.ToString()), 4).ToString(),
                Status = true,
                Sort = 1,
                IsMissionGiveaway = false
            };
           
            using (var scope2 = new TransactionScope())//创建事务
            {
                var fs = _financialStatementsService.getDataRechargeRecord(userCode, typeCode, wallet, storeCode, ratio,"微信");
                LogHelper.WriteLog("报表表数据更新完成");
                _financialStatementsService.Insert(fs);
                //钱包
                us.InsertUseWallet(wallet);
                //消费记录
                cs.InsertRecore(typeCode, userCode, decimal.Parse(money.ToString()), explain,null, null);
                scope2.Complete();
            }
            //充值
            //InsertRechargeRecord(record);
            return true;
        }
        public bool InsertRechargeRecord(RechargeRecord record)
        {
            if (string.IsNullOrEmpty(record.RechargeRecordCode))
            {
                record.RechargeRecordCode = GetMaxCode();
            }
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@RechargeRecordCode", record.RechargeRecordCode, System.Data.DbType.String);
            paras.Add("@UserCode", record.UserCode, System.Data.DbType.String);
            paras.Add("@RechargeAmount", record.RechargeAmount, System.Data.DbType.String);
            paras.Add("@Explain", record.Explain, System.Data.DbType.String);
            paras.Add("@CreateTime", record.CreateTime, System.Data.DbType.String);
            User userone = DapperSqlHelper.FindOne<User>(@"INSERT INTO [dbo].[RechargeRecord]
           ([RechargeRecordCode],[UserCode] ,[RechargeAmount]  ,[Explain]) 
            VALUES  ( @RechargeRecordCode, 
                     @UserCode, 
                     @RechargeAmount, 
                     @Explain, 
                     getdate())", paras, false);
            return true;
        }

        public string GetMaxCode()
        {
            var record = DapperSqlHelper.FindOne<RechargeRecord>("SELECT MAX(RechargeRecordCode) RechargeRecordCode FROM [dbo].[RechargeRecord]", null, false);
            var code = record != null ? Convert.ToInt32(record.RechargeRecordCode) + 1 : 1;
            return code.ToString();
        }
    }
}
