using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

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

        public bool PayOrder(string productCode, string userCode, string peopleCount, DateTime dateTime)
        {
            IProductInfoService _productInfoService = new ProductInfoService();
            IOrderService _orderService = new OrderService();
            var p = _productInfoService.GetProductInfo(productCode);
            Order order = new Order();
            order.Money = p.ExperiencePrice;
            order.Number = 1;
            order.PayTime = DateTime.Now;
            order.StoreCode = "";
            order.UserCode = userCode;
            order.CreateTime = DateTime.Now;
            order.ExperienceVoucherCode = "";
            order.AppointmentTime = dateTime;
            _orderService.InsertOrder(order);
            return true;
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
