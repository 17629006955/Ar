using Ar.API.Controllers.BaseContolles;
using Ar.Common;
using Ar.IServices;
using Ar.Model.BaseResult;
using Ar.Services;
using AR.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Ar.API.Controllers
{
    public class MessageController : BaseApiController
    {
        IVerificationService verificationService = new VerificationService();
        IUserInfo userInfo = new UserInfo();
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        //http://localhost:10010//api/Message/SendMessageCode?phone=18235139350
        public IHttpActionResult SendMessageCode(string phone)
        {
            LogHelper.WriteLog("SendMessageCode ", phone);
            SimpleResult result = new SimpleResult();
            if (UserAuthorization)
            {
                if (ConfigurationManager.AppSettings["isSendMessage"] != null && ConfigurationManager.AppSettings["isSendMessage"].ToString() == "true")
                {
                    var sendMessageResult = Common.SendMessageCode(phone);
                    if (sendMessageResult != null && sendMessageResult.Status)
                    {
                        //写入到手机号和和数据库
                        verificationService.Delete(phone);
                        Verification verification = new Verification();
                        verification.code = Guid.NewGuid().ToString();
                        verification.VerificationCode = sendMessageResult.Message;
                        verification.Phone = phone;
                        verificationService.CreateVerification(verification);
                        result.Resource = sendMessageResult.Message;
                        result.Status = Result.SUCCEED;
                    }
                    else
                    {
                        result.Msg = "验证码没有发送成功";
                        result.Status = Result.SYSTEM_ERROR;
                    }

                }
                else
                {
                    //写入到手机号和和数据库
                    verificationService.Delete(phone);
                    Verification verification = new Verification();
                    verification.code = Guid.NewGuid().ToString();
                    Random rd = new Random();
                    int num = rd.Next(100000, 1000000);
                    verification.VerificationCode = num.ToString();
                    verification.Phone = phone;
                    verificationService.CreateVerification(verification);
                    result.Resource = num;
                    result.Status = Result.SUCCEED;
                }
            }
            else
            {
                result.Status = ResultType;
                result.Resource = ReAccessToken;
                result.Msg = TokenMessage;
            }

            LogHelper.WriteLog("SendMessageCode result" + Json(result));
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
        /// 绑定手机号
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="verificationCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>

        [HttpPost]
        //http://localhost:10010//api/Message/BangMessageCode?phone=18235139350&verificationCode=232232&userCode=121ewe&birthday='yyyy-mm-dd'
        public IHttpActionResult BangMessageCode(string phone, string verificationCode, string userCode, string storeCode, string birthday, string recommendedPhone = null)
        {
            IStoreService storeService = new StoreService();
            ICouponService _service = new CouponService();
            IUserInfo _userservice = new UserInfo();
            ICouponTypeService _couponTypeservice = new CouponTypeService();
            IUserTaskService _userTaskservice = new UserTaskService();
            SimpleResult result = new SimpleResult();
            try
            {
                if (UserAuthorization)
            {

                if (verificationService.CheckVerification(phone, verificationCode))
                {
                    DateTime birthdaydate = new DateTime();
                    if (DateTime.TryParse(birthday, out birthdaydate))
                    {
                        var use = userInfo.GetUserByCode(userCode);
                            if (use != null)
                            {
                                if (!use.IsMember && string.IsNullOrEmpty(use.ReferenceNumber))
                                {
                                    var store = storeService.GetStore(storeCode);
                                    if (store != null)
                                    {
                                        var wxc = Common.GetCardExt(store, userCode);
                                        if (wxc != null)
                                        {
                                            //写入到手机号和和数据库
                                            var count = userInfo.UpdateByPhone(userCode, phone, birthdaydate, wxc.cardExt.code, recommendedPhone);
                                            if (count > 0)
                                            {
                                                result.Status = Result.SUCCEED;
                                                LogHelper.WriteLog("BangMessageCode :" + result.Status);
                                                LogHelper.WriteLog("wxc :" + wxc);
                                                result.Resource = wxc;
                                            }
                                            else
                                            {
                                                result.Status = Result.SYSTEM_ERROR;
                                                result.Msg = "获取配置失败重新获取";
                                            }
                                        }
                                        else
                                        {
                                            result.Status = Result.USER_AUTH_ERROR;
                                            result.Msg = "获取配置失败重新获取";
                                        }
                                    }
                                    else
                                    {
                                        result.Status = Result.SYSTEM_ERROR;
                                        result.Msg = "店铺不存在";
                                    }

                                }
                                else
                                {
                                    var count = userInfo.UpdateByuserCodePhone(userCode, phone, birthdaydate);
                                    if (count > 0)
                                    {
                                        result.Status = Result.SUCCEED;
                                    }
                                    else
                                    {
                                        result.Status = Result.SYSTEM_ERROR;
                                        result.Msg = "当前用户绑定手机号失败";
                                    }
                                }
                            }
                            else
                            {
                                result.Status = Result.SYSTEM_ERROR;
                                result.Msg = "当前用户不存在";
                            }
                    }
                    else
                    {
                        result.Status = Result.SYSTEM_ERROR;
                        result.Msg = "选择生日有误";
                    }
                }
                else
                {
                    result.Status = Result.SYSTEM_ERROR;
                    result.Msg = "验证码错误或者已经过期，请重新获取验证码。";
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
                LogHelper.WriteLog("BangMessageCode：", ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("BangMessageCode result" + Json(result));

            return Json(result);

        }
        [HttpPost]
        [HttpGet]
        //http://localhost:10010//api/Message/BangMessageOk?userCode=18235139350
        public IHttpActionResult BangMessageOk(string userCode)
        {
            LogHelper.WriteLog("BangMessageOk :" + userCode);

            ICouponService _service = new CouponService();
            IUserInfo _userservice = new UserInfo();
            ICouponTypeService _couponTypeservice = new CouponTypeService();
            IUserTaskService _userTaskservice = new UserTaskService();
            SimpleResult result = new SimpleResult();
            try
            {
                if (UserAuthorization)
                {
                    var user = userInfo.GetUserByCode(userCode);
                    if (user != null)
                    {
                        //写入到手机号和和数据库
                        var count = userInfo.UpdateIsMemberByuserCode(userCode);
                        if (count > 0)
                        {
                            if (!string.IsNullOrEmpty(user.RecommendedPhone))
                            {
                                var recouser = _userservice.GetUserByphone(user.RecommendedPhone);
                                //判断是不是已经领够了2次
                                if (recouser != null && recouser.IsMember)
                                {
                                    if (_service.checkCoupon(recouser.Code))
                                    {
                                        var couponType = _couponTypeservice.GetCouponTypeByIsGivedType();
                                        if (couponType != null)
                                        {
                                            Coupon coupon = new Coupon();
                                            coupon.CouponCode = Guid.NewGuid().ToString();
                                            coupon.UserCode = recouser.Code;
                                            coupon.CouponTypeCode = couponType.CouponTypeCode;
                                            coupon.StratTime = DateTime.Now;
                                            coupon.VersionEndTime = DateTime.MaxValue;
                                            coupon.IsGiveed = true;
                                            coupon.CouponUseCode = Str(10, true);
                                            //没有添加任务优惠券
                                            var re = _service.Insert(coupon);
                                            //更改任务状态
                                            var userTask = _userTaskservice.GetUserTaskList(recouser.Code);
                                            var ut = userTask.Where(u => u.TaskCode == "2").FirstOrDefault();
                                            ut.IsComplete = true;
                                            _userTaskservice.UpdateUserTask(ut.UserTaskCode, 1);
                                            result.Resource = re;
                                            result.Status = Result.SUCCEED;

                                        }
                                        else
                                        {
                                            result.Resource = "好友赠送任务已经结束";
                                            result.Status = Result.SYSTEM_ERROR;
                                        }
                                    }
                                    else
                                    {
                                        result.Resource = "好友已经达到任务奖励上限";

                                        result.Status = Result.SYSTEM_ERROR;
                                    }
                                }
                                else
                                {
                                    result.Resource = "您还没有注册会员";
                                    result.Status = Result.SYSTEM_ERROR;
                                }
                            }
                            if (!user.IsMember)
                            {
                                //
                                //添加赠送本人
                                if (_service.checkCoupon(userCode))
                                {
                                    var couponType = _couponTypeservice.GetCouponTypeByIsGivedType();
                                    if (couponType != null)
                                    {
                                        Coupon coupon = new Coupon();
                                        coupon.CouponCode = Guid.NewGuid().ToString();
                                        coupon.UserCode = userCode;
                                        coupon.CouponTypeCode = couponType.CouponTypeCode;
                                        coupon.StratTime = DateTime.Now;
                                        coupon.VersionEndTime = DateTime.MaxValue;
                                        coupon.IsGiveed = true;
                                        coupon.CouponUseCode = Str(10, true);
                                        //没有添加任务优惠券
                                        var re = _service.Insert(coupon);
                                        //更改任务状态
                                        //更改任务状态
                                        var userTask = _userTaskservice.GetUserTaskList(userCode);
                                        var ut = userTask.Where(u => u.TaskCode == "1").FirstOrDefault();
                                        ut.IsComplete = true;
                                        _userTaskservice.UpdateUserTask(ut.UserTaskCode, 1);
                                        result.Resource = re;
                                        result.Status = Result.SUCCEED;
                                    }

                                }
                            }

                            result.Resource = count;
                            result.Status = Result.SUCCEED;
                        }
                        else
                        {
                            result.Status = Result.SYSTEM_ERROR;
                            result.Resource = "添加没有成功，请重试。";
                        }
                    }
                    else
                    {
                        result.Status = Result.SYSTEM_ERROR;
                        result.Resource = "当前用户不存在";
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
                LogHelper.WriteLog("BangMessageOk：" + ex.Message, ex);
                LogHelper.WriteLog("BangMessageOk：" + ex.StackTrace, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("BangMessageOk result" + Json(result));
            return Json(result);

        }
    }
}