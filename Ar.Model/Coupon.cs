/**  版本信息模板在安装目录下，可自行修改。
* Coupon.cs
*
* 功 能： N/A
* 类 名： Coupon
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
	/// Coupon:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Coupon
	{
		public Coupon()
		{}
		#region Model
		private string _couponcode;
		private string _usercode;
		private string _coupontypecode;
        private string _coupontypeName;
        private DateTime? _createtime;
		private DateTime? _strattime;
		private DateTime? _versionendtime;
		private bool _isused;
		private bool _isgiveed;
		private DateTime? _usetime;
		private DateTime? _giveedtime;
		private string _couponusecode;
		/// <summary>
		/// 
		/// </summary>
		public string CouponCode
		{
			set{ _couponcode=value;}
			get{return _couponcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CouponTypeCode
		{
			set{ _coupontypecode=value;}
			get{return _coupontypecode;}
		}

        public string CouponTypeName
        {
            set { _coupontypeName = value; }
            get { return _coupontypeName; }
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
		public DateTime? StratTime
		{
			set{ _strattime=value;}
			get{return _strattime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? VersionEndTime
		{
			set{ _versionendtime=value;}
			get{return _versionendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsUsed
		{
			set{ _isused=value;}
			get{return _isused;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsGiveed
		{
			set{ _isgiveed=value;}
			get{return _isgiveed;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UseTime
		{
			set{ _usetime=value;}
			get{return _usetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? GiveedTime
		{
			set{ _giveedtime=value;}
			get{return _giveedtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CouponUseCode
		{
			set{ _couponusecode=value;}
			get{return _couponusecode;}
		}
		#endregion Model

	}
}

