/**  版本信息模板在安装目录下，可自行修改。
* RecordsOfConsumption.cs
*
* 功 能： N/A
* 类 名： RecordsOfConsumption
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
	/// RecordsOfConsumption:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordsOfConsumption
	{
		public RecordsOfConsumption()
		{}
		#region Model
		private string _recordsofconsumptioncode;
		private string _usercode;
		private string _rechargetypecode;
        private string _rechargetypeName;
        private decimal? _recordsmoney;
		private string _explain;
		private DateTime? _createtime;
        private bool _isRecharging;
        private decimal? _RecordsDonationAmountMoney;
        private decimal? _RecordsAccountPrincipalMoney;
        private string _ordercode;
        public bool IsRecharging
        {
            set { _isRecharging = value; }
            get { return _isRecharging; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderCode
        {
            set { _ordercode = value; }
            get { return _ordercode; }
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
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RechargeTypeCode
		{
			set{ _rechargetypecode=value;}
			get{return _rechargetypecode;}
		}

        public string RechargeTypeName
        {
            set { _rechargetypeName = value; }
            get { return _rechargetypeName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? RecordsMoney
		{
			set{ _recordsmoney=value;}
			get{return _recordsmoney;}
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
        /// <summary>
        /// 
        /// </summary>
        public decimal? RecordsDonationAmountMoney
        {

            set { _RecordsDonationAmountMoney = value; }
            get { return _RecordsDonationAmountMoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? RecordsAccountPrincipalMoney
        {

            set { _RecordsAccountPrincipalMoney = value; }
            get { return _RecordsAccountPrincipalMoney; }
        }
        #endregion Model

    }
    
}

