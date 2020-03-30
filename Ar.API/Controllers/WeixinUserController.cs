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
using System.Text;
using System.Security.Cryptography;
using Ar.Common;
using System.Web.Security;
using System.Net.Http;
using System.Transactions;

namespace Ar.API.Controllers
{
    public class WeixinUserController : ApiController
    {
        ICertificationService certificationService = new CertificationService();
        IUserStoreService userStoreService = new UserStoreService();
        IUserInfo userInfo = new UserInfo();
        IStoreService storeService = new StoreService();

        ///http://localhost:10010//api/WeixinUser/reAccessToken?reAccessToken=18235139350
        [HttpGet]
        public IHttpActionResult reAccessToken(string reAccessToken)
        {
            LogHelper.WriteLog("reAccessToken reAccessToken" + reAccessToken);
            var wxAccessToken = certificationService.ReAccessToken(reAccessToken);
            SimpleResult result = new SimpleResult();
            if (wxAccessToken != null)
            {
                Certification certification = new Certification();
                certification.CertificationCode = wxAccessToken.CertificationCode;
                certification.AccessToken = Guid.NewGuid().ToString();
                certification.CreateTime = DateTime.Now;
                certification.ReAccessToken = Guid.NewGuid().ToString();
                certificationService.UpdateUserCertificationByCertificationCode(certification);
                result.Resource = certification;
                result.Status = Result.SUCCEED;
            }
            LogHelper.WriteLog("reAccessToken result" + Json(result));
            return Json(result);

        }

        ////http://localhost:10010//api/WeixinUser/GetUserInfo?phone=18235139350
        [HttpGet]
        public IHttpActionResult GetUserInfo(string phone)
        {
            
            var user = userInfo.GetUserByphone(phone);
            SimpleResult result = new SimpleResult();
            result.Resource = user;
            LogHelper.WriteLog("GetUserInfo result" + Json(result));
            return Json(result);

        }
        [HttpGet]
        [HttpPost]
        //http://localhost:10010//api/WeixinUser/Login?storeCode=3
        public IHttpActionResult Login(string storeCode)
        {
            LogHelper.WriteLog("Login storeCode" + storeCode);
            SimpleResult result = new SimpleResult();
            try
            {
                LogHelper.WriteLog("Login进来");
                LogHelper.WriteLog("storeCode:" + storeCode);
               
                var store = storeService.GetStore(storeCode);
                if (store != null)
                {

                    Common.Appid = store.appid?.Trim();
                    LogHelper.WriteLog("store.appid " + store.appid);
                    Common.Secret = store.secret?.Trim();
                    LogHelper.WriteLog("store.secret " + store.secret);
                    Common.Mchid = store.mchid?.Trim();
                    LogHelper.WriteLog("store.mchid " + store.mchid);
                    result.Status = Result.SUCCEED;
                    result.Resource = Common.Appid;
                }
                else
                {
                    result.Msg = "登陆失败，没有改店信息，请联系客服配置店面信息。";

                }
            } catch (Exception e)
            {
                result.Msg = e.Message;
            }
            LogHelper.WriteLog("Login result" + Json(result));
            return Json(result);

        }
        ////http://localhost:10010//api/WeixinUser/WxCode?phone=18235139350
        [HttpGet]
        public IHttpActionResult WxCode(string Code, string state)
        {

            WxCode wxCode = new WxCode();
            wxCode.code = Code;
            wxCode.state = state;
            SimpleResult result = new SimpleResult();
            result.Status = Result.SUCCEED;
            result.Resource = wxCode;
            return Json(result);

        }

        ///// <summary>
        ///// 微信公众号服务器地址验证
        ///// </summary>
        ///// <param name="signature">微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。</param>
        ///// <param name="timestamp">时间戳</param>
        ///// <param name="nonce">随机数</param>
        ///// <param name="echostr">随机字符串</param>
        ///// <returns>成功时，验证通过</returns> 
        //http://rk825j.natappfree.cc/api/WeixinUser/CheckSignature

