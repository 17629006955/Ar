using Ar.Common;
using Ar.Model.BaseResult;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using WxPayAPI;

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
        public ActionResult ProcessRequest()
        {
            LogHelper.WriteLog("ProcessRequest " );
            //接收从微信后台POST过来的数据
            System.IO.Stream s = Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();

            Log.Info(this.GetType().ToString(), "Receive data from WeChat : " + builder.ToString());

            //转换数据格式并验证签名
            WxPayData data = new WxPayData();
            try
            {
                data.FromXml(builder.ToString());
            }
            catch (WxPayException ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                Log.Error(this.GetType().ToString(), "Sign check error : " + res.ToXml());
               Response.Write(res.ToXml());
                Response.End();

            }
            var notifyData = data;
            Log.Info(this.GetType().ToString(), "Check sign success");
      
            //检查openid和product_id是否返回
            if (!notifyData.IsSet("openid") || !notifyData.IsSet("product_id"))
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "回调数据异常");
                Log.Info(this.GetType().ToString(), "The data WeChat post is error : " + res.ToXml());
                Response.Write(res.ToXml());
                Response.End();
            }
            if (notifyData.IsSet("return_code"))
            {
                //统一下单成功,则返回成功结果给微信支付后台
                WxPayData datareturn = new WxPayData();
                datareturn.SetValue("return_code", "SUCCESS");
                datareturn.SetValue("return_msg", "OK");
                Log.Info(this.GetType().ToString(), "UnifiedOrder success , send data to WeChat : " + datareturn.ToXml());
                Response.Write(datareturn.ToXml());
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