/**  版本信息模板在安装目录下，可自行修改。
* CouponType.cs
*
* 功 能： N/A
* 类 名： CouponType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:08:51   N/A    初版
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
	/// CouponType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CouponType
	{
		public CouponType()
		{}
		#region Model
		private string _coupontypecode;
		private string _coupontypename;
		private bool _status;
		/// <summary>
		/// 
		/// </summary>
		public string CouponTypeCode
		{
			set{ _coupontypecode=value;}
			get{return _coupontypecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CouponTypeName
		{
			set{ _coupontypename=value;}
			get{return _coupontypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

