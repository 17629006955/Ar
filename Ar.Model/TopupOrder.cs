/**  版本信息模板在安装目录下，可自行修改。
* TopupOrder.cs
*
* 功 能： N/A
* 类 名： TopupOrder
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/3/8 15:36:13   N/A    初版
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
	/// TopupOrder:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TopupOrder
	{
		public TopupOrder()
		{}
		#region Model
		private string _topupordercode;
		private string _usercode;
		private string _recordsofconsumptioncode;
		private string _walleprcode;
		private string _outtradeno;
		private DateTime? _paydatetime;
		/// <summary>
		/// 
		/// </summary>
		public string TopupOrderCode
		{
			set{ _topupordercode=value;}
			get{return _topupordercode;}
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
		public string RecordsOfConsumptionCode
		{
			set{ _recordsofconsumptioncode=value;}
			get{return _recordsofconsumptioncode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WallePrCode
		{
			set{ _walleprcode=value;}
			get{return _walleprcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OutTradeNo
		{
			set{ _outtradeno=value;}
			get{return _outtradeno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PayDatetime
		{
			set{ _paydatetime=value;}
			get{return _paydatetime;}
		}
		#endregion Model

	}
}

