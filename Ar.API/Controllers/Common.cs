using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using Ar.Common;
using Ar.Common.tool;
using Ar.IServices;
using Ar.Model;
using Ar.Services;
using AR.Model;
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

        public static WxCertification wxCertification(string authorizationCode, Store store)
        {
            var url = ConfigurationManager.AppSettings["access_token"].ToString() + "?" + "appid=" + store.appid.Trim() + "&secret=" + store.secret + "&code=" + authorizationCode + "&grant_type=authorization_code";
            LogHelper.WriteLog("微信认证url:" + url);
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, 60000, null, null);
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            var content = reader.ReadToEnd();
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            LogHelper.WriteLog("微信认证接收:" + content);
            var wxAccessToken = jsonSerialize.Deserialize<WxCertification>(content);
            if (wxAccessToken!=null && !string.IsNullOrEmpty(wxAccessToken.access_token))
            {
                var wt = wxticket(wxAccessToken.access_token);
                if (wt != null)
                {
                    if (!string.IsNullOrEmpty(wt?.ticket))
                    {
                        IStoreService _stoeservice = new StoreService();
                        store.accessToken = wxAccessToken.access_token;
                        store.jsapi_ticket = wt?.ticket;
                        store.accessTokenCreateTime = DateTime.Now;
                        LogHelper.WriteLog("store.accessToken:" + store.accessToken);
                        LogHelper.WriteLog("store.jsapi_ticket:" + store.jsapi_ticket);
                        LogHelper.WriteLog("store.accessTokenCreateTime:" + store.accessTokenCreateTime);
                        _stoeservice.UpdateStoreaccessToken(store);
                    }
                }
                var wtapiticket = apiticket(wxAccessToken.access_token);
                if (wtapiticket != null)
                {
                    if (!string.IsNullOrEmpty(wt?.ticket))
                    {
                        IStoreService _stoeservice = new StoreService();
                        store.accessToken = wxAccessToken.access_token;
                        store.api_ticket = wt?.ticket;
                        store.accessTokenCreateTime = DateTime.Now;

                        LogHelper.WriteLog("store.api_ticket:" + store.api_ticket);

                        _stoeservice.UpdateStoreaccessToken(store);


                    }
                }
            
            }
                return wxAccessToken;
        }
        public static WxUserInfo wxuserInfo(string access_token ,string openid)
        {
            //  wxuserInfo
            //access_token=ACCESS_TOKEN&openid=OPENID&lang=zh_CN
            var url = ConfigurationManager.AppSettings["wxuserInfo"].ToString()  + "=" + access_token + "&openid=" + openid + "&lang=zh_CN";
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
        public static WxAccessToken wxAccessToken(string Appid,string Secret)
        {
            LogHelper.WriteLog("Appid"+ Appid);
            LogHelper.WriteLog("Secret" + Secret);
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
            LogHelper.WriteLog("wxticket.accessToken:" + access_token);
            var url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token="+ access_token + "&type=jsapi";
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, 60000, null, null);
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            var content = reader.ReadToEnd();
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();

            var Wxticket = jsonSerialize.Deserialize<Wxticket>(content);
            return Wxticket;
        }
        public static Wxticket apiticket(string access_token)
        {
            LogHelper.WriteLog("wxticket.accessToken:" + access_token);
            var url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + access_token + "&type=wx_card";
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, 60000, null, null);
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            var content = reader.ReadToEnd();
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();

            var Wxticket = jsonSerialize.Deserialize<Wxticket>(content);
            return Wxticket;
        }
        public static WxConfig GetWxConfig(Store store ,string url)
        {
            WxConfig wxConfig = new WxConfig();
            LogHelper.WriteLog("store.appid:" + store.appid.Trim());
            LogHelper.WriteLog("store.accessToken:" + store.accessToken);
            LogHelper.WriteLog("store.jsapi_ticket:" + store.jsapi_ticket);
            LogHelper.WriteLog("store.accessTokenCreateTime:" + store.accessTokenCreateTime);
            if (store.accessToken!=null && store.jsapi_ticket != null  && store.accessTokenCreateTime>DateTime.Now.AddHours(-1))
            {
                url = url.Split('#')[0];
                var jsapi_ticket = store.jsapi_ticket;
                wxConfig.appId = store.appid.Trim();
                wxConfig.debug = true;
                wxConfig.nonceStr = WxPayApi.GenerateNonceStr();
                wxConfig.timestamp = WxPayApi.GenerateTimeStamp();
                wxConfig.jsApiList = new List<string>();
                wxConfig.signature = signature(jsapi_ticket, wxConfig.nonceStr, wxConfig.timestamp, url);
            }
            else
            { 
            var accessToken = wxAccessToken(store.appid.Trim(), store.secret.Trim());
                if (accessToken != null)
                {
                    if (!string.IsNullOrEmpty(accessToken.access_token))
                    {
                        var wt = wxticket(accessToken.access_token);
                        if (wt != null)
                        {
                            if (!string.IsNullOrEmpty(wt?.ticket))
                            {
                                IStoreService _stoeservice = new StoreService();
                                store.accessToken = accessToken.access_token;
                                store.jsapi_ticket = wt?.ticket;
                                store.accessTokenCreateTime = DateTime.Now;
                                LogHelper.WriteLog("store.accessToken:" + store.accessToken);
                                LogHelper.WriteLog("store.jsapi_ticket:" + store.jsapi_ticket);
                                LogHelper.WriteLog("store.accessTokenCreateTime:" + store.accessTokenCreateTime);
                                var api_ticketwx = apiticket(accessToken.access_token);
                                store.api_ticket = api_ticketwx?.ticket;
                                LogHelper.WriteLog("store.api_ticket:" + store.api_ticket);
                                _stoeservice.UpdateStoreaccessToken(store);
                                url = url.Split('#')[0];
                                var jsapi_ticket = wt?.ticket;
                                wxConfig.appId = store.appid.Trim();
                                wxConfig.debug = true;
                                wxConfig.nonceStr = WxPayApi.GenerateNonceStr();
                                wxConfig.timestamp = WxPayApi.GenerateTimeStamp();
                                wxConfig.jsApiList = new List<string>();


                                wxConfig.signature = signature(jsapi_ticket, wxConfig.nonceStr, wxConfig.timestamp, url);
                            }
                            else

                            {
                                return null;
                            }
                        }
                        else

                        {
                            return null;
                        }
                    }
                    else

                    {
                        return null;
                    }
                }
                else

                {
                    return null;
                }

            }
            return wxConfig;
        }
        public static addCard GetCardExt(Store store,string userCode)
        {
            IUserStoreService userStoreService = new UserStoreService();

            var cardId = ConfigurationManager.AppSettings["cardId"].ToString();
            addCard addCard = new addCard();
            addCard.cardId = cardId;
            CardExt cardExt = new CardExt();
            LogHelper.WriteLog("store.appid:" + store.appid.Trim());
            LogHelper.WriteLog("store.accessToken:" + store.accessToken);
            LogHelper.WriteLog("store.api_ticket:" + store.api_ticket);
            LogHelper.WriteLog("store.accessTokenCreateTime:" + store.accessTokenCreateTime);
            var userStore = userStoreService.GetUserStorebyUserCodestoreCode(userCode,store.StoreCode);
            if (!string.IsNullOrEmpty(store.accessToken ) && !string.IsNullOrEmpty(store.api_ticket) && store.accessTokenCreateTime > DateTime.Now.AddHours(-1))
            {
                
                var api_ticket = store.api_ticket;
                cardExt.code = WxPayApi.GenerateNonceStr();
                cardExt.openid = userStore.OpenID;
                cardExt.nonce_str = WxPayApi.GenerateNonceStr();
                cardExt.timestamp = WxPayApi.GenerateTimeStamp();
                cardExt.signature = GetSignature(api_ticket, cardExt.nonce_str, cardExt.timestamp, cardExt.code, cardExt.openid, cardId);
                LogHelper.WriteLog("api_ticket :" + api_ticket);
                LogHelper.WriteLog("nonce_str :" + cardExt.nonce_str);
                LogHelper.WriteLog("timestamp :" + cardExt.timestamp);
                LogHelper.WriteLog("code :" + cardExt.code);
                LogHelper.WriteLog("openid :" + cardExt.openid);
                LogHelper.WriteLog("cardId :" + cardId);
            }
            else
            {
                var accessToken = wxAccessToken(store.appid.Trim(), store.secret.Trim());
                if (accessToken != null)
                {
                    if (!string.IsNullOrEmpty(accessToken.access_token))
                    {
                        var wt = apiticket(accessToken.access_token);
                        if (wt != null)
                        {
                            if (!string.IsNullOrEmpty(wt?.ticket))
                            {
                                IStoreService _stoeservice = new StoreService();
                                store.accessToken = accessToken.access_token;
                                store.api_ticket = wt?.ticket;
                                store.accessTokenCreateTime = DateTime.Now;
                                LogHelper.WriteLog("store.accessToken:" + store.accessToken);
                                LogHelper.WriteLog("store.api_ticket:" + store.api_ticket);
                                LogHelper.WriteLog("store.accessTokenCreateTime:" + store.accessTokenCreateTime);
                                _stoeservice.UpdateStoreaccessToken(store);

                                var api_ticket = store.api_ticket;
                                cardExt.code = WxPayApi.GenerateNonceStr();
                                cardExt.openid = userStore.OpenID;
                                cardExt.nonce_str = WxPayApi.GenerateNonceStr();
                                cardExt.timestamp = WxPayApi.GenerateTimeStamp();
                                cardExt.signature = GetSignature(api_ticket, cardExt.nonce_str, cardExt.timestamp, cardExt.code, cardExt.openid, ConfigurationManager.AppSettings["Company"].ToString());
                                LogHelper.WriteLog("api_ticket :" + api_ticket);
                                LogHelper.WriteLog("nonce_str :" + cardExt.nonce_str);
                                LogHelper.WriteLog("timestamp :" + cardExt.timestamp);
                                LogHelper.WriteLog("code :" + cardExt.code);
                                LogHelper.WriteLog("openid :" + cardExt.openid);
                                LogHelper.WriteLog("cardId :" + cardId);
                            }
                            else

                            {
                                return null;
                            }
                        }
                        else

                        {
                            return null;
                        }
                    }
                    else

                    {
                        return null;
                    }
                }
                else

                {
                    return null;
                }

            }
            addCard.cardExt = cardExt;
            return addCard;
        }
        public static string getcardExtcode(string phone)
            {
            return WxPayApi.GenerateNonceStr() + phone;
            }


        public static string signature(string jsapi_ticket, string noncestr, string timestamp, string url)
        {
            var string1= "jsapi_ticket="+ jsapi_ticket + "&noncestr="+ noncestr + "&timestamp="+ timestamp + "&url=" + url;
            return Sha1(string1);
        }
        public static string GetSignature(string api_ticket, string noncestr, string timestamp, string code, string openId, string cardId)
        {
            var string1Builder = new StringBuilder();

            var paramList = new List<string>();
            paramList.Add(api_ticket);
            paramList.Add(cardId);
            paramList.Add(code);
            paramList.Add(noncestr);
            paramList.Add(openId);
            paramList.Add(timestamp.ToString());
            paramList.Sort(string.CompareOrdinal);

            var strParam = new StringBuilder();

            foreach (var item in paramList)
                strParam.Append(item);
            return Sha1(strParam.ToString());
               
        }


        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <param name="encode">指定加密编码</param>
        /// <returns>返回40位大写字符串</returns>
        public static string Sha1(this string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            var data = SHA1.Create().ComputeHash(buffer);

            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }
            
            return sb.ToString().ToLower();
        }

        public static Wxprepay wxPayOrderSomething(string openid,string total_fee,string couponType, Store store)
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
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(store.appid.Trim(), store.mchid.Trim(), total_fee, store.StoreName, couponType,openid, prepayid);
                var wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                    
                
                LogHelper.WriteLog("wxJsApiParam:" + wxJsApiParam.ToString());
                //在页面上显示订单信息
                wxprepay.wxJsApiParam = wxJsApiParam;
                wxprepay.prepayid = prepayid;
                return wxprepay;
            }

           
           
        }
        public static string wxPayOrderQuery(string out_trade_no, string appid, string mchid)
        {

            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(out_trade_no))//如果微信订单号存在，则以微信订单号为准
            {
                data.SetValue("out_trade_no", out_trade_no);
            }
            else
            {
                throw new WxPayException("订单查询接口中，out_trade_no填一个！");
            }
 
            WxPayData result = WxPayApi.OrderQuery(data, appid, mchid);//提交订单查询请求给API，接收返回数据
            if (result.IsSet("result_code") && result.GetValue("result_code").ToString() == "SUCCESS" && result.IsSet("trade_state") && result.GetValue("trade_state").ToString() == "SUCCESS")
            {
                return result.GetValue("time_end").ToString() ;
            }else
                { return null; }

        }

        
       
    }
}