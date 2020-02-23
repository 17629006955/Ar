using Ar.API.Controllers.BaseContolles;
using Ar.IServices;
using Ar.Model.BaseResult;
using Ar.Services;
using AR.Model;
using System;
using System.Collections.Generic;
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
            SimpleResult result = new SimpleResult();
            if (UserAuthorization)
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
            }
            else
            {
                result.Status = ResultType;
                result.Resource = ReAccessToken;
                result.Msg = TokenMessage;
            }
           

            return Json(result);

        }
        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="verificationCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        //http://localhost:10010//api/Message/BangMessageCode?phone=18235139350&SendMessageCode=232232&userCode=121ewe
        public IHttpActionResult BangMessageCode(string phone,string verificationCode, string userCode)
        {
            SimpleResult result = new SimpleResult();
            if (UserAuthorization)
            {    
            if (verificationService.CheckVerification(phone, verificationCode))
            {
                //写入到手机号和和数据库
                var count = userInfo.UpdateByPhone( userCode, phone);
                result.Resource = count;
                result.Status = Result.SUCCEED;
            }
            }
            else
            {
                result.Status = ResultType;
                result.Resource = ReAccessToken;
                result.Msg = TokenMessage;
            }


            return Json(result);

        }

    }
}