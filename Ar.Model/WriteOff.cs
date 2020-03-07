/**  版本信息模板在安装目录下，可自行修改。
* WriteOffCode.cs
*
* 功 能： N/A
* 类 名： WriteOffCode
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/3/7 15:58:56   N/A    初版
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
	/// WriteOffCode:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class WriteOff
	{
		public WriteOff()
		{}
		#region Model
		private string _writeoffcode;
		private string _ordercode;
		private DateTime? _createtime;
		/// <summary>
		/// 
		/// </summary>
		public string WriteOffCode
		{
			set{ _writeoffcode=value;}
			get{return _writeoffcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderCode
		{
			set{ _ordercode=value;}
			get{return _ordercode;}
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

