/**  版本信息模板在安装目录下，可自行修改。
* RechargeType.cs
*
* 功 能： N/A
* 类 名： RechargeType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:09:01   N/A    初版
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
	/// RechargeType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RechargeType
	{
		public RechargeType()
		{}
		#region Model
		private string _rechargetypecode;
		private string _rechargetypename;
		private bool _status;
		private decimal? _money;
		private decimal? _donationamount;
		/// <summary>
		/// 
		/// </summary>
		public string RechargeTypeCode
		{
			set{ _rechargetypecode=value;}
			get{return _rechargetypecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RechargeTypeName
		{
			set{ _rechargetypename=value;}
			get{return _rechargetypename; }
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DonationAmount
		{
			set{ _donationamount=value;}
			get{return _donationamount;}
		}
		#endregion Model

	}
}

