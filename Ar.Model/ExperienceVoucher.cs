﻿/**  版本信息模板在安装目录下，可自行修改。
* ExperienceVoucher.cs
*
* 功 能： N/A
* 类 名： ExperienceVoucher
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:08:52   N/A    初版
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
	/// ExperienceVoucher:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ExperienceVoucher
	{
		public ExperienceVoucher()
		{}
		#region Model
		private string _experiencevouchercode;
		private string _productcode;
		private int? _experienceno;
		private decimal? _experienceprice;
		private DateTime? _versionstarttime;
		private DateTime? _versionendtime;
		/// <summary>
		/// 
		/// </summary>
		public string ExperienceVoucherCode
		{
			set{ _experiencevouchercode=value;}
			get{return _experiencevouchercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductCode
		{
			set{ _productcode=value;}
			get{return _productcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ExperienceNo
		{
			set{ _experienceno=value;}
			get{return _experienceno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ExperiencePrice
		{
			set{ _experienceprice=value;}
			get{return _experienceprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? VersionStartTime
		{
			set{ _versionstarttime=value;}
			get{return _versionstarttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? VersionEndTime
		{
			set{ _versionendtime=value;}
			get{return _versionendtime;}
		}
		#endregion Model

	}
}

