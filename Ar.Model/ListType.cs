/**  版本信息模板在安装目录下，可自行修改。
* ListType.cs
*
* 功 能： N/A
* 类 名： ListType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:08:54   N/A    初版
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
	/// ListType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ListType
	{
		public ListType()
		{}
		#region Model
		private string _listtypecode;
		private string _listtypename;
		private bool _status;
		/// <summary>
		/// 
		/// </summary>
		public string ListTypeCode
		{
			set{ _listtypecode=value;}
			get{return _listtypecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ListTypeName
		{
			set{ _listtypename=value;}
			get{return _listtypename;}
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

