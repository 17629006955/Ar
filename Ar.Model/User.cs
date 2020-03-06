/**  版本信息模板在安装目录下，可自行修改。
* User.cs
*
* 功 能： N/A
* 类 名： User
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:09:03   N/A    初版
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
	/// User:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class User
	{
		public User()
		{}
		#region Model
		private string _code;
		private string _username;
		private DateTime? _birthday;
		private string _phone;
		private string _useriamgeurl;
		private bool _sex;
		private DateTime? _createtime;
		private int? _level;
		private bool _ismember;
		private string _privilegedescription;
		private DateTime? _effectivedate;
		private string _availabletimeinterval;
		private string _cardtelephone;
		private string _instructions;
        private string _recommendedPhone;
        
        /// <summary>
        /// 
        /// </summary>
        public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserIamgeUrl
		{
			set{ _useriamgeurl=value;}
			get{return _useriamgeurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Level
		{
			set{ _level=value;}
			get{return _level;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsMember
		{
			set{ _ismember=value;}
			get{return _ismember;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PrivilegeDescription
		{
			set{ _privilegedescription=value;}
			get{return _privilegedescription;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EffectiveDate
		{
			set{ _effectivedate=value;}
			get{return _effectivedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AvailableTimeInterval
		{
			set{ _availabletimeinterval=value;}
			get{return _availabletimeinterval;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CardTelephone
		{
			set{ _cardtelephone=value;}
			get{return _cardtelephone;}
		}

        /// <summary>
        /// 
        /// </summary>
        public string RecommendedPhone
        {
            set { _recommendedPhone = value; }
            get { return _recommendedPhone; }
        }
        
        #endregion Model

    }
}

