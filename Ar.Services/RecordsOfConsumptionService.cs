using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;
using AR.Model;
using Dapper;
using System.Transactions;
using System.Configuration;
using System.Net;

namespace Ar.Services
{
    public class RecordsOfConsumptionService : IRecordsOfConsumptionService
    {
        public RecordsOfConsumption GetRecordsOfConsumptionByCode(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@code", code, System.Data.DbType.String);
            RecordsOfConsumption rechargeRecord = DapperSqlHelper.FindOne<RecordsOfConsumption>("select * from [dbo].[RecordsOfConsumption] where RecordsOfConsumptionCode=@code", paras, false);
            return rechargeRecord;
        }

        public IList<RecordsOfConsumption> GetRecordsOfConsumptionList()
        {
            DynamicParameters paras = new DynamicParameters();
            IList<RecordsOfConsumption> list = DapperSqlHelper.FindToList<RecordsOfConsumption>(@"select * from [dbo].[RecordsOfConsumption]", null, false);
            return list;
        }


        public IList<RecordsOfConsumption> GetRecordsOfConsumptionListByUserCode(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@UserCode", userCode, System.Data.DbType.String);
            IList<RecordsOfConsumption> list = DapperSqlHelper.FindToList<RecordsOfConsumption>(@"select * from [dbo].[RecordsOfConsumption] where UserCode=@UserCode", paras, false);
            return list;
        }
        public bool InsertRecore(string typeCode,string userCode,decimal? recordsMoney,string explain)
        {
            var tempRecord = DapperSqlHelper.FindOne<RecordsOfConsumption>("SELECT MAX(RecordsOfConsumptionCode) RecordsOfConsumptionCode FROM [dbo].[RecordsOfConsumption]", null, false);
            if (tempRecord != null && !string.IsNullOrEmpty(tempRecord.RecordsOfConsumptionCode))
            {
                tempRecord.RecordsOfConsumptionCode = tempRecord.RecordsOfConsumptionCode;
            }
            else
            {
                tempRecord.RecordsOfConsumptionCode = "1";

            }
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@UserCode", userCode, System.Data.DbType.String);
            paras.Add("@RechargeTypeCode", typeCode, System.Data.DbType.String);
            paras.Add("@RecordsMoney", recordsMoney, System.Data.DbType.Decimal);
            paras.Add("@Explain", explain, System.Data.DbType.String);
            paras.Add("@RecordsOfConsumptionCode", tempRecord.RecordsOfConsumptionCode, System.Data.DbType.String);
            var n = DapperSqlHelper.ExcuteNonQuery<RecordsOfConsumption>(@"INSERT INTO [dbo].[RecordsOfConsumption]
           ([RecordsOfConsumptionCode],[UserCode] ,[RechargeTypeCode]  ,[RecordsMoney],Explain,CreateTime) 
            VALUES  ( @RecordsOfConsumptionCode, 
                      @UserCode, 
                      @RechargeTypeCode, 
                      @RecordsMoney, 
                      @Explain, 
                      getdate())", paras, false);
            return true;
        }

        public bool PayOrder(string productCode, string userCode, string peopleCount, DateTime dateTime,decimal money, string storeId, string couponCode="")
        {
            IProductInfoService _productInfoService = new ProductInfoService();
            DateTime now = DateTime.Now;
           ICouponService _couponService = new CouponService();
            IOrderService _orderService = new OrderService();
            IUserStoreService  _userStoreService = new UserStoreService();
            IUseWalletService _useWalletService = new UseWalletService();
            var p = _productInfoService.GetProductInfo(productCode);
            //var userSotre=_userStoreService.GetUserStorebyUserCode(userCode);
            Order order = new Order();
            order.Money = p.ExperiencePrice;
            order.Number = 1;
            order.PayTime = now;
            order.StoreCode = storeId;
            order.UserCode = userCode;
            order.ProductCode = productCode;
            order.CreateTime = now;
            order.ExperienceVoucherCode = couponCode;
            order.AppointmentTime = dateTime;
            using (var scope = new TransactionScope())//创建事务
            {
                _orderService.InsertOrder(order);
                _couponService.UsedUpdate(couponCode, userCode);
                _useWalletService.UpdateData(userCode, money);
                scope.Complete();//这是最后提交事务
            }
            return true;
        }
        public Order WxPayOrder(string productCode, string userCode, string peopleCount, DateTime dateTime, decimal money, string wxPrepayId,string couponCode = "")
        {
            IProductInfoService _productInfoService = new ProductInfoService();
            DateTime now = DateTime.Now;
            
            IOrderService _orderService = new OrderService();
            IUserStoreService _userStoreService = new UserStoreService();
     
            var p = _productInfoService.GetProductInfo(productCode);
            var userSotre = _userStoreService.GetUserStorebyUserCode(userCode);
            Order order = new Order();
            order.Money = p.ExperiencePrice;
            order.Number = 1;
            order.PayTime = now;
            order.StoreCode = userSotre.UserStoreCode;
            order.UserCode = userCode;
            order.ProductCode = productCode;
            order.CreateTime = now;
            order.ExperienceVoucherCode = couponCode;
            order.AppointmentTime = dateTime;
            order.WxPrepayId = wxPrepayId;
            _orderService.InsertOrder(order);
             
             
            return order;
        }
      
        public bool IsWriteOffUser(string phone)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@phone", phone, System.Data.DbType.String);
            string sql = @"select ID as Code from [dbo].[TDF_SYSTEM_USER] where PHONE=@phone";
            var list = DapperSqlHelper.FindToList<User>(sql, paras, false);
            if (list.Count > 0)
            {
             return true;
            }
            return false;
        }
        /// <summary>
        /// 核销
        /// </summary>
        /// <param name="tel"></param>
        /// <param name="orderCode"></param>
        public bool WriteOff(string phone, string orderCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@phone", phone, System.Data.DbType.String);
            string sql = @"select ID as Code from [dbo].[TDF_SYSTEM_USER] where PHONE=@phone";
            var list=DapperSqlHelper.FindToList<User>(sql, paras, false);
            if (list.Count > 0)
            {
                paras = new DynamicParameters();
                paras.Add("@orderCode", orderCode, System.Data.DbType.String);
                sql = @"select * from [dbo].[Order] where OrderCode=@orderCode";
                list = DapperSqlHelper.FindToList<User>(sql, paras, false);
                if (list.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
