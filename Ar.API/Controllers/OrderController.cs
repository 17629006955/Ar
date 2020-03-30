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
using AR.Model;
using Ar.Common;

namespace Ar.API.Controllers
{
    public class OrderController : BaseApiController
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/Order/GetOrderList?userCode=1
        [HttpGet]
        public IHttpActionResult GetOrderList(string userCode)
        {
            LogHelper.WriteLog("GetOrderList userCode" + userCode);
            SimpleResult result = new SimpleResult();
            IOrderService _service = new OrderService();
            IUserStoreService _userStoreService = new UserStoreService();
            IStoreService _Storeservice = new StoreService();

            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetOrderList(userCode);
                    var userSotre = _userStoreService.GetUserStorebyUserCode(userCode);
                    var store = _Storeservice.GetStore(userSotre.MembershipCardStore);
                    if (store != null)
                    {
                        foreach (var item in list)
                        {
                            if (!string.IsNullOrEmpty(item.WxPrepayId) && item.PayTime == null)
                            {

                                var PayTime = Common.wxPayOrderQuery(item.WxPrepayId, store.appid.Trim(), store.mchid);
                                if (!string.IsNullOrEmpty(PayTime))
                                {
                                    item.PayTime = Convert.ToDateTime(PayTime);
                                    _service.UpdateOrder(item);
                                }

                            }


                        }
                    }
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
                LogHelper.WriteLog("GetOrderList userCode" + userCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetOrderList result" + Json(result));
            return Json(result);

        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Order/GetOrderByCode?code=1
        [HttpGet]
        public IHttpActionResult GetOrderByCode(string code)
        {
            LogHelper.WriteLog("GetOrderByCode code" + code);
            SimpleResult result = new SimpleResult();
            IOrderService _service = new OrderService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetOrderByCode(code);
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
             
                LogHelper.WriteLog("GetOrderByCode code" + code, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetOrderByCode result" + Json(result));
            return Json(result);

        }


        /// <summary>
        /// 获取可使用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Order/GetPayOrderList?userCode=1
        [HttpGet]
        public IHttpActionResult GetPayOrderList(string userCode)
        {
            LogHelper.WriteLog("GetPayOrderList userCode" + userCode);
            SimpleResult result = new SimpleResult();
            IOrderService _service = new OrderService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetPayOrderList(userCode);
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
                LogHelper.WriteLog("GetPayOrderList userCode" + userCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetPayOrderList result" + Json(result));
            return Json(result);

        }


        /// <summary>
        /// 获取未支付
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Order/GetNOPayOrderList?userCode=1
        [HttpGet]
        public IHttpActionResult GetNOPayOrderList(string userCode)
        {
            LogHelper.WriteLog("GetNOPayOrderList userCode" + userCode);
            SimpleResult result = new SimpleResult();
            IOrderService _service = new OrderService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetNOPayOrderList(userCode);
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
                LogHelper.WriteLog("GetNOPayOrderList userCode" + userCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetNOPayOrderList result" + Json(result));
            return Json(result);

        }
        /// <summary>
        ///  更新订单
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Order/UpdateOrder
        [HttpPost]
        public IHttpActionResult UpdateOrder(Order order)
        {
            LogHelper.WriteLog("UpdateOrder start");
            SimpleResult result = new SimpleResult();
            IOrderService _service = new OrderService();
            try
            {
                if (UserAuthorization)
                {
                    _service.UpdateOrder(order);
                    result.Resource = null;
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
                LogHelper.WriteLog("UpdateOrder order" + order.OrderCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }
        /// <summary>
        ///   新增订单
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Order/InsertOrder
        [HttpPost]
        public IHttpActionResult InsertOrder(Order order)
        {
            SimpleResult result = new SimpleResult();
            IOrderService _service = new OrderService();
            try
            {
                if (UserAuthorization)
                {
                 _service.InsertOrder(order);
                    result.Resource = null;
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
                LogHelper.WriteLog("InsertOrder order" + order.OrderCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }
        /// <summary>
        ///  下单购买（微信支付）
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Order/WxOrder
        [HttpPost]
        public IHttpActionResult WxOrder(Order order)
        {
            SimpleResult result = new SimpleResult();
            IOrderService _service = new OrderService();
            try
            {
                if (UserAuthorization)
                {
                    _service.InsertOrder(order);
                    result.Resource = null;
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
                LogHelper.WriteLog("WxOrder order"+ order.OrderCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }
    }
}