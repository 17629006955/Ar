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
using System.Security.Cryptography;
using System.Text;
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
        public static Wxticket wxticket(string access_token)
        {
            var url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token="+ access_token + "&type=jsapi";
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, 60000, null, null);
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            var content = reader.ReadToEnd();
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();

            var Wxticket = jsonSerialize.Deserialize<Wxticket>(content);
            return Wxticket;
        }
        public static WxConfig GetWxConfig(string url)
        {
            WxConfig wxConfig = new WxConfig();
            var accessToken = wxAccessToken();
            if (accessToken != null)
            {
                if (!string.IsNullOrEmpty(accessToken.access_token))
                {
                    var wt = wxticket(accessToken.access_token);
                    if (!string.IsNullOrEmpty(wt?.ticket))
                    {
                        url = url.Split('#')[0];
                        var jsapi_ticket = wt?.ticket;
                        wxConfig.appId = Appid;
                        wxConfig.debug = true;
                        wxConfig.nonceStr = WxPayApi.GenerateNonceStr();
                        wxConfig.timestamp = WxPayApi.GenerateTimeStamp();
                        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["jsApiList"].ToString()))
                        {
                            var st = ConfigurationManager.AppSettings["jsApiList"].ToString().Split(',');
                            foreach (var item in st)
                            {
                                wxConfig.jsApiList.Add(item);
                            }

                        }

                        wxConfig.signature = signature(jsapi_ticket, wxConfig.nonceStr, wxConfig.timestamp, url);
                    }
                }
                else

                {
                    return null;
                }

            }
            return wxConfig;
        }
        public static string signature(string jsapi_ticket, string noncestr, string timestamp, string url)
        {
            var string1= "jsapi_ticket ="+ jsapi_ticket + "&noncestr="+ noncestr + "&timestamp"+ timestamp + "&url=" + url;
            return SHA1(string1, Encoding.UTF8);
        }
        
        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <param name="encode">指定加密编码</param>
        /// <returns>返回40位大写字符串</returns>
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }


        public static string wxJsApiParam { get; set; } //H5调起JS API参数
        public static Wxprepay wxPayOrderSomething(string openid,string total_fee,string couponType,string stoeName)
        {

            //var url = ConfigurationManager.AppSettings["wxpay"].ToString() + "?" + "grant_type=client_credential&appid=" + Appid + "&secret=" + Secret;
            Wxprepay wxprepay = new Wxprepay();
            //检测是否给当前页面传递了相关参数
            if (string.IsNullOrEmpty(openid) || string.IsNullOrEmpty(total_fee))
            {

                return null;
            }
            else
            {

                ////若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
                JsApiPay jsApiPay = new JsApiPay();
                //jsApiPay.openid = openid;
                //jsApiPay.total_fee = int.Parse(total_fee);

                //JSAPI支付预处理
                var prepayid = WxPayApi.GenerateOutTradeNo();
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(Appid, Mchid, total_fee, stoeName, couponType,openid, prepayid);
                wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                    

                LogHelper.WriteLog("wxJsApiParam:" + wxJsApiParam);
                //在页面上显示订单信息
                wxprepay.wxJsApiParam = wxJsApiParam;
                wxprepay.prepayid = prepayid;
                return wxprepay;
            }

           
           
        }
        public static string wxPayOrderQuery(string out_trade_no)
        {

            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(out_trade_no))//如果微信订单号存在，则以微信订单号为准
            {
                data.SetValue("transaction_id", out_trade_no);
            }
            else
            {
                throw new WxPayException("订单查询接口中，out_trade_no填一个！");
            }
 
            WxPayData result = WxPayApi.OrderQuery(data, Appid, Mchid);//提交订单查询请求给API，接收返回数据
            if (result.IsSet("result_code") && result.GetValue("result_code").ToString() == "SUCCESS" && result.IsSet("trade_state") && result.GetValue("trade_state").ToString() == "SUCCESS")
            {
                return result.GetValue("time_end").ToString() ;
            }else
                { return null; }

        }

        
       
    }
}