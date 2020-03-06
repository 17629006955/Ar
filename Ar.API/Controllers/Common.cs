using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using Ar.Common;
using Ar.Common.tool;
using Ar.Model;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using WxPayAPI;

namespace Ar.API.Controllers
{
    public static  class Common
    {
        public static string Appid { get; set; }
        public static string Secret { get; set; }
        public static string Mchid { get; set; }

        public static MessageReturn SendMessageCode(string pheone)
        {
            LogHelper.WriteLog("短信认证手机号:" + pheone);
            MessageReturn messageReturn = new MessageReturn();
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", ConfigurationManager.AppSettings["AccessKeyID"].ToString(), ConfigurationManager.AppSettings["AccessKeySecret"].ToString() );
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            // request.Protocol = ProtocolType.HTTP;
            request.AddQueryParameters("PhoneNumbers", pheone);
            request.AddQueryParameters("SignName", ConfigurationManager.AppSettings["SignName"].ToString());
            request.AddQueryParameters("TemplateCode", ConfigurationManager.AppSettings["TemplateCode"].ToString());
            MessageNo messageNo = new MessageNo();
             Random rd = new Random();
            int num = rd.Next(100000, 1000000);
            messageNo.code = num.ToString();
            request.AddQueryParameters("TemplateParam", jsonSerialize.Serialize(messageNo));
            LogHelper.WriteLog("短信认证参数:" + jsonSerialize.Serialize(request.QueryParameters));
            try
            {
                CommonResponse response = client.GetCommonResponse(request);
                
                var sendSmsResponse = jsonSerialize.Deserialize<SendSmsResponse>(response.Data);
                if (sendSmsResponse.Code.Equals("OK"))
                {
                    messageReturn.Status = true;
                    messageReturn.Message = num.ToString() ;
                }
            }
            catch (ServerException e)
            {
                messageReturn.Status = false;
                messageReturn.Message = e.Message;
                LogHelper.WriteLog("短信认证错误:" + jsonSerialize.Serialize(messageReturn.Message));
            }
            catch (ClientException e)
            {
                messageReturn.Status = false;
                messageReturn.Message = e.Message;
                LogHelper.WriteLog("短信认证错误:" + jsonSerialize.Serialize(messageReturn.Message));
            }
            return messageReturn;
        }

        public static WxCertification wxCertification(string authorizationCode)
        {
            var url = ConfigurationManager.AppSettings["access_token"].ToString() + "?" + "appid=" + Appid + "&secret=" + Secret + "&code=" + authorizationCode+ "&grant_type=authorization_code";
            LogHelper.WriteLog("微信认证url:" + url);
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, 60000, null, null);
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            var content = reader.ReadToEnd();
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            LogHelper.WriteLog("微信认证接收:" + content);
            var wxAccessToken = jsonSerialize.Deserialize<WxCertification>(content);
            return wxAccessToken;
        }
        public static WxUserInfo wxuserInfo(string access_token ,string openid)
        {
            //access_token=ACCESS_TOKEN&openid=OPENID&lang=zh_CN
            var url = ConfigurationManager.AppSettings["wxuserInfo"].ToString() + "?" + "access_token=" + access_token + "&openid=" + openid + "&lang=zh_CN";
            LogHelper.WriteLog("微信获取用户信息url:" + url);
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, 60000, null, null);
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            var content = reader.ReadToEnd();
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            LogHelper.WriteLog("微信获取用户信息接收:" + content);
            var wxuserInfo = jsonSerialize.Deserialize<WxUserInfo>(content);
            return wxuserInfo;
        }
        public static WxAccessToken wxAccessToken()
        {
            var url = ConfigurationManager.AppSettings["wxurl"].ToString() + "?" + "grant_type=client_credential&appid=" + Appid + "&secret=" + Secret;
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, 60000, null, null);
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            var content = reader.ReadToEnd();
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();

            var wxAccessToken = jsonSerialize.Deserialize<WxAccessToken>(content);
            return wxAccessToken;
        }
        public static string wxJsApiParam { get; set; } //H5调起JS API参数
        public static string  wxPayOrderSomething(string openid,string total_fee,string couponType,string stoeName)
        {
            
            //var url = ConfigurationManager.AppSettings["wxpay"].ToString() + "?" + "grant_type=client_credential&appid=" + Appid + "&secret=" + Secret;

            //检测是否给当前页面传递了相关参数
            if (string.IsNullOrEmpty(openid) || string.IsNullOrEmpty(total_fee))
            {

                return "没有相关参数,用户数据错误";
            }
            else
            {

                ////若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
                JsApiPay jsApiPay = new JsApiPay();
                //jsApiPay.openid = openid;
                //jsApiPay.total_fee = int.Parse(total_fee);

                //JSAPI支付预处理

                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(Appid, Mchid, total_fee, stoeName, couponType,openid);
                //wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                    

                LogHelper.WriteLog("wxJsApiParam:" + wxJsApiParam);
                //在页面上显示订单信息
                return unifiedOrderResult.GetValue("prepay_id")?.ToString();
            }

           
           
        }

    }
}