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
    public class CouponController : BaseApiController
    {
        /// <summary>
        /// 获取优惠券
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/Coupon/GetCouponByUserCode?userCode=1
        [HttpGet]
        public IHttpActionResult GetCouponByUserCode(string userCode)
        {
            LogHelper.WriteLog("GetCouponByUserCode start");
            LogHelper.WriteLog("GetCouponByUserCode获取"+userCode+"优惠卷");
            SimpleResult result = new SimpleResult();
            ICouponService _service = new CouponService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetCoupon(userCode);
                    result.Resource = list;
                    LogHelper.WriteLog("GetCouponByUserCode list"+ list.ToString());
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
                LogHelper.WriteLog("GetCouponByUserCode获取" + userCode + "优惠卷："+ ex.Message,ex);
                LogHelper.WriteLog("GetCouponByUserCode获取" + userCode + "优惠卷："+ ex.StackTrace,ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetCouponByUserCode end");
            return Json(result);

        }

        /// <summary>
        /// 获取优惠券
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/Coupon/GetCouponByCode?code=1
        [HttpGet]
        public IHttpActionResult GetCouponByCode(string code)
        {
            LogHelper.WriteLog("GetCouponByCode start");
            LogHelper.WriteLog("GetCouponByCode根据CouponUseCode获取优惠卷信息：code=" + code);
            SimpleResult result = new SimpleResult();
            ICouponService _service = new CouponService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetCouponByCode(code);
                    LogHelper.WriteLog("GetCouponByCode list" + list);
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
                LogHelper.WriteLog("GetCouponByCode根据CouponUseCode获取优惠卷信息：code=" + code + ex.Message,ex);
                LogHelper.WriteLog("GetCouponByCode根据CouponUseCode获取优惠卷信息：code=" + code + ex.StackTrace,ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetCouponByCode end");
            return Json(result);

        }

        /// <summary>
        /// 获取优惠券
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Coupon/GetCouponList?userCode=1
        [HttpGet]
        public IHttpActionResult GetCouponList(string userCode)
        {
            LogHelper.WriteLog("GetCouponList start");
            SimpleResult result = new SimpleResult();
            ICouponService _service = new CouponService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetCouponList(userCode);
                    result.Resource = list;
                    LogHelper.WriteLog("GetCouponList list"+ list);
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
                LogHelper.WriteLog("GetCouponList获取用户的优惠卷信息userCode=" + userCode+ ex.Message,ex);
                LogHelper.WriteLog("GetCouponList获取用户的优惠卷信息userCode=" + userCode + ex.StackTrace,ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetCouponList end");
            return Json(result);

        }

        /// <summary>
        /// 给用户添加优惠卷
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Coupon/InsertCouponByUser?couponCode=1&userCode=1
        [HttpGet]
        public IHttpActionResult InsertCouponByUser(string couponCode,string userCode)
        {
            LogHelper.WriteLog("InsertCouponByUser start");
            LogHelper.WriteLog("InsertCouponByUser couponCode" + couponCode);
            LogHelper.WriteLog("InsertCouponByUser userCode" + userCode);
            SimpleResult result = new SimpleResult();
            ICouponService _service = new CouponService();
            try
            {
                if (UserAuthorization)
                {
                    var re = _service.InsertCouponByUser(couponCode, userCode);
                    result.Resource = re;
                    result.Status = re?Result.SUCCEED: Result.SYSTEM_ERROR;
                    result.Msg = re ?"":"优惠卷不存在或者已经过期！";
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
                LogHelper.WriteLog("InsertCouponByUser添加用户的优惠卷信息userCode=" + userCode + ",couponCode;" + couponCode + ex.Message,ex);
                LogHelper.WriteLog("InsertCouponByUser添加用户的优惠卷信息userCode=" + userCode + ",couponCode;" + couponCode + ex.StackTrace,ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("InsertCouponByUser end");
            return Json(result);

        }
        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public string Str(int Length, bool Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        /// <summary>
        /// 获取完成任务优惠券
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Coupon/GiveedUpdate?phone=18235139350
        [HttpGet]
        public IHttpActionResult GiveedUpdate(string phone)
        {
            LogHelper.WriteLog("GiveedUpdate strat");
            LogHelper.WriteLog("GiveedUpdate phone"+ phone);
            SimpleResult result = new SimpleResult();
            ICouponService _service = new CouponService();
            IUserInfo _userservice = new UserInfo();
            ICouponTypeService _couponTypeservice = new CouponTypeService();
          
            try
            {
                if (UserAuthorization)
                {
                    
                    var user= _userservice.GetUserByphone( phone);
                    //判断是不是已经领够了2次
                    if (user!=null && user.IsMember)
                    {
                        if (_service.checkCoupon(user.Code))
                        {
                            var couponType =_couponTypeservice.GetCouponTypeByIsGivedType();
                            if (couponType!=null)
                            {
                                Coupon coupon = new Coupon();
                                coupon.CouponCode = Guid.NewGuid().ToString();
                                coupon.UserCode = user.Code;
                                coupon.CouponTypeCode = couponType.CouponTypeCode;
                                coupon.StratTime = DateTime.Now;
                                coupon.VersionEndTime = DateTime.MaxValue;
                                coupon.IsGiveed = false;
                                coupon.CouponUseCode = Str(10, true);
                                //没有添加任务优惠券
                                var re = _service.Insert(coupon);
                                result.Resource = re;
                                result.Status = Result.SUCCEED;
                            }
                            else {
                                result.Resource = "赠送任务已经结束";
                                result.Status = Result.SYSTEM_ERROR;
                            }
                        }
                        else
                        {
                            result.Resource = "已经达到任务奖励上限";
                            
                            result.Status = Result.SYSTEM_ERROR;
                        }
                    } else
                    {
                        result.Resource = "您还没有注册会员";
                        result.Status = Result.SYSTEM_ERROR;
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
                LogHelper.WriteLog("GiveedUpdate=获取完成任务的优惠卷" + phone + ex.Message,ex);
                LogHelper.WriteLog("GiveedUpdate=获取完成任务的优惠卷" + phone + ex.StackTrace,ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GiveedUpdate end");
            return Json(result);

        }
    }
}