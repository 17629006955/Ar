﻿/**  版本信息模板在安装目录下，可自行修改。
* Order.cs
*
* 功 能： N/A
* 类 名： Order
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
	/// Order:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Order
	{
		public Order()
		{}
		#region Model
		private string _ordercode;
		private string _usercode;
		private string _productcode;
		private int? _number;
		private decimal? _money;
		private string _storecode;
		private DateTime? _createtime;
		private DateTime? _paytime;
		private DateTime? _appointmenttime;
		private string _experiencevouchercode;
        private string _wxPrepayId;
        private bool _isWriteOff;
        private string _productName;
        private int _orderState;
        private string _imageurl;
        private string _videourl;

        private string _orderNO;
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
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductCode
		{
			set{ _productcode=value;}
			get{return _productcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Number
		{
			set{ _number=value;}
			get{return _number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StoreCode
		{
			set{ _storecode=value;}
			get{return _storecode;}
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
		public DateTime? PayTime
		{
			set{ _paytime=value;}
			get{return _paytime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AppointmentTime
		{
			set{ _appointmenttime=value;}
			get{return _appointmenttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ExperienceVoucherCode
		{
			set{ _experiencevouchercode=value;}
			get{return _experiencevouchercode;}
		}

        public string ProductName
        {
            set { _productName = value; }
            get { return _productName; }
        }

        public int OrderState
        {
            set { _orderState = value; }
            get { return _orderState; }
        }

        public string Imageurl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }

        public string OrderNO
        {
            set { _orderNO = value; }
            get { return _orderNO; }
        }
        public string Videourl
        {
            set { _videourl = value; }
            get { return _videourl; }
        }


        /// <summary>
		/// 
		/// </summary>
		public string WxPrepayId
        {
            set { _wxPrepayId = value; }
            get { return _wxPrepayId; }
        }
        
        public bool IsWriteOff
        {
            set { _isWriteOff = value; }
            get { return _isWriteOff; }
        }
        #endregion Model

    }


    public partial class OrderShow
    {
        public OrderShow()
        { }
        #region Model
        private string _ordercode;
        private string _usercode;
        private string _productcode;
        private string _productName;
        private string _imageurl;
        private string _videourl;
        private int? _number;
        private decimal? _money;
        private string _storecode;
        private DateTime? _createtime;
        private DateTime? _paytime;
        private DateTime? _appointmenttime;
        private string _experiencevouchercode;
        private string _orderState;
        private bool _isWriteOff;
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
        public string UserCode
        {
            set { _usercode = value; }
            get { return _usercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }

        public string ProductName
        {
            set { _productName = value; }
            get { return _productName; }
        }
        public string Imageurl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }

        public string Videourl
        {
            set { _videourl = value; }
            get { return _videourl; }
        }
        public string OrderState
        {
            set { _orderState = value; }
            get { return _orderState; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int? Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StoreCode
        {
            set { _storecode = value; }
            get { return _storecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PayTime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AppointmentTime
        {
            set { _appointmenttime = value; }
            get { return _appointmenttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExperienceVoucherCode
        {
            set { _experiencevouchercode = value; }
            get { return _experiencevouchercode; }
        }
        public bool IsWriteOff
        {
            set { _isWriteOff = value; }
            get { return _isWriteOff; }
        }
        
        #endregion Model

    }
}

