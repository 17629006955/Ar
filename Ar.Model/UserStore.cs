/**  版本信息模板在安装目录下，可自行修改。
* UserStore.cs
*
* 功 能： N/A
* 类 名： UserStore
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:09:04   N/A    初版
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
	/// UserStore:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserStore
	{
		public UserStore()
		{}
		#region Model
		private string _userstorecode;
		private string _usercode;
		private string _openid;
		private string _membershipcardstore;
		/// <summary>
		/// 
		/// </summary>
		public string UserStoreCode
		{
			set{ _userstorecode=value;}
			get{return _userstorecode;}
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
		public string OpenID
		{
			set{ _openid=value;}
			get{return _openid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MembershipCardStore
		{
			set{ _membershipcardstore=value;}
			get{return _membershipcardstore;}
		}
		#endregion Model

	}
}

