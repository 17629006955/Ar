/**  版本信息模板在安装目录下，可自行修改。
* financialStatements.cs
*
* 功 能： N/A
* 类 名： financialStatements
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/3/21 11:32:05   N/A    初版
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
	/// financialStatements:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class financialStatements
	{
		public financialStatements()
		{}
		#region Model
		private string _code;
		private DateTime? _createtime;
		private string _userphone;
		private DateTime? _usercreatetime;
		private string _storename;
		private string _productiontype;
		private string _cstname;
		private string _productioncode;
		private string _productionname;
		private int? _iquantity;
		private decimal? _itaxunitprice;
		private decimal? _isum;
		private string _cpersonname;
		private string _paytype;
		private decimal? _amountofincome;
		private decimal? _donationamount;
		private string _couponusecode;
		private decimal? _couponusemoney;
		private DateTime? _getcoupontime;
		private decimal? _usewalletmoney;
		private string _ratio;
		private DateTime? _recordsofconsumptioncreatetime;
		private string _writeoffuser;
		private string _productioncode1;
		private string _productionname1;
		private decimal? _experienceprice;
		private int? _iquantity1;
		private decimal? _recordsmoney;
		private decimal? _couponusemoney1;
		private decimal? _actualconsumption;
		private decimal? _usewalletmoney1;
		private decimal? _usewalletaccountprincipal;
		private decimal? _financialrevenueaccounting;
		private decimal? _imoney;
		private string _productinforate;
		private decimal? _itax;
		private decimal? _grossprofit;
		/// <summary>
		/// 
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
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
		public string UserPhone
		{
			set{ _userphone=value;}
			get{return _userphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UserCreateTime
		{
			set{ _usercreatetime=value;}
			get{return _usercreatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StoreName
		{
			set{ _storename=value;}
			get{return _storename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductionType
		{
			set{ _productiontype=value;}
			get{return _productiontype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Cstname
		{
			set{ _cstname=value;}
			get{return _cstname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductionCode
		{
			set{ _productioncode=value;}
			get{return _productioncode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductionName
		{
			set{ _productionname=value;}
			get{return _productionname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Iquantity
		{
			set{ _iquantity=value;}
			get{return _iquantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Itaxunitprice
		{
			set{ _itaxunitprice=value;}
			get{return _itaxunitprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Isum
		{
			set{ _isum=value;}
			get{return _isum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CpersonName
		{
			set{ _cpersonname=value;}
			get{return _cpersonname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PayType
		{
			set{ _paytype=value;}
			get{return _paytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? AmountOfIncome
		{
			set{ _amountofincome=value;}
			get{return _amountofincome;}
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
		/// 
		/// </summary>
		public string CouponUseCode
		{
			set{ _couponusecode=value;}
			get{return _couponusecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CouponUseMoney
		{
			set{ _couponusemoney=value;}
			get{return _couponusemoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? GetCouponTime
		{
			set{ _getcoupontime=value;}
			get{return _getcoupontime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? UseWalletMoney
		{
			set{ _usewalletmoney=value;}
			get{return _usewalletmoney;}
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
		public DateTime? RecordsOfConsumptionCreateTime
		{
			set{ _recordsofconsumptioncreatetime=value;}
			get{return _recordsofconsumptioncreatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WriteOffUser
		{
			set{ _writeoffuser=value;}
			get{return _writeoffuser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductionCode1
		{
			set{ _productioncode1=value;}
			get{return _productioncode1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductionName1
		{
			set{ _productionname1=value;}
			get{return _productionname1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ExperiencePrice
		{
			set{ _experienceprice=value;}
			get{return _experienceprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Iquantity1
		{
			set{ _iquantity1=value;}
			get{return _iquantity1;}
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
		public decimal? CouponUseMoney1
		{
			set{ _couponusemoney1=value;}
			get{return _couponusemoney1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ActualConsumption
		{
			set{ _actualconsumption=value;}
			get{return _actualconsumption;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? UseWalletMoney1
		{
			set{ _usewalletmoney1=value;}
			get{return _usewalletmoney1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? UseWalletAccountPrincipal
		{
			set{ _usewalletaccountprincipal=value;}
			get{return _usewalletaccountprincipal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? FinancialRevenueAccounting
		{
			set{ _financialrevenueaccounting=value;}
			get{return _financialrevenueaccounting;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Imoney
		{
			set{ _imoney=value;}
			get{return _imoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductInfoRate
		{
			set{ _productinforate=value;}
			get{return _productinforate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Itax
		{
			set{ _itax=value;}
			get{return _itax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? GrossProfit
		{
			set{ _grossprofit=value;}
			get{return _grossprofit;}
		}
		#endregion Model

	}
}

