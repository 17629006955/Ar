
using System;
using System.Collections.Generic;

namespace AR.Model
{
    /// <summary>
    /// RechargePage:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
	public partial class RechargePage
    {
		public RechargePage()
		{}
		#region Model
		private IList<RechargeType> _rechargeTypeList;
        private decimal? _totalMoney;
        private decimal? _pechargeMoney;
		private decimal? _donationamount;
		/// <summary>
		/// 
		/// </summary>
		public IList<RechargeType> RechargeTypeList
        {
			set{ _rechargeTypeList = value;}
			get{return _rechargeTypeList; }
		}
		/// <summary>
		/// 账户余额
		/// </summary>
		public decimal? TotalMoney
        {
			set{ _totalMoney = value;}
			get{return _totalMoney; }
		}
        /// <summary>
		/// 充值金额
		/// </summary>
		public decimal? PechargeMoney
        {
            set { _pechargeMoney = value; }
            get { return _pechargeMoney; }
        }
        /// <summary>
        /// 赠送金额
        /// </summary>
        public decimal? DonationAmount
		{
			set{ _donationamount=value;}
			get{return _donationamount;}
		}
		#endregion Model

	}
}

