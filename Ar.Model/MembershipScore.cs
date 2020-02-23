/**  版本信息模板在安装目录下，可自行修改。
* MembershipScore.cs
*
* 功 能： N/A
* 类 名： MembershipScore
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:08:55   N/A    初版
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
	/// MembershipScore:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MembershipScore
	{
		public MembershipScore()
		{}
		#region Model
		private string _membershipscorecode;
		private string _usercode;
		private int? _score;
		/// <summary>
		/// 
		/// </summary>
		public string MembershipScoreCode
		{
			set{ _membershipscorecode=value;}
			get{return _membershipscorecode;}
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
		public int? Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		#endregion Model

	}
}

