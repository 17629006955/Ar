/**  版本信息模板在安装目录下，可自行修改。
* Certification.cs
*
* 功 能： N/A
* 类 名： Certification
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:08:50   N/A    初版
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
	/// Certification:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Certification
	{
		public Certification()
		{}
		#region Model
		private string _certificationcode;
		private string _openid;
		private string _accesstoken;
        private string _reAccessToken;
        private DateTime? _createtime;
		/// <summary>
		/// 
		/// </summary>
		public string CertificationCode
		{
			set{ _certificationcode=value;}
			get{return _certificationcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OpenID
		{
			set{ _openid=value;}
			get{return _openid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccessToken
		{
			set{ _accesstoken=value;}
			get{return _accesstoken;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
        public string ReAccessToken
        {
            set { _reAccessToken = value; }
            get { return _reAccessToken; }
        }

        
        #endregion Model

    }
}

