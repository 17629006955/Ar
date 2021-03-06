﻿using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;
using WxPayAPI;

namespace Ar.Services
{
    public class OrderService : IOrderService
    {
        public int DeletOrderInfo(string orderCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@OrderCode", orderCode, System.Data.DbType.String);
            return DapperSqlHelper.ExcuteNonQuery<Order>(@"delete  [dbo].[Order] where OrderCode=@OrderCode", paras, false);
            
        }
        public Order GetOrderInfo(string orderCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@OrderCode", orderCode, System.Data.DbType.String);
            Order order = DapperSqlHelper.FindOne<Order>(@"select * from [dbo].[Order] where OrderCode=@OrderCode",paras,false);
            return order;
        }
        public Order GetOrderByCode(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@OrderCode", code, System.Data.DbType.String);
            Order order = DapperSqlHelper.FindOne<Order>(@"select a.*,b.ProductCode,b.ProductName,b.Imageurl,b.videourl,
                 case when ISNULL(a.PayTime,'')='' then 0 
				 WHEN   ISNULL(a.PayTime,'')!='' AND 
				isnull(a.IsWriteOff,0)=1 THEN  1
				WHEN  ISNULL(a.PayTime,'')!='' AND 
				ISNULL(a.IsWriteOff,0)=0 THEN  2 end OrderState 
                from [dbo].[Order] a,[dbo].[ProductInfo] b  where a.OrderCode=@OrderCode  
             and b.ProductCode = a.ProductCode", paras, false);
            return order;
        }

        public IList<Order> GetOrderList(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            IList<Order> list = DapperSqlHelper.FindToList<Order>(@"select a.*,b.ProductCode,b.ProductName,b.Imageurl,b.videourl,
                 case when ISNULL(a.PayTime,'')='' then 0 
				 WHEN   ISNULL(a.PayTime,'')!='' AND 
				isnull(a.IsWriteOff,0)=1 THEN  1
				WHEN  ISNULL(a.PayTime,'')!='' AND 
				ISNULL(a.IsWriteOff,0)=0 THEN  2 end OrderState 
                from [dbo].[Order] a,[dbo].[ProductInfo] b  where a.UserCode=@userCode  
             and b.ProductCode = a.ProductCode", paras, false);
            return list;
        }

        public IList<Order> GetPayOrderList(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            IList<Order> list = DapperSqlHelper.FindToList<Order>(@"select a.*,b.ProductCode,b.ProductName,b.Imageurl,b.videourl,
               2 OrderState 
                from [dbo].[Order] a,[dbo].[ProductInfo] b  where a.UserCode=@userCode 
             and b.ProductCode = a.ProductCode and isnull(a.PayTime,'')!='' and isnull(a.IsWriteOff,0)=0", paras, false);
            return list;
        }

        public IList<Order> GetNOPayOrderList(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            IList<Order> list = DapperSqlHelper.FindToList<Order>(@"select a.*,b.ProductCode,b.ProductName,b.Imageurl,b.videourl,
                0 OrderState 
                from [dbo].[Order] a,[dbo].[ProductInfo] b  where a.UserCode=@userCode 
             and b.ProductCode = a.ProductCode and isnull(a.PayTime,'') ='' ", paras, false);
            return list;
        }

        public int UpdateOrder(Order order)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@OrderCode", order.OrderCode, System.Data.DbType.String);
            paras.Add("@UserCode", order.UserCode, System.Data.DbType.String);
            paras.Add("@ProductCode", order.ProductCode, System.Data.DbType.String);
            paras.Add("@Number", order.Number, System.Data.DbType.Int32);
            paras.Add("@Money", order.Money, System.Data.DbType.Decimal);
            paras.Add("@StoreCode", order.StoreCode, System.Data.DbType.String);
            paras.Add("@PayTime", order.PayTime, System.Data.DbType.DateTime);
            paras.Add("@AppointmentTime", order.AppointmentTime, System.Data.DbType.DateTime);
            paras.Add("@ExperienceVoucherCode", order.ExperienceVoucherCode, System.Data.DbType.String);
            paras.Add("@IsWriteOff", order.IsWriteOff, System.Data.DbType.Boolean);
            paras.Add("@WxPrepayId", order.WxPrepayId, System.Data.DbType.String);
            paras.Add("@OrderNO", order.OrderNO, System.Data.DbType.String);
            string sql = @"update [dbo].[Order] set  UserCode=@UserCode,ProductCode=@ProductCode,Number=@Number,
                        Money=@Money,StoreCode=@StoreCode,PayTime=@PayTime,AppointmentTime=@AppointmentTime,ExperienceVoucherCode=@ExperienceVoucherCode,IsWriteOff=@IsWriteOff,WxPrepayId=@WxPrepayId,OrderNO=@OrderNO
                        where OrderCode=@OrderCode";
            return DapperSqlHelper.ExcuteNonQuery<Order>(sql, paras, false);
        }
        public int UpdateOrderbyWxorder(Order order)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@OrderCode", order.OrderCode, System.Data.DbType.String);
            paras.Add("@UserCode", order.UserCode, System.Data.DbType.String);
            paras.Add("@ProductCode", order.ProductCode, System.Data.DbType.String);
            paras.Add("@Number", order.Number, System.Data.DbType.Int32);
            paras.Add("@Money", order.Money, System.Data.DbType.Decimal);
            paras.Add("@StoreCode", order.StoreCode, System.Data.DbType.String);
            paras.Add("@PayTime", order.PayTime, System.Data.DbType.DateTime);
            paras.Add("@AppointmentTime", order.AppointmentTime, System.Data.DbType.DateTime);
            paras.Add("@ExperienceVoucherCode", order.ExperienceVoucherCode, System.Data.DbType.String);
            paras.Add("@OrderNO", order.OrderNO, System.Data.DbType.String);
            paras.Add("@WxPrepayId", order.WxPrepayId, System.Data.DbType.String);
            string sql = @"update [dbo].[Order] set  UserCode=@UserCode,ProductCode=@ProductCode,Number=@Number,
                        Money=@Money,StoreCode=@StoreCode,PayTime=@PayTime,AppointmentTime=@AppointmentTime,ExperienceVoucherCode=@ExperienceVoucherCode,WxPrepayId=@WxPrepayId,OrderNO=@OrderNO
                        where OrderCode=@OrderCode";
            return DapperSqlHelper.ExcuteNonQuery<Order>(sql, paras, false);
        }

        public string InsertOrder(Order order)
        {
            DynamicParameters paras = new DynamicParameters();
           
            paras.Add("@OrderCode", order.OrderCode, System.Data.DbType.String);
            paras.Add("@UserCode", order.UserCode, System.Data.DbType.String);
            paras.Add("@ProductCode", order.ProductCode, System.Data.DbType.String);
            paras.Add("@Number", order.Number, System.Data.DbType.Int32);
            paras.Add("@Money", order.Money, System.Data.DbType.Decimal);
            paras.Add("@StoreCode", order.StoreCode, System.Data.DbType.String);
            paras.Add("@PayTime", order.PayTime, System.Data.DbType.DateTime);
            paras.Add("@AppointmentTime", order.AppointmentTime, System.Data.DbType.DateTime);
            paras.Add("@ExperienceVoucherCode", order.ExperienceVoucherCode, System.Data.DbType.String);
            paras.Add("@OrderNO", order.OrderNO, System.Data.DbType.String);
            paras.Add("@WxPrepayId", order.WxPrepayId, System.Data.DbType.String);
            string sql = @"insert into [dbo].[Order](OrderCode,UserCode,ProductCode,Number,Money,StoreCode,
                    CreateTime,PayTime,AppointmentTime,ExperienceVoucherCode,OrderNO,WxPrepayId)
                    values(@OrderCode,@UserCode,@ProductCode,@Number,@Money,@StoreCode,
                    getdate(),@PayTime,@AppointmentTime,@ExperienceVoucherCode,@OrderNO,@WxPrepayId)";
            DapperSqlHelper.ExcuteNonQuery<Order>(sql, paras, false);
            return order.OrderCode;
        }

        public string GetMaxCode()
        {
            var order = DapperSqlHelper.FindOne<Order>("SELECT MAX(OrderCode) OrderCode FROM [dbo].[Order]", null, false);
            var code = order != null ? Convert.ToInt32(order.OrderCode) + 1 : 1;
            return code.ToString();
        }
    }
}
