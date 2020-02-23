/**  版本信息模板在安装目录下，可自行修改。
* IntegralRecord.cs
*
* 功 能： N/A
* 类 名： IntegralRecord
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:08:53   N/A    初版
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
	/// IntegralRecord:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class IntegralRecord
	{
		public IntegralRecord()
		{}
		#region Model
		private string _integralrecord;
		private string _usercode;
		private string _recordtype;
		private int? _integral;
		private DateTime? _createtime;
		private string _explain;
		/// <summary>
		/// 
		/// </summary>
		public string IntegralRecordCode
		{
			set{ _integralrecord=value;}
			get{return _integralrecord;}
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
		public string RecordType
		{
			set{ _recordtype=value;}
			get{return _recordtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Integral
		{
			set{ _integral=value;}
			get{return _integral;}
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
		public string Explain
		{
			set{ _explain=value;}
			get{return _explain;}
		}
		#endregion Model

	}
}

