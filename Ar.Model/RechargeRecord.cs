/**  版本信息模板在安装目录下，可自行修改。
* RechargeRecord.cs
*
* 功 能： N/A
* 类 名： RechargeRecord
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:09:00   N/A    初版
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
	/// RechargeRecord:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RechargeRecord
	{
        /// <summary>
        /// 充值记录
        /// </summary>
		public RechargeRecord()
		{}
		#region Model
		private string _rechargerecord;
		private string _usercode;
		private decimal? _rechargeamount;
		private string _explain;
		private DateTime? _createtime;
		/// <summary>
		/// 
		/// </summary>
		public string RechargeRecordCode
		{
			set{ _rechargerecord=value;}
			get{return _rechargerecord;}
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
		public decimal? RechargeAmount
		{
			set{ _rechargeamount=value;}
			get{return _rechargeamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Explain
		{
			set{ _explain=value;}
			get{return _explain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

