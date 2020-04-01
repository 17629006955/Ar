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
using System.Transactions;
using Ar.Common;

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
            LogHelper.WriteLog("GetProductInfoListByListCode start");
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
                LogHelper.WriteLog("GetRechargeRecordList" , ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetRechargeRecordList result" + Json(result));
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

            LogHelper.WriteLog("GetRechargeRecordListByUserCode userCode" + userCode);
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
                LogHelper.WriteLog("GetRechargeRecordListByUserCode userCode" + userCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetRechargeRecordListByUserCode result" + Json(result));
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
            LogHelper.WriteLog("GetRechargeRecordByCode code" + code);
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
                LogHelper.WriteLog("GetRechargeRecordByCode code" + code, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetRechargeRecordByCode result" + Json(result));
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
        public IHttpActionResult  Recharge (string typeCode, string userCode, string storeCode, decimal? money = 0)
        {
            LogHelper.WriteLog("Recharge typeCode" + typeCode);
            LogHelper.WriteLog("Recharge userCode" + userCode);
            LogHelper.WriteLog("Recharge storeCode" + storeCode);
            LogHelper.WriteLog("Recharge money" + money);
            ICouponService _couponService = new CouponService();
            IUseWalletService _useWalletService = new UseWalletService();
            IStoreService _stoeservice = new StoreService();
            SimpleResult result = new SimpleResult();
            IRechargeRecordService _service = new RechargeRecordService();
            try
            {
                if (UserAuthorization)
                {
                    using (var scope = new TransactionScope())//创建事务
                    {
                        IUserStoreService _userStoreservice = new UserStoreService();
                    IRechargeTypeService s = new RechargeTypeService();
                    ITopupOrderServrce tos = new TopupOrderServrce();
                    var store = _stoeservice.GetStore(storeCode);
                    var userStoreser = _userStoreservice.GetUserStorebyUserCodestoreCode(userCode, storeCode);
                        if (userStoreser != null)
                        {//生成微信预支付订单

                            string rechargeTypeName = "充值";
                            decimal? donationAmount = 0;
                            if (money >= 0)
                            {
                                typeCode = "0";
                                donationAmount = 0;
                            }
                            else
                            {
                                var type = s.GetRechargeTypeByCode(typeCode);
                                rechargeTypeName = type.RechargeTypeName;
                                donationAmount = type?.DonationAmount;
                                money = type?.Money;
                            }
                            var wxprepay = Common.wxPayOrderSomething(userStoreser.OpenID, money.ToString(), rechargeTypeName, store);
                            if (wxprepay != null)
                            {
                                //更新充值预订单
                                //给TopupOrder写数据
                                tos.InsertTopupOrder(userCode, wxprepay.prepayid, typeCode, money);

                                WxOrder wxorder = new WxOrder();
                                wxorder.orderCode = null;
                                wxorder.wxJsApiParam = wxprepay.wxJsApiParam;
                                wxorder.prepayid = wxprepay.prepayid;
                                result.Resource = wxorder;
                                wxorder.IsWxPay = true;
                                result.Status = Result.SUCCEED;
                            }
                            else
                            {
                                result.Resource = "微信充值失败，重新充值";
                                result.Status = Result.SYSTEM_ERROR;
                            }
                           
                        }
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
         
                LogHelper.WriteLog("wxPrePay typeCode" + typeCode + " userCode" + userCode+ " storeCode" + storeCode+ " money"+ money, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("Recharge result" + Json(result));
            return Json(result);
        }
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RechargeRecord/Recharge?prepayid=hgjj
        [HttpGet]
        [HttpPost]
        public IHttpActionResult wxPrePay(string prepayid,string storeCode)
        {
            LogHelper.WriteLog("wxPrePay prepayid" + prepayid);
            LogHelper.WriteLog("wxPrePay storeCode" + storeCode);

            SimpleResult result = new SimpleResult();
            IRechargeRecordService _service = new RechargeRecordService();
            ITopupOrderServrce tos = new TopupOrderServrce();
            IStoreService _Storeservice = new StoreService();
            IUserStoreService _userStoreService = new UserStoreService();
            try
            {
                if (UserAuthorization)
                {
                    using (var scope = new TransactionScope())//创建事务
                    {
                        var opupOrder=tos.GetTopupOrderbyWallePrCode(prepayid);
                        if (opupOrder!=null )
                        {
                            var userSotre = _userStoreService.GetUserStorebyUserCode(opupOrder.UserCode);
                            var store = _Storeservice.GetStore(userSotre.MembershipCardStore);                    
                            if (store != null)
                            {
                                if (!string.IsNullOrEmpty(prepayid))
                                {
                                    var PayTime = Common.wxPayOrderQuery(prepayid, store.appid.Trim(), store.mchid);
                                    if (!string.IsNullOrEmpty(PayTime))
                                    {
                                        LogHelper.WriteLog("wxPrePay PayTime" + PayTime);
                                        DateTime dt = DateTime.ParseExact(PayTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                                        var payTime = dt;
                                        //更新TopupOrder 的支付时间
                                        tos.UpdateTopupOrder(prepayid, payTime);
                                        var tosmodel = tos.GetTopupOrderbyWallePrCode(prepayid);
                                        var list = _service.Recharge(tosmodel.RechargeTypeCode, tosmodel.UserCode, tosmodel.RecordsMoney, storeCode);
                                        result.Resource = list;
                                        result.Status = Result.SUCCEED;
                                       
                                    }
                                }

                            }
                        }
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
                LogHelper.WriteLog("wxPrePay prepayid" + prepayid+ " storeCode"+ storeCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("wxPrePay result" + Json(result));
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
            LogHelper.WriteLog("InsertRechargeRecord " + record);
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
                LogHelper.WriteLog("InsertRechargeRecord record" + record.ToString(), ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("InsertRechargeRecord result" + Json(result));
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
            LogHelper.WriteLog("GetRechargePage userCode" + userCode);
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
                LogHelper.WriteLog("GetRechargePage userCode"+ userCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetRechargePage result" + Json(result));
            return Json(result);
        }
    }
}