        [HttpGet]
        [HttpPost]
        public void CheckSignature()
        {
            LogHelper.WriteLog("微信进来");
            var signature = HttpContext.Current.Request["signature"];// RequestHelper.GetString("app_key");
            LogHelper.WriteLog("signature：" + signature);
            //app_secret = RequestHelper.GetString("app_secret");   //app_secret仅作加密使用，不在提交中使用
            var timestamp = HttpContext.Current.Request["timestamp"];//RequestHelper.GetString("sign");
            LogHelper.WriteLog("timestamp：" + timestamp);
            var nonce = HttpContext.Current.Request["nonce"];//RequestHelper.GetString("timestamp");
            LogHelper.WriteLog("nonce：" + nonce);
            var echostr = HttpContext.Current.Request["echostr"]; //RequestHelper.GetString("token");  
            LogHelper.WriteLog("echostr：" + echostr);
            HttpContext.Current.Response.Write(echostr);
            HttpContext.Current.Response.End();
          

        }
        [HttpGet]
        [HttpPost]
        //http://localhost:10010//api/WeixinUser/Wxconfig?storeCode=4&url=https://aidezahuopu.com/ar/a
        public IHttpActionResult Wxconfig(string storeCode,string url)
        {
            SimpleResult result = new SimpleResult();
            try
            {
                var store = storeService.GetStore(storeCode);
                if (store != null)
                {
                    Common.Appid = store.appid?.Trim();
                    LogHelper.WriteLog("store.appid " + store.appid);
                    Common.Secret = store.secret?.Trim();
                    LogHelper.WriteLog("store.secret " + store.secret);
                    Common.Mchid = store.mchid?.Trim();
                    LogHelper.WriteLog("store.mchid " + store.mchid);
                    LogHelper.WriteLog("微信进来");
                    var wxc = Common.GetWxConfig(store, url);
                    if (wxc != null)
                    {
                        result.Status = Result.SUCCEED;
                        result.Resource = wxc;
                    }
                    else
                    {
                        result.Status = Result.USER_AUTH_ERROR;
                        result.Resource = "获取配置失败重新获取";
                    }

                }
                else
                {
                    result.Status = Result.SYSTEM_ERROR;
                    result.Resource = "店铺没有配置";
                }


                
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Wxconfig", ex);
                result.Status = Result.FAILURE;
                    result.Msg = ex.Message;
              

            }
            LogHelper.WriteLog("Wxconfig result" + Json(result));
            return Json(result);

        }

        [HttpGet]
        [HttpPost]
        //http://localhost:10010//api/WeixinUser/Wxconfig?storeCode=4&url=https://aidezahuopu.com/ar/a
        public IHttpActionResult WxCardExt(string storeCode, string userCode)
        {
            SimpleResult result = new SimpleResult();
            try
            {
                var store = storeService.GetStore(storeCode);
                if (store != null)
                {
                    Common.Appid = store.appid?.Trim();
                    LogHelper.WriteLog("store.appid " + store.appid);
                    Common.Secret = store.secret?.Trim();
                    LogHelper.WriteLog("store.secret " + store.secret);
                    Common.Mchid = store.mchid?.Trim();
                    LogHelper.WriteLog("store.mchid " + store.mchid);
                    LogHelper.WriteLog("微信进来");
                    var wxc = Common.GetCardExt(store, userCode);
                    if (wxc != null)
                    {
                        var use = userInfo.GetUserByCode(userCode);
                        if (use != null)
                        {
                            //写入会员号
                            var count = userInfo.UpdateReferenceNumber(userCode, wxc.cardExt.code);
                        }
                        result.Status = Result.SUCCEED;
                        result.Resource = wxc;
                    }
                    else
                    {
                        result.Status = Result.USER_AUTH_ERROR;
                        result.Resource = "获取配置失败重新获取";
                    }

                }
                else
                {
                    result.Status = Result.SYSTEM_ERROR;
                    result.Resource = "店铺没有配置";
                }



            }
            catch (Exception ex)
            {

                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
                LogHelper.WriteLog("WxCardExt", ex);

            }
            LogHelper.WriteLog("WxCardExt result" + Json(result));
            return Json(result);

        }

