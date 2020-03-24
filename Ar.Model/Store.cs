/**  版本信息模板在安装目录下，可自行修改。
* Store.cs
*
* 功 能： N/A
* 类 名： Store
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:09:02   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace AR.Model
{
	/// <summary>
	/// Store:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Store
	{

        public Store()
        { }
        #region Model
        private string _storecode;
        private string _storename;
        private string _addr;
        private string _addrip;
        private string _appid;
        private string _secret;
        private string _mchid;
        private string _accessToken;
        private string _jsapi_ticket;
        private string _api_ticket;
        private string _VIPName;
        private string _BackgroundPictureUrl;
        private string _SmallIcon;
        private string _BrandStoreName;
        private DateTime? _accessTokenCreateTime;

        public string VIPName
        {
            set { _VIPName = _VIPName; }
            get { return _storecode; }
        }
        public string BackgroundPictureUrl
        {
            set { _BackgroundPictureUrl = value; }
            get { return _BackgroundPictureUrl; }
        }
        public string SmallIcon
        {
            set { _SmallIcon = value; }
            get { return _SmallIcon; }
        }
        public string BrandStoreName
        {
            set { _BrandStoreName = value; }
            get { return _BrandStoreName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StoreCode
        {
            set { _storecode = value; }
            get { return _storecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StoreName
        {
            set { _storename = value; }
            get { return _storename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string addr
        {
            set { _addr = value; }
            get { return _addr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string addrIP
        {
            set { _addrip = value; }
            get { return _addrip; }
        }   /// <summary>
            /// 
            /// </summary>
        public string appid
        {
            set { _appid = value; }
            get { return _appid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mchid
        {
            set { _mchid = value; }
            get { return _mchid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string secret
        {
            set { _secret = value; }
            get { return _secret; }
        }

        public string accessToken
        {
            set { _accessToken = value; }
            get { return _accessToken; }
        }
        public string jsapi_ticket
        {
            set { _jsapi_ticket = value; }
            get { return _jsapi_ticket; }
        }
        public string api_ticket
        {
            set { _api_ticket = value; }
            get { return _api_ticket; }
        }
        
        public DateTime? accessTokenCreateTime
        {
            set { _accessTokenCreateTime = value; }
            get { return _accessTokenCreateTime; }
        }


        
        #endregion Model
    }
}

