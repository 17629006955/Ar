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
            try
            {
                if (UserAuthorization)
                {
                    if (param.paytype == 0)
                    {
                        if (!string.IsNullOrEmpty(param.couponCode))
                        {
                            var n = _couponService.Exist(param.couponCode);
                            if (n == 3)
                            {
                                if (_useWalletService.ExistMoney(param.userCode, param.money))
                                {
                                    var re = _service.PayOrder(param.productCode, param.userCode, param.peopleCount, param.dateTime, param.money, param.storeId, param.orderCode,param.couponCode);
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
                            else if (n == 1)
                            {
                                result.Status = Result.SYSTEM_ERROR;
                                result.Msg = "优惠卷不存在";
                                result.Resource = null;
                            }
                            else if (n == 2)
                            {
                                result.Status = Result.SYSTEM_ERROR;
                                result.Msg = "优惠卷已经被使用";
                                result.Resource = null;
                            }

                        }
                    }
                    else
                    {
                        using (var scope = new TransactionScope())//创建事务
                        {

                            IUserStoreService _userStoreservice = new UserStoreService();
                            var store = _stoeservice.GetStore(param.storeId);
                            var couponser = _couponService.GetCouponByCode(param.couponCode);
                            var userStoreser = _userStoreservice.GetUserStorebyUserCodestoreCode(param.userCode, param.storeId);
                            if (userStoreser != null)
                            {
                                //生成微信预支付订单
                                var wxprepay = Common.wxPayOrderSomething(userStoreser.OpenID, param.money.ToString(), couponser.CouponTypeName, store.StoreName);
                                if (wxprepay != null)
                                {
                                    var order = _service.WxPayOrder(param.productCode, param.userCode, param.peopleCount, param.dateTime, param.money, wxprepay.prepayid, param.couponCode);

                                    WxOrder wxorder = new WxOrder();
                                    wxorder.order = order;
                                    wxorder.wxJsApiParam = wxprepay.wxJsApiParam;
                                    result.Resource = wxorder;
                                    result.Status = Result.SUCCEED;
                                }
                                else
                                {
                                    result.Resource = "微信下单失败，重新提交订单";
                                    result.Status = Result.SYSTEM_ERROR;
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
        public IHttpActionResult WxPayOrder(string userCode, string orderCode, string couponCode = "")
        {
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
                        IOrderService _orderService = new OrderService();
                        ICouponService _couponService = new CouponService();
                        var order = _orderService.GetOrderByCode(orderCode);
                        order.PayTime = DateTime.Now;
                        _orderService.UpdateOrder(order);
                        _couponService.UsedUpdate(couponCode, userCode);
                        result.Status = Result.SUCCEED;
                        scope.Complete();//这是最后提交事务
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
                        result.Resource = "没有当前用户";
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
        /// 获取核销码
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        //http://localhost:10010//api/RecordsOfConsumption/WriteOff?orderCode=1 
        public IHttpActionResult WriteOff(string orderCode)
        {
            SimpleResult result = new SimpleResult();
            IOrderService _orderservice = new OrderService();
            IWriteOffService _writeOffServicee = new WriteOffService();
            try
            {

                if (UserAuthorization)
                {
                    var order = _orderservice.GetOrderByCode(orderCode);
                    if (order != null && !order.IsWriteOff)
                    {
                        _writeOffServicee.Delete(orderCode);
                        WriteOff writeOff = new WriteOff();
                        writeOff.WriteOffCode = Guid.NewGuid().ToString();
                        writeOff.OrderCode = orderCode;
                        writeOff.CreateTime = DateTime.Now;
                        _writeOffServicee.CreateWriteOff(writeOff);
                        result.Resource = writeOff.WriteOffCode;
                        result.Status = Result.SUCCEED;

                    }
                    else
                    {
                        result.Resource = "订单已经被核销";
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
        public IHttpActionResult WriteOff(string userCode, string orderCode, string writeOffCode)
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            IWriteOffService _writeOffServicee = new WriteOffService();
            try
            {
                if (UserAuthorization)
                {
                    var use = userInfo.GetUserByCode(userCode);
                    if (use != null)
                    {
                        if (!string.IsNullOrEmpty(use.Phone))
                        {

                            if (_writeOffServicee.CheckWriteOff(orderCode, writeOffCode))
                            {
                                var re = _service.WriteOff(use.Phone, orderCode);
                                result.Resource = re;
                                result.Status = Result.SUCCEED;
                            }
                            else
                            {
                                result.Resource = "核销二维码已经失效请用户重新打开二维码";
                                result.Status = Result.SYSTEM_ERROR;
                            }
                        }
                        else
                        {
                            result.Resource = "请您绑定手机号再核销";
                            result.Status = Result.SYSTEM_ERROR;
                        }
                    }
                    else
                    {
                        result.Resource = "没有当前用户";
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
       public  string productCode { get; set; }
        public string userCode { get; set; }
        public string peopleCount { get; set; }
        public DateTime dateTime { get; set; }
        public string couponCode { get; set; }
        public decimal money { get; set; }
        public string storeId  { get; set; }
    }

}