        //http://localhost:10010//api/WeixinUser/Getaccess_token?authorizationCode=18235139350&membershipCardStore=3
        /// <summary>
        /// 进来新判断是否有认证有的话直接返回没有的话直接
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Getaccess_token(string authorizationCode,string membershipCardStore)
        {
            SimpleResult result = new SimpleResult();
            try
            {
                var store = storeService.GetStore(membershipCardStore);
                if (store != null)
                {
                    LogHelper.WriteLog("微信authorizationCode:" + authorizationCode+ "membershipCardStore:"+ membershipCardStore);
                    var wxAccessToken = Common.wxCertification(authorizationCode, store);
                    if (wxAccessToken != null && wxAccessToken.access_token != null)
                    {
                        Certification certification = new Certification();
                        //①请求判断是否用户已经存在认证②存在了更新
                        LogHelper.WriteLog("微信openid:" + wxAccessToken.openid + "membershipCardStore:" + membershipCardStore);
                        if (certificationService.CheckCertification(wxAccessToken.openid))
                        {
                            certification.CertificationCode = Guid.NewGuid().ToString();
                            certification.OpenID = wxAccessToken.openid;
                            certification.AccessToken = Guid.NewGuid().ToString();
                            certification.CreateTime = DateTime.Now;
                            certification.ReAccessToken = Guid.NewGuid().ToString();
                            certificationService.UpdateUserCertification(certification);
                        }
                        else
                        {
                            //③不存在 微信authorizationCode转化自己的authorizationCode
                            certification.CertificationCode = Guid.NewGuid().ToString();
                            certification.OpenID = wxAccessToken.openid;
                            certification.CreateTime = DateTime.Now;
                            certification.AccessToken = Guid.NewGuid().ToString();
                            certification.ReAccessToken = Guid.NewGuid().ToString();
                            //3.1 创建认证信息
                            certificationService.CreateUserCertification(certification);
                        }
                        IUserTaskService _userTaskService = new UserTaskService();
                        //3.2用OpenID检查用户 没有的话创建用户信息待写
                        //using (var scope = new TransactionScope())//创建事务
                        //{
                        User user = new User();
                        var userStore = userStoreService.GetUserStoreby(wxAccessToken.openid);
                        if (userStore != null)
                        {
                            user = userInfo.GetUserByCode(userStore.UserCode);
                            if (_userTaskService.GetUserTaskList(user.Code).Count() != 2)
                            {
                                _userTaskService.InsertUserTask(user.Code, "1");
                                _userTaskService.InsertUserTask(user.Code, "2");
                            }
                        }
                        // 用OpenID获取用户信息然后创建用户信息
                        else
                        {


                            var wxuserinfo = Common.wxuserInfo(wxAccessToken.access_token, wxAccessToken.openid);
                            if (wxuserinfo != null)
                            {

                                user.Code = Guid.NewGuid().ToString();
                                if (wxuserinfo.sex == 2)
                                {
                                    user.Sex = true;
                                }
                                else
                                {
                                    user.Sex = false;
                                }

                                user.UserIamgeUrl = wxuserinfo.headimgurl;
                                user.UserName = wxuserinfo.nickname;
                                user.CreateTime = DateTime.Now;
                                userInfo.CreateUser(user);
                                UserStore userStorew = new UserStore();
                                userStorew.OpenID = wxAccessToken.openid;
                                userStorew.UserStoreCode = Guid.NewGuid().ToString();
                                userStorew.UserCode = user.Code;
                                userStorew.MembershipCardStore = membershipCardStore;
                                userStoreService.CreateUserStore(userStorew);

                                _userTaskService.InsertUserTask(user.Code, "1");
                                _userTaskService.InsertUserTask(user.Code, "2");
                            }
                            else
                            {
                                result.Msg = "请使用微信获取用户信息失败";
                            }
                        }

                        result.Status = Result.SUCCEED;
                        FristModel fristModel = new FristModel();
                        fristModel.certification = certification;
                        fristModel.userInfo = user;
                        result.Resource = fristModel;
                        result.Msg = "请使用AccessToken请求认证";
                        //    scope.Complete();//这是最后提交事务
                        //}
                    }
                    else
                    {
                        result.Msg = "认证失败";
                        result.Msg = "请求微信认证失败重新获取Code";
                    }
                }
                else
                {
                    result.Msg = "店铺没有配置";
                }
            }
            catch (Exception e)
            {
                result.Msg = e.Message+e.StackTrace;
                LogHelper.WriteLog("Getaccess_token " , e);
            }
            LogHelper.WriteLog("Getaccess_token result" + Json(result));
            return Json(result);

        }

    }
}