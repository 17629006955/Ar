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
    public class UseWalletController : BaseApiController
    {
        /// <summary>
        /// 获取会员钱包
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/UseWallet/GetUseWalletList
        [HttpGet]
        public IHttpActionResult GetUseWalletList()
        {
            LogHelper.WriteLog("GetUseWalletList ");
            SimpleResult result = new SimpleResult();
            IUseWalletService _service = new UseWalletService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetUseWalletList();
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
                LogHelper.WriteLog("GetUseWalletList " , ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;

            }
            LogHelper.WriteLog("GetUseWalletList result" + Json(result));
            return Json(result);

        }
        /// <summary>
        /// 获取会员钱包通过用户总金额，充值金额，赠送金额
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/UseWallet/GetUseWalletInfoByUserCode?userCode=1
        [HttpGet]
        public IHttpActionResult GetUseWalletInfoByUserCode(string userCode)
        {
            LogHelper.WriteLog("GetUseWalletInfoByUserCode userCode" + userCode);
           
            SimpleResult result = new SimpleResult();
            IUseWalletService _service = new UseWalletService();
            ITopupOrderServrce tos = new TopupOrderServrce();
            IRechargeRecordService _RechargeRecordService = new RechargeRecordService();
            IStoreService _Storeservice = new StoreService();
            IUserStoreService _userStoreService = new UserStoreService();
            try
            {
                if (UserAuthorization)
                {
                    //查看没有给微信支付核对的订单继续核对
                    var topupOrder = tos.GetTopupOrderbyuserCode(userCode);
                    var userSotre = _userStoreService.GetUserStorebyUserCode(userCode);
                    var store = _Storeservice.GetStore(userSotre.MembershipCardStore);
                  
                    if (store != null)
                    {
                        foreach (var item in topupOrder)
                        {
                            if (!string.IsNullOrEmpty(item.WallePrCode) && item.PayDatetime == null)
                            {
                                var PayTime = Common.wxPayOrderQuery(item.WallePrCode, store.appid.Trim(), store.mchid);
                                if (!string.IsNullOrEmpty(PayTime))
                                {
                                    LogHelper.WriteLog("GetUseWalletInfoByUserCode PayTime" + PayTime);
                                    DateTime dt = DateTime.ParseExact(PayTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                                    item.PayDatetime = dt;
                                    tos.UpdateTopupOrder(item.WallePrCode, item.PayDatetime);
                                    _RechargeRecordService.Recharge(item.RechargeTypeCode, item.UserCode, item.RecordsMoney, store.StoreCode);
                                }

                            }

                        }
                    }
                    var list = _service.GetUseWalletInfoByUserCode(userCode);
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
                LogHelper.WriteLog("GetUseWalletInfoByUserCode userCode" + userCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetUseWalletInfoByUserCode result" + Json(result));
            return Json(result);

        }


        /// <summary>
        /// 获取会员钱包通过用户
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/UseWallet/GetUseWalletUserCode?userCode=1
        [HttpGet]
        public IHttpActionResult GetUseWalletUserCode(string userCode)
        {
            LogHelper.WriteLog("GetUseWalletUserCode userCode" + userCode);
            SimpleResult result = new SimpleResult();
            IUseWalletService _service = new UseWalletService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetUseWallet(userCode);
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
                LogHelper.WriteLog("GetUseWalletUserCode userCode"+ userCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetUseWalletUserCode result" + Json(result));
            return Json(result);

        }

        /// <summary>
        /// 插入会员钱包
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/UseWallet/InsertUseWalle
        [HttpPost]
        public IHttpActionResult InsertUseWallet(UseWallet useWallet)
        {
            LogHelper.WriteLog("InsertUseWallet useWallet" + useWallet?.WalletCode);
            SimpleResult result = new SimpleResult();
            IUseWalletService _service = new UseWalletService();
            try
            {
                var list = _service.InsertUseWallet(useWallet);
                result.Resource = list;
                result.Status = Result.SUCCEED;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("InsertUseWallet useWallet"+ useWallet.UserCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("InsertUseWallet result" + Json(result));
            return Json(result);

        }

    }
}