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

namespace Ar.API.Controllers
{
    public class RechargeRecordController : BaseApiController
    {
        /// <summary>
        /// 获取充值记录列表
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/RechargeRecord/GetRechargeRecordList
        [HttpGet]
        [HttpPost]
        public IHttpActionResult GetRechargeRecordList()
        {
            SimpleResult result = new SimpleResult();
            IRechargeRecordService _service = new RechargeRecordService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetRechargeRecordList();
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
            catch(Exception ex)
            {
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }


        /// <summary>
        /// 获取充值记录列表
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/RechargeRecord/GetRechargeRecordListByUserCode?userCode=1
        [HttpGet]
        [HttpPost]
        public IHttpActionResult GetRechargeRecordListByUserCode(string userCode)
        {
            SimpleResult result = new SimpleResult();
            IRechargeRecordService _service = new RechargeRecordService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetRechargeRecordListByUserCode(userCode);
                    list = list.OrderByDescending(t => t.Createtime)?.ToList();
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
        /// 获取充值记录详情
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RechargeRecord/GetRechargeRecordByCode?code=1
        [HttpGet]
        [HttpPost]
        public IHttpActionResult GetRechargeRecordByCode(string code)
        {
            SimpleResult result = new SimpleResult();
            IRechargeRecordService _service = new RechargeRecordService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetRechargeRecordByCode(code);
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
        /// 充值预订单
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RechargeRecord/Recharge?typeCode=1&userCode=1
        [HttpGet]
        [HttpPost]
        public IHttpActionResult RechargePrePay(string typeCode, string userCode, string storecode )
        {
            ICouponService _couponService = new CouponService();
            IUseWalletService _useWalletService = new UseWalletService();
            IStoreService _stoeservice = new StoreService();
            SimpleResult result = new SimpleResult();
            IRechargeRecordService _service = new RechargeRecordService();
            try
            {
                if (UserAuthorization)
                {
                    IUserStoreService _userStoreservice = new UserStoreService();
                    IRechargeTypeService s = new RechargeTypeService();
                    var store = _stoeservice.GetStore(storecode);
                    var userStoreser = _userStoreservice.GetUserStorebyUserCodestoreCode(userCode, storecode);
                    if (userStoreser != null)
                    {//生成微信预支付订单
                        var type = s.GetRechargeTypeByCode(typeCode);
                        var donationAmount = type.DonationAmount;
                        var money = type.Money;
                        var wxprepay = Common.wxPayOrderSomething(userStoreser.OpenID, money.ToString(), type.RechargeTypeName, store.StoreName);
                        if (wxprepay != null)
                        {
                           //更新充值预订单

                            WxOrder wxorder = new WxOrder();
                            wxorder.order =null;
                            wxorder.wxJsApiParam = wxprepay.wxJsApiParam;
                            result.Resource = wxorder;
                            result.Status = Result.SUCCEED;
                        }
                        else
                        {
                            result.Resource = "微信充值失败，重新充值";
                            result.Status = Result.SYSTEM_ERROR;
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
        /// 充值
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RechargeRecord/Recharge?typeCode=1&userCode=1
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Recharge(string typeCode, string userCode)
        {
            SimpleResult result = new SimpleResult();
            IRechargeRecordService _service = new RechargeRecordService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.Recharge(typeCode, userCode);
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
        /// 充值
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RechargeRecord/InsertRechargeRecord
        [HttpPost]
        public IHttpActionResult InsertRechargeRecord(RechargeRecord record)
        {
            SimpleResult result = new SimpleResult();
            IRechargeRecordService _service = new RechargeRecordService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.InsertRechargeRecord(record);
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
        /// 获取充值页面数据
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RechargeRecord/GetRechargePage?userCode=1
        [HttpGet]
        [HttpPost]
        public IHttpActionResult GetRechargePage(string userCode)
        {
            SimpleResult result = new SimpleResult();
            IUseWalletService _service = new UseWalletService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetRechargePage(userCode);
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
    }
}