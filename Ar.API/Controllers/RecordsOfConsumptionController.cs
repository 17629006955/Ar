using Ar.Common.tool;
using Ar.Model;
using Ar.API.Controllers.BaseContolles;
using Ar.Model.BaseResult;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using Ar.IServices;
using Ar.Services;
using System.Transactions;
using AR.Model;
using Ar.Common;

namespace Ar.API.Controllers
{
    public class RecordsOfConsumptionController : BaseApiController
    {
        IUserInfo userInfo = new UserInfo();
        /// <summary>
        /// 获取消费记录
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/RecordsOfConsumption/GetRecordsOfConsumptionList
        [HttpGet]
        public IHttpActionResult GetRecordsOfConsumptionList()
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetRecordsOfConsumptionList();
                    result.Resource = list;
                    result.Status = Result.SUCCEED;
                }
                else
                {
                    result.Status = Result.FAILURE;
                    result.Resource = ReAccessToken;
                    result.Msg = TokenMessage;
                }
            }
            catch (Exception ex)
            {
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RecordsOfConsumption/GetRecordsOfConsumptionByCode?code=1
        [HttpGet]
        public IHttpActionResult GetRecordsOfConsumptionByCode(string code)
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetRecordsOfConsumptionByCode(code);
                    result.Resource = list;
                    result.Status = Result.SUCCEED;
                }
                else
                {
                    result.Status = ResultType;
                    result.Resource = ReAccessToken;
                    result.Msg = TokenMessage;
                }
            }
            catch (Exception ex)
            {
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }


        /// <summary>
        /// 获取消费记录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RecordsOfConsumption/GetRecordsOfConsumptionByUserCode?userCode=1
        [HttpGet]
        public IHttpActionResult GetRecordsOfConsumptionByUserCode(string userCode)
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetRecordsOfConsumptionListByUserCode(userCode);
                    list = list.OrderByDescending(t => t.CreateTime)?.ToList();
                    result.Resource = list;
                    result.Status = Result.SUCCEED;
                }
                else
                {
                    result.Status = ResultType;
                    result.Resource = ReAccessToken;
                    result.Msg = TokenMessage;
                }
            }
            catch (Exception ex)
            {
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }

        /// <summary>
        ///购买支付订单
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PayOrder([FromBody] PayOrderParam param)
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            ICouponService _couponService = new CouponService();
            IUseWalletService _useWalletService = new UseWalletService();
            IStoreService _stoeservice = new StoreService();
            IProductInfoService _productInfoService = new ProductInfoService();
            try
            {
                if (UserAuthorization)
                {
                    LogHelper.WriteLog("PayOrder接口");
                    LogHelper.WriteLog("productCode " + param.productCode);
                 
                    var isExistProduct = _productInfoService.IsExistProduct(param.productCode);
                    if (!isExistProduct)
                    {
                        result.Status = Result.SYSTEM_ERROR;
                        result.Msg = "商品已失效或不存在";
                        result.Resource = null;
                    }
                    if (param.paytype == 0)
                    {
                        LogHelper.WriteLog("会员支付 " + param.paytype);
                        
                        var isPay = true;
                        if (!string.IsNullOrEmpty(param.couponCode))
                        {
                            var n = _couponService.Exist(param.couponCode);
                            if (n == 1)
                            {
                                result.Status = Result.SYSTEM_ERROR;
                                result.Msg = "优惠卷不存在";
                                result.Resource = null;
                                isPay = false;
                            }
                            else if (n == 2)
                            {
                                result.Status = Result.SYSTEM_ERROR;
                                result.Msg = "优惠卷已经被使用";
                                result.Resource = null;
                                isPay = false;
                            }
                        }

                        if (isPay)
                        {
                            if (_useWalletService.ExistMoney(param.userCode, param.money))
                            {
                                var re = _service.PayOrder(param.productCode, param.userCode, param.peopleCount, param.dateTime, param.money, param.storeId, param.orderCode, param.couponCode);
                                result.Resource = re;
                                result.Status = Result.SUCCEED;
                            }
                            else
                            {
                                result.Status = Result.SYSTEM_ERROR;
                                result.Msg = "账号余额不足";
                                result.Resource = null;
                            }
                        }
                    }
                    else
                    {
                        var isPay = true;
                        if (!string.IsNullOrEmpty(param.couponCode))
                        {
                            var n = _couponService.Exist(param.couponCode);
                            if (n == 1)
                            {
                                result.Status = Result.SYSTEM_ERROR;
                                result.Msg = "优惠卷不存在";
                                result.Resource = null;
                                isPay = false;
                            }
                            else if (n == 2)
                            {
                                result.Status = Result.SYSTEM_ERROR;
                                result.Msg = "优惠卷已经被使用";
                                result.Resource = null;
                                isPay = false;
                            }
                        }
                        if (isPay)
                        {
                            using (var scope = new TransactionScope())//创建事务
                            {

                                IUserStoreService _userStoreservice = new UserStoreService();
                                var store = _stoeservice.GetStore(param.storeId);
                                var couponser = _couponService.GetCouponByCode(param.couponCode);
                                var userStoreser = _userStoreservice.GetUserStorebyUserCodestoreCode(param.userCode, param.storeId);
                                if (userStoreser != null )
                                {
                                    if (param.money==0)
                                    {
                                        //生成微信预支付订单
                                        var wxprepay = Common.wxPayOrderSomething(userStoreser.OpenID, param.money.ToString(), couponser?.CouponTypeName, store);
                                        if (wxprepay != null)
                                        {
                                            var order = _service.WxPayOrder(param.productCode, param.userCode, param.peopleCount, param.dateTime, param.money, wxprepay.prepayid, param.couponCode);

                                            WxOrder wxorder = new WxOrder();
                                            wxorder.order = order;
                                            wxorder.wxJsApiParam = wxprepay.wxJsApiParam;
                                            wxorder.prepayid = wxprepay.prepayid;
                                            result.Resource = wxorder;
                                            result.Status = Result.SUCCEED;
                                        }
                                        else
                                        {
                                            result.Msg = "微信下单失败，重新提交订单";
                                            result.Status = Result.SYSTEM_ERROR;
                                        }
                                    } else
                                    {
                                        var order = _service.WxPayNoMoneyOrder(param.productCode, param.userCode, param.peopleCount, param.dateTime, param.money, param.couponCode);

                                        WxOrder wxorder = new WxOrder();
                                        wxorder.order = order;
                                        result.Status = Result.SUCCEED;
                                    }
                                }
                                else
                                {
                                    result.Resource = "";
                                    result.Status = Result.SYSTEM_ERROR;
                                }
                                scope.Complete();//这是最后提交事务
                            }
                        }

                    }

                }
                else
                {
                    result.Status = ResultType;
                    result.Resource = ReAccessToken;
                    result.Msg = TokenMessage;
                }
            }
            catch (Exception ex)
            {
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }


        /// <summary>
        ///微信支付成功更新订单和优惠券
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RecordsOfConsumption/PayOrder?productCode=1&userCode=1&peopleCount=1&dateTime=2019-12-07&money=9&couponCode=1ac31b4d-e383-447a-9417-9c66ca9e6004 
        [HttpGet]
        public IHttpActionResult WxPayOrder(string userCode, string orderCode, string prepayid, string couponCode = "")
        {
            LogHelper.WriteLog("WxPayOrder接口");
            LogHelper.WriteLog("userCode " + userCode);
            LogHelper.WriteLog("orderCode " + orderCode);
            LogHelper.WriteLog("prepayid " + prepayid);
            LogHelper.WriteLog("couponCode " + couponCode);
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            IStoreService _stoeservice = new StoreService();
            ICouponService _couponservice = new CouponService();
            try
            {
                if (UserAuthorization)
                {

                    using (var scope = new TransactionScope())//创建事务
                    {
                        if (!string.IsNullOrEmpty(prepayid) )
                        {
                            var PayTime = Common.wxPayOrderQuery(prepayid);
                            LogHelper.WriteLog("微信支付时间： " + PayTime);
                            if (!string.IsNullOrEmpty(PayTime))
                            {
                                IOrderService _orderService = new OrderService();
                                ICouponService _couponService = new CouponService();
                                var order = _orderService.GetOrderByCode(orderCode);
                                LogHelper.WriteLog("更新的订单： " + orderCode);
                                order.PayTime = DateTime.Now;
                                _orderService.UpdateOrder(order);
                                _couponService.UsedUpdate(couponCode, userCode);
                                LogHelper.WriteLog("更新的钱包和优惠券couponCode： " + couponCode);
                                result.Status = Result.SUCCEED;
                                scope.Complete();//这是最后提交事务
                            }

                        }
                       
                    }

                }
                else
                {
                    result.Status = ResultType;
                    result.Resource = ReAccessToken;
                    result.Msg = TokenMessage;
                }
            }
            catch (Exception ex)
            {
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }
        /// <summary>
        /// 是否是核销人员
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        //http://localhost:10010//api/RecordsOfConsumption/IsWriteOffUser?userCode=1 
        public IHttpActionResult IsWriteOffUser(string userCode)
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            try
            {

                if (UserAuthorization)
                {
                    var use = userInfo.GetUserByCode(userCode);
                    if (use != null)
                    {

                        var re = _service.IsWriteOffUser(use.Phone);
                        result.Resource = re;
                        result.Status = Result.SUCCEED;

                    }
                    else
                    {
                        result.Msg = "没有当前用户";
                        result.Status = Result.SYSTEM_ERROR;
                    }
                }
                else
                {
                    result.Status = Result.FAILURE;
                    result.Resource = ReAccessToken;
                    result.Msg = TokenMessage;
                }
            }
            catch (Exception ex)
            {
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);
        }
       
        /// <summary>
        ///订单核销
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
            ////http://localhost:10010//api/RecordsOfConsumption/WriteOff?userCode=68add88&phone=18235139350&orderCode=1&writeOffCode=hjhhjh
        [HttpGet]
        [HttpPost]
        public IHttpActionResult WriteOff(string userCode, string orderCode)
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            IWriteOffService _writeOffServicee = new WriteOffService();
            IOrderService _orderservice = new OrderService();
            try
            {
                if (UserAuthorization)
                {
                    var use = userInfo.GetUserByCode(userCode);
                    if (use != null)
                    {
                         if (_service.IsWriteOffUser(use.Phone))
                        {
                            var order = _orderservice.GetOrderByCode(orderCode);
                            if (order != null && !order.IsWriteOff)
                            {
                                order.IsWriteOff = true;
                                if (_orderservice.UpdateOrder(order) > 0)
                                {
                                    result.Status = Result.SUCCEED;
                                }
                                else
                                {
                                    result.Msg = "订单已经被核销";
                                    result.Status = Result.SYSTEM_ERROR;
                                }

                            }
                            else
                            {
                                result.Msg = "订单已经被核销";
                                result.Status = Result.SYSTEM_ERROR;
                            }
                            
                        }
                        else
                        {
                            result.Msg = "您不是核销人员。";
                            result.Status = Result.SYSTEM_ERROR;
                        }
                    }
                    else
                    {
                        result.Msg = "没有当前用户";
                        result.Status = Result.SYSTEM_ERROR;
                    }
                }
                else
                {
                    result.Status = Result.FAILURE;
                    result.Resource = ReAccessToken;
                    result.Msg = TokenMessage;
                }
            }
            catch (Exception ex)
            {
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }
    }

    public class PayOrderParam
    {
        public string orderCode { get; set; }
        public int paytype { get; set; }
        public string productCode { get; set; }
        public string userCode { get; set; }
        public string peopleCount { get; set; }
        public DateTime dateTime { get; set; }
        public string couponCode { get; set; }
        public decimal money { get; set; }
        public string storeId { get; set; }
    }

}