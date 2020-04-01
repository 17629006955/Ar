using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;
using AR.Model;
using Dapper;
using System.Transactions;
using System.Configuration;
using System.Net;
using WxPayAPI;
using Ar.Common;

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
            IList<RecordsOfConsumption> list = DapperSqlHelper.FindToList<RecordsOfConsumption>(@"select * from [dbo].[RecordsOfConsumption] 
             where UserCode=@UserCode AND ISNULL(IsRecharging,0)=0 ", paras, false);
            return list;
        }
        public bool InsertRecore(string typeCode,string userCode,decimal? recordsMoney,string explain, decimal? recordsdonationAmount, decimal? recordsaccountPrincipal,bool IsRecharging=true)
        {
            var tempRecord = DapperSqlHelper.FindOne<RecordsOfConsumption>("SELECT MAX(RecordsOfConsumptionCode) RecordsOfConsumptionCode FROM [dbo].[RecordsOfConsumption]", null, false);
            if (tempRecord != null && !string.IsNullOrEmpty(tempRecord.RecordsOfConsumptionCode))
            {
                tempRecord.RecordsOfConsumptionCode = Guid.NewGuid().ToString();
            }
            else
            {
                tempRecord.RecordsOfConsumptionCode = "1";

            }
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@IsRecharging", IsRecharging, System.Data.DbType.String);
            paras.Add("@UserCode", userCode, System.Data.DbType.String);
            paras.Add("@RechargeTypeCode", typeCode, System.Data.DbType.String);
            paras.Add("@RecordsMoney", recordsMoney, System.Data.DbType.Decimal);
            paras.Add("@Explain", explain, System.Data.DbType.String);
            paras.Add("@IsRecharging", IsRecharging, System.Data.DbType.Int16);
            paras.Add("@RecordsOfConsumptionCode", tempRecord.RecordsOfConsumptionCode, System.Data.DbType.String);
            paras.Add("@recordsdonationAmount", recordsdonationAmount, System.Data.DbType.Decimal);
            paras.Add("@recordsaccountPrincipal", recordsaccountPrincipal, System.Data.DbType.Decimal);
            var n = DapperSqlHelper.ExcuteNonQuery<RecordsOfConsumption>(@"INSERT INTO [dbo].[RecordsOfConsumption]
           ([RecordsOfConsumptionCode],[UserCode] ,[RechargeTypeCode]  ,[RecordsMoney],Explain,CreateTime,IsRecharging,RecordsDonationAmountMoney,RecordsAccountPrincipalMoney) 
            VALUES  ( @RecordsOfConsumptionCode, 
                      @UserCode, 
                      @RechargeTypeCode, 
                      @RecordsMoney, 
                      @Explain, 
                      getdate(),@IsRecharging,@recordsdonationAmount,@recordsaccountPrincipal)", paras, false);
            if (n > 0)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public string  PayOrder(string productCode, string userCode, string peopleCount, DateTime dateTime, decimal money, string storeId, string orderCode = "", string couponCode = "")
        {
            string msg = "SUCCEED";
            IProductInfoService _productInfoService = new ProductInfoService();
            DateTime now = DateTime.Now;
            ICouponService _couponService = new CouponService();
            IOrderService _orderService = new OrderService();
            IUserInfo _userService = new UserInfo();
            IStoreService _storeService = new StoreService();
            IUserStoreService _userStoreService = new UserStoreService();
            IRecordsOfConsumptionService _recordsOfConsumption = new RecordsOfConsumptionService();
            IUseWalletService _useWalletService = new UseWalletService();
            IFinancialStatementsService _financialStatementsService = new FinancialStatementService();
            ICouponTypeService _couponTypeService = new CouponTypeService();
            var u = _userService.GetUserByCode(userCode);
            var p = _productInfoService.GetProductInfo(productCode);
            var s=_storeService.GetStore(storeId);
            var uw = _useWalletService.GetUseWalletCountMoney(userCode);

            Order order = new Order();
            order.Money = money;
            int ss = 0;
            if (int.TryParse(peopleCount,out ss))
            {
                order.Number = ss;
            }
            order.PayTime = now;
            order.StoreCode = storeId;
            order.UserCode = userCode;
            order.ProductCode = productCode;
            order.CreateTime = now;
            order.ExperienceVoucherCode = couponCode;
            order.AppointmentTime = dateTime;
            order.WxPrepayId = string.Empty;
            financialStatements fs = _financialStatementsService.getData(userCode, order, "会员卡");
            using (var scope = new TransactionScope())//创建事务
            {
                if (!string.IsNullOrEmpty(orderCode))
                {
                    var temporder = _orderService.GetOrderByCode(orderCode);
                    if (temporder != null && temporder.UserCode == userCode)
                    {
                        order.OrderCode = temporder.OrderCode;
                        order.CreateTime = temporder.CreateTime;
                        order.OrderNO = temporder.OrderNO;
                        _orderService.UpdateOrder(order);
                        msg = temporder.OrderCode;
                    }
                    else
                    {
                        msg = _orderService.InsertOrder(order);
                    }
                }
                else
                {
                    msg=_orderService.InsertOrder(order);
                }
                if (money==0)
                {
                    LogHelper.WriteLog("会员支付0元 " + money);
                    LogHelper.WriteLog("couponCode " + couponCode);
                    _couponService.UsedUpdate(couponCode, userCode, orderCode);

                    LogHelper.WriteLog("financialStatements " + fs.Code);
                    _financialStatementsService.Insert(fs);
                }
                else
                {
                    LogHelper.WriteLog("couponCode " + couponCode);
                    _couponService.UsedUpdate(couponCode, userCode, orderCode);
                    LogHelper.WriteLog("会员支付金额 " + money);
                    _useWalletService.UpdateData(userCode, money);
                    LogHelper.WriteLog("financialStatements " + fs.Code);
                    _financialStatementsService.Insert(fs);
                }

               
                scope.Complete();//这是最后提交事务
            }
            return msg;
        }

        public Order WxPayOrder(string productCode, string userCode, string peopleCount, DateTime dateTime, decimal money, string wxPrepayId, string storeId,string orderCode = "", string couponCode = "")
        {
            IProductInfoService _productInfoService = new ProductInfoService();
            DateTime now = DateTime.Now;

            IOrderService _orderService = new OrderService();
            IUserStoreService _userStoreService = new UserStoreService();

          
            var userSotre = _userStoreService.GetUserStorebyUserCode(userCode);
            var tempOrder = _orderService.GetOrderByCode(orderCode);
            Order order = new Order();
            if (tempOrder != null && tempOrder.UserCode == userCode)
            {
              
                order.CreateTime = tempOrder.CreateTime;
                order.OrderCode = tempOrder.OrderCode;
                order.OrderNO = tempOrder.OrderNO;

            }
            else
            {
                order.CreateTime = now;
                order.OrderCode = Guid.NewGuid().ToString();
                order.OrderNO = WxPayApi.GenerateOutTradeNo().ToString();
                order.CreateTime = now;
            }
            order.Money = money;
            int ss = 0;
            if (int.TryParse(peopleCount, out ss))
            {
                order.Number = ss;
            }
            order.PayTime = null;
            order.StoreCode = storeId;
            order.UserCode = userCode;
            order.ProductCode = productCode;
            order.ExperienceVoucherCode = couponCode;
            order.AppointmentTime = dateTime;
            order.WxPrepayId = wxPrepayId;
            
            
            LogHelper.WriteLog("订单编码OrderCode " + order.OrderCode);
            LogHelper.WriteLog("订单号OrderNO " + order.OrderNO);
            if (tempOrder != null && tempOrder.UserCode == userCode)
            {
                _orderService.UpdateOrderbyWxorder(order);
            }
            else
            {
                _orderService.InsertOrder(order);
            }
            return order;
        }
        public Order WxPayNoMoneyOrder(string productCode, string userCode, string peopleCount, DateTime dateTime, decimal money,string orderCode = "", string couponCode = "")
        {
            IProductInfoService _productInfoService = new ProductInfoService();
            DateTime now = DateTime.Now;

            IOrderService _orderService = new OrderService();
            IUserStoreService _userStoreService = new UserStoreService();

            
            var userSotre = _userStoreService.GetUserStorebyUserCode(userCode);
            var tempOrder = _orderService.GetOrderByCode(orderCode);
            Order order = new Order();
            if (tempOrder != null && tempOrder.UserCode == userCode)
            {
              
                order.CreateTime = tempOrder.CreateTime;
                order.OrderCode = tempOrder.OrderCode;
                order.OrderNO = tempOrder.OrderNO;
            }
            else
            {
                order.OrderCode = Guid.NewGuid().ToString();
                order.OrderNO = WxPayApi.GenerateOutTradeNo().ToString();
                order.CreateTime = now;
            }
            order.Money = money;
            int ss = 0;
            if (int.TryParse(peopleCount, out ss))
            {
                order.Number = ss;
            }
            order.PayTime = DateTime.Now;
            order.StoreCode = userSotre.UserStoreCode;
            order.UserCode = userCode;
            order.ProductCode = productCode;
            order.ExperienceVoucherCode = couponCode;
            order.AppointmentTime = dateTime;
            LogHelper.WriteLog("订单编码OrderCode " + order.OrderCode);
            LogHelper.WriteLog("订单号OrderNO " + order.OrderNO);
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
