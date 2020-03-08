/**  版本信息模板在安装目录下，可自行修改。
* UseWallet.cs
*
* 功 能： N/A
* 类 名： UseWallet
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:09:06   N/A    初版
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
	/// UseWallet:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UseWallet
	{
		public UseWallet()
		{}
		#region Model
		private string _walletcode;
		private string _usercode;
		private decimal? _accountprincipal;
		private decimal? _donationamount;
        private decimal? _totalAmount;
        private string _ratio;
		private bool _status;
		private int? _sort;
        private bool _IsMissionGiveaway;
        /// <summary>
        /// 
        /// </summary>
        public string WalletCode
		{
			set{ _walletcode=value;}
			get{return _walletcode;}
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
		public decimal? AccountPrincipal
		{
			set{ _accountprincipal=value;}
			get{return _accountprincipal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DonationAmount
		{
			set{ _donationamount=value;}
			get{return _donationamount;}
		}
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal? TotalAmount
        {
            set { _totalAmount = value; }
            get { return _totalAmount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Ratio
		{
			set{ _ratio=value;}
			get{return _ratio;}
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
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
        /// <summary>
		/// 
		/// </summary>
		public bool IsMissionGiveaway
        {
            set { _IsMissionGiveaway = value; }
            get { return _IsMissionGiveaway; }
        }
        #endregion Model

    }
}

