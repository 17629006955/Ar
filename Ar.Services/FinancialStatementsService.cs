using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    public class FinancialStatementService : IFinancialStatementsService
    {
        public bool Insert(financialStatements model)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@Code", model.Code, System.Data.DbType.String);
            paras.Add("@CreateTime", model.CreateTime, System.Data.DbType.String);
            paras.Add("@UserPhone", model.UserPhone, System.Data.DbType.String);
            paras.Add("@UserCreateTime", model.UserCreateTime, System.Data.DbType.String);
            paras.Add("@StoreName", model.StoreName, System.Data.DbType.String);
            paras.Add("@ProductionType", model.ProductionType, System.Data.DbType.String);
            paras.Add("@Cstname", model.Cstname, System.Data.DbType.String);
            paras.Add("@ProductionCode", model.ProductionCode, System.Data.DbType.String);
            paras.Add("@ProductionName", model.ProductionName, System.Data.DbType.String);
            paras.Add("@Iquantity", model.Iquantity, System.Data.DbType.String);
            paras.Add("@Itaxunitprice", model.Itaxunitprice, System.Data.DbType.String);
            paras.Add("@Isum", model.Isum, System.Data.DbType.String);
            paras.Add("@CpersonName", model.CpersonName, System.Data.DbType.String);
            paras.Add("@PayType", model.PayType, System.Data.DbType.String);
            paras.Add("@AmountOfIncome", model.AmountOfIncome, System.Data.DbType.String);
            paras.Add("@DonationAmount", model.DonationAmount, System.Data.DbType.String);
            paras.Add("@CouponUseCode", model.CouponUseCode, System.Data.DbType.String);
            paras.Add("@CouponUseMoney", model.CouponUseMoney, System.Data.DbType.String);
            paras.Add("@GetCouponTime", model.GetCouponTime, System.Data.DbType.String);
            paras.Add("@UseWalletMoney", model.UseWalletMoney, System.Data.DbType.String);
            paras.Add("@Ratio", model.Ratio, System.Data.DbType.String);
            paras.Add("@RecordsOfConsumptionCreateTime", model.RecordsOfConsumptionCreateTime, System.Data.DbType.String);
            paras.Add("@WriteOffUser", model.WriteOffUser, System.Data.DbType.String);
            paras.Add("@ProductionCode1", model.ProductionCode1, System.Data.DbType.String);
            paras.Add("@ProductionName1", model.ProductionName1, System.Data.DbType.String);
            paras.Add("@ExperiencePrice", model.ExperiencePrice, System.Data.DbType.String);
            paras.Add("@Iquantity1", model.Iquantity1, System.Data.DbType.String);
            paras.Add("@RecordsMoney", model.RecordsMoney, System.Data.DbType.String);
            paras.Add("@CouponUseMoney1", model.CouponUseMoney1, System.Data.DbType.String);
            paras.Add("@ActualConsumption", model.ActualConsumption, System.Data.DbType.String);
            paras.Add("@UseWalletMoney1", model.UseWalletMoney1, System.Data.DbType.String);
            paras.Add("@UseWalletAccountPrincipal", model.UseWalletAccountPrincipal, System.Data.DbType.String);
            paras.Add("@FinancialRevenueAccounting", model.FinancialRevenueAccounting, System.Data.DbType.String);
            paras.Add("@Imoney", model.Imoney, System.Data.DbType.String);
            paras.Add("@ProductInfoRate", model.ProductInfoRate, System.Data.DbType.String);
            paras.Add("@Itax", model.Itax, System.Data.DbType.String);
            paras.Add("@GrossProfit", model.GrossProfit, System.Data.DbType.String);
            string sql = (@"INSERT INTO dbo.financialStatements
           (Code,CreateTime ,UserPhone,UserCreateTime,StoreName ,ProductionType,Cstname ,ProductionCode,ProductionName
           ,Iquantity,Itaxunitprice,Isum,CpersonName,PayType,AmountOfIncome ,DonationAmount
           ,CouponUseCode,CouponUseMoney,GetCouponTime,UseWalletMoney,Ratio,RecordsOfConsumptionCreateTime
           ,WriteOffUser,ProductionCode1,ProductionName1,ExperiencePrice,Iquantity1
           ,RecordsMoney ,CouponUseMoney1 ,ActualConsumption ,UseWalletMoney1,UseWalletAccountPrincipal
           ,FinancialRevenueAccounting,Imoney ,ProductInfoRate ,Itax ,GrossProfit)
                values(@Code,@CreateTime ,@UserPhone,@UserCreateTime,@StoreName ,@ProductionType,@Cstname ,@ProductionCode,@ProductionName
           ,@Iquantity,@Itaxunitprice,@Isum,@CpersonName,@PayType,@AmountOfIncome ,@DonationAmount
           ,@CouponUseCode,@CouponUseMoney,@GetCouponTime,@UseWalletMoney,@Ratio,@RecordsOfConsumptionCreateTime
           ,@WriteOffUser,@ProductionCode1,@ProductionName1,@ExperiencePrice,@Iquantity1
           ,@RecordsMoney ,@CouponUseMoney1 ,@ActualConsumption ,@UseWalletMoney1,@UseWalletAccountPrincipal
           ,@FinancialRevenueAccounting,@Imoney ,@ProductInfoRate ,@Itax ,@GrossProfit)");
            DapperSqlHelper.ExcuteNonQuery<Coupon>(sql, paras, false);
            return true;
        }

        /// <summary>
        /// 购买获取报表的实体
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="order"></param>
        /// <param name="payType"></param>
        /// <returns></returns>
        public financialStatements getData(string userCode, Order order, string payType)
        {
            ICouponService _couponService = new CouponService();
            IProductInfoService _productInfoService = new ProductInfoService();
            ICouponTypeService _couponTypeService = new CouponTypeService();
            IUseWalletService _useWalletService = new UseWalletService();
            IStoreService _storeService = new StoreService();
            IUserInfo _userService = new UserInfo();
            var uw = _useWalletService.GetUseWalletCountMoney(userCode);
            var s = _storeService.GetStore(order.StoreCode);
            var p = _productInfoService.GetProductInfo(order.ProductCode);
            var u = _userService.GetUserByCode(userCode);
            financialStatements fs = new financialStatements();
            fs.Code = Guid.NewGuid().ToString();
            fs.CreateTime = order.CreateTime;
            fs.UserPhone = u?.Phone;
            fs.UserCreateTime = u?.CreateTime;
            fs.StoreName = s?.StoreName;
            fs.OrderNo = order.OrderNO;
            switch (p.Type)
            {
                case "1":
                    fs.ProductionType = "体验服务";break;
                case "2":
                    fs.ProductionType = "硬件产品"; break;
                case "3":
                    fs.ProductionType = "水吧服务"; break;
                case "4":
                    fs.ProductionType = "衍生品"; break;
                case "5":
                    fs.ProductionType = "配件"; break;

            }
            fs.Cstname = "普通销售";
            fs.ProductionCode = p.ProductCode;
            fs.ProductionName = p.ProductName;
            fs.Iquantity = order.Number;
            fs.Itaxunitprice = p.ExperiencePrice;
            if (payType.Equals("微信"))
            {
                fs.Isum = order.Money;
            }
            else
            {
                fs.Isum = p.ExperiencePrice * order.Number;
            }
            fs.CpersonName = p.CreatorName;
            fs.PayType = payType;
            fs.AmountOfIncome = order.Money;
            fs.DonationAmount = 0;
            fs.CouponUseCode = order.ExperienceVoucherCode;
            if (!string.IsNullOrEmpty(order.ExperienceVoucherCode))
            {
                var coupon = _couponService.GetCouponByCode(order.ExperienceVoucherCode);
                var couponMoney = _couponTypeService.GetCouponTypeByCode(coupon?.CouponTypeCode);
                if (couponMoney != null)
                {
                    fs.CouponUseMoney = couponMoney.Money;
                }
            }
            fs.GetCouponTime = order.CreateTime;
            if (payType.Equals("微信"))
            {
                fs.UseWalletMoney = order.Money;
                fs.Ratio = "100%";
            }
            else
            {
                fs.UseWalletMoney = uw.TotalAmount;
                fs.UseWalletMoney1 = fs.UseWalletMoney;
                fs.UseWalletAccountPrincipal = uw.AccountPrincipal;
                if (!string.IsNullOrEmpty(uw.Ratio))
                {
                    fs.Ratio = Math.Round(100 - Convert.ToDouble(uw.Ratio) * 100, 2).ToString() + '%';
                }
                fs.UseWalletMoney = uw.TotalAmount - order.Money;
                fs.UseWalletAccountPrincipal = uw.AccountPrincipal - order.Money * (1-Convert.ToDecimal(uw.Ratio));
                if (fs.UseWalletMoney != 0)
                {
                    
                    if (!string.IsNullOrEmpty(uw.Ratio))
                    {

                        fs.Ratio = Math.Round(100-Convert.ToDouble(uw.Ratio)*100,2).ToString() + '%';
                    }
                    else
                    {
                        fs.Ratio = "100%";
                    }
                }
                else
                {
                    fs.Ratio = "100%";
                }
                fs.UseWalletMoney1 = fs.UseWalletMoney;
            }

            fs.ProductInfoRate = p.Rate + "%";
            return fs;
        }
        


        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="order"></param>
        /// <param name="payType"></param>
        /// <returns></returns>
        public financialStatements getDataRechargeRecord(string userCode, string typeCode,UseWallet useWallet, string storeCode, decimal?  ratio ,string payType)
        {
            ICouponService _couponService = new CouponService();
            IRechargeTypeService rt = new RechargeTypeService();
            IProductInfoService _productInfoService = new ProductInfoService();
            ICouponTypeService _couponTypeService = new CouponTypeService();
            IUseWalletService _useWalletService = new UseWalletService();
            IUserStoreService _userStoreService = new UserStoreService();
            IStoreService _storeService = new StoreService();
            IUserInfo _userService = new UserInfo();
            var rechargetype = rt.GetRechargeTypeByCode(typeCode);
            var uw = _useWalletService.GetUseWalletCountMoney(userCode);
            if (string.IsNullOrEmpty(storeCode))
            {
                var us = _userStoreService.GetUserStorebyUserCode(userCode);
                storeCode = us.MembershipCardStore;
            }
            var s = _storeService.GetStore(storeCode);
            var u = _userService.GetUserByCode(userCode);
            financialStatements fs = new financialStatements();
            fs.Code = Guid.NewGuid().ToString();
            fs.CreateTime = DateTime.Now;
            fs.UserPhone = u?.Phone;
            fs.UserCreateTime = u?.CreateTime;
            fs.StoreName = s?.StoreName;
            fs.ProductionType = "体验服务";
            fs.Cstname = "普通销售";
            fs.ProductionCode = rechargetype?.RechargeTypeCode;
            fs.ProductionName = rechargetype?.RechargeTypeName;
            fs.Iquantity = 1;
            fs.Itaxunitprice = useWallet?.AccountPrincipal;
            fs.Isum = useWallet?.AccountPrincipal;
            fs.CpersonName = "业务员";
            fs.PayType = payType;
            fs.AmountOfIncome = useWallet?.AccountPrincipal;
            fs.DonationAmount = useWallet?.DonationAmount;
            fs.CouponUseCode = "";
            fs.CouponUseMoney = 0;
            fs.UseWalletMoney = uw?.TotalAmount+ useWallet?.AccountPrincipal+ useWallet?.DonationAmount;

            if (ratio!=null)
            {
                fs.Ratio = Math.Round(100-Convert.ToDouble(ratio) * 100, 2).ToString() + '%';
            }
            
            fs.UseWalletMoney1 = fs?.UseWalletMoney;
            fs.UseWalletAccountPrincipal = uw?.AccountPrincipal + useWallet?.AccountPrincipal;
           
            fs.ProductInfoRate = "0";
            return fs;
        }


        /// <summary>
        /// 核销
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="order"></param>
        /// <param name="payType"></param>
        /// <returns></returns>
        public financialStatements getWriteOff(string wfuserName,string userCode, string orderCode,string payType, decimal? RecordsAccountPrincipalMoney)
        {
            var dateTime = DateTime.Now;
            ICouponService _couponService = new CouponService();
            IProductInfoService _productInfoService = new ProductInfoService();
            ICouponTypeService _couponTypeService = new CouponTypeService();
            IUseWalletService _useWalletService = new UseWalletService();
            IOrderService _orderService = new OrderService();
            IStoreService _storeService = new StoreService();
            IUserInfo _userService = new UserInfo();
            var order=_orderService.GetOrderByCode(orderCode);
            var uw = _useWalletService.GetUseWalletCountMoneyWf(userCode);
            var s = _storeService.GetStore(order.StoreCode);
            var p = _productInfoService.GetProductInfo(order.ProductCode);
            var u = _userService.GetUserByCode(userCode);
            financialStatements fs = new financialStatements();
            fs.OrderNo = order?.OrderNO;
            fs.Code = Guid.NewGuid().ToString();
            fs.CreateTime = dateTime;
            fs.UserPhone = u?.Phone;
            fs.UserCreateTime = u?.CreateTime;
            fs.StoreName = s?.StoreName;
            switch (p.Type)
            {
                case "1":
                    fs.ProductionType = "体验服务"; break;
                case "2":
                    fs.ProductionType = "硬件产品"; break;
                case "3":
                    fs.ProductionType = "水吧服务"; break;
                case "4":
                    fs.ProductionType = "衍生品"; break;
                case "5":
                    fs.ProductionType = "配件"; break;

            }
            fs.Cstname = "普通销售";
            fs.ProductionCode = "";
            fs.ProductionName = "";
            fs.Iquantity = 0;
            fs.Itaxunitprice = 0;
            fs.Isum = 0;
            fs.CpersonName = p.CreatorName;
            fs.PayType = payType;
            fs.AmountOfIncome = 0;
            fs.DonationAmount = 0;
            fs.CouponUseCode = "";
            fs.CouponUseMoney = 0;
            fs.GetCouponTime = null;
            if (payType.Equals("微信"))
            {
                fs.UseWalletMoney = order.Money;
                fs.Ratio = "100%";
            }
            else
            {
                fs.UseWalletMoney = uw.TotalAmount;
                fs.UseWalletMoney1 = fs.UseWalletMoney;
                fs.UseWalletAccountPrincipal = uw.AccountPrincipal;
                if (!string.IsNullOrEmpty(uw.Ratio))
                {
                    fs.Ratio = Math.Round(100-Convert.ToDouble(uw.Ratio) * 100, 2).ToString() + '%';
                }
            }
            fs.RecordsOfConsumptionCreateTime = dateTime;
            fs.WriteOffUser = wfuserName;
            fs.ProductionCode1 = p.ProductCode;
            fs.ProductionName1 = p.ProductName;
            fs.ExperiencePrice = p.ExperiencePrice;
            fs.Iquantity1 = order.Number;
            fs.RecordsMoney = p.ExperiencePrice * order.Number;
            if (!string.IsNullOrEmpty(order.ExperienceVoucherCode))
            {
                var coupon = _couponService.GetCouponByCode(order.ExperienceVoucherCode);
                var couponMoney = _couponTypeService.GetCouponTypeByCode(coupon?.CouponTypeCode);
                if (couponMoney != null)
                {
                    fs.CouponUseMoney1 = couponMoney.Money;
                    fs.ActualConsumption = fs.RecordsMoney - fs.CouponUseMoney1;
                    if(fs.ActualConsumption<0m)
                    {
                        fs.ActualConsumption = 0m;
                    }
                    
                }
            }
            else {
                fs.ActualConsumption = fs.RecordsMoney;
            }

            if (payType.Equals("微信"))
            {
                fs.FinancialRevenueAccounting = fs.ActualConsumption;
            }
            else
            {
                fs.FinancialRevenueAccounting = RecordsAccountPrincipalMoney;


            }
            fs.Imoney = fs.FinancialRevenueAccounting /( Convert.ToDecimal((Convert.ToDouble(p.Rate)) * 0.01)+1);
            fs.ProductInfoRate = p.Rate + "%";
            fs.Itax = fs.Imoney * Convert.ToDecimal((Convert.ToDouble(p.Rate))*0.01); 
            fs.GrossProfit = fs.Imoney;
            return fs;
        }
    }
}
