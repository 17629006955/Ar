using Ar.Model.BaseResult;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ar.API.Controllers
{
    public class WxTokenController : Controller
    {

        //定义Token，与微信公众平台保持一致
        public string Signin(string SigninCode)
        {

            SimpleResult result = new SimpleResult();
            result.Status = Result.SUCCEED;
            return "fsf";
        }

        //微信验证端口
        public ActionResult WeiXin()
        {
            string Token = ConfigurationManager.AppSettings["token"].ToString();
            string echoStr = Request.QueryString["echoStr"];
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            if (CheckSignature(Token, signature, timestamp, nonce) && !string.IsNullOrEmpty(echoStr))
            {
                Response.Write(echoStr);
                Response.End();
            }
            return View();

        }

        //微信密码验证
        private static bool CheckSignature(string Token, string signature, string timestamp, string nonce)
        {
            string[] arrTmp = { Token, timestamp, nonce };
            Array.Sort(arrTmp);
            string tmpStr = string.Join("", arrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            if (tmpStr != null)
            {
                tmpStr = tmpStr.ToLower();
                return tmpStr == signature;
            }
            return false;

        }

    }
}