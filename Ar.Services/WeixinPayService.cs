using System;
using System.Configuration;

namespace TalentCloud.Agriculture.Weixin.Services
{
    public class WeixinPayService
    {
        /// <summary>
        /// 微信分配的AppId
        /// </summary>
        public string AppId = ConfigurationManager.AppSettings["WeixinPayAppid"].ToString();
        /// <summary>
        /// 公众号的支付密钥appsecret
        /// </summary>
        public string AppSecret = ConfigurationManager.AppSettings["WeixinPayAppsecret"].ToString();
        /// <summary>
        /// 当前用户IP
        /// </summary>
        public string Ip = "";
        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId = ConfigurationManager.AppSettings["MchId"].ToString();
        /// <summary>
        /// 签名Key
        /// </summary>
        public string SignKey = ConfigurationManager.AppSettings["SignKey"].ToString();
        //=======【证书路径设置】=====================================
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public string CertPath = ConfigurationManager.AppSettings["certPath"].ToString();
        /// <summary>
        /// 证书密码,windows上可以直接双击导入系统，导入过程中会提示输入证书密码，证书密码默认为您的商户ID
        /// </summary>
        public string CertPassword = ConfigurationManager.AppSettings["certpsd"].ToString();
        /// <summary>
        /// 支付结果通知回调url，用于商户接收支付结果
        /// </summary>
        public string NOTIFY_URL = ConfigurationManager.AppSettings["WebSite"].ToString() + "/Weixin/PayCallback.aspx";


        /// <summary>
        ///   生成随机串，随机串包含字母或数字 @return 随机串
        /// </summary>
        /// <returns></returns>
        public string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
        /// </summary>
        /// <returns></returns>
        public string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}