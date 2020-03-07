/**  版本信息模板在安装目录下，可自行修改。
* ProductInfo.cs
*
* 功 能： N/A
* 类 名： ProductInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:08:56   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;

namespace AR.Model
{
	/// <summary>
	/// ProductInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductInfo
	{
		public ProductInfo()
		{}
		#region Model
		private string _productcode;
		private string _productname;
		private string _experiencepopulation;
		private string _specialrequirements;
		private string _plot;
		private string _imageurl;
		private string _videourl;
		private DateTime? _versionstarttime;
		private DateTime? _versionendtime;
		private decimal? _experienceprice;
		private Single _thriller;
		private string _instructions;
        private List<string> _typeShowList;
        private string _listTypeCode;
        private string _listTypeName;
        private string _store;

        private List<DateTime> _selectDate;
        private int _peopleCount;

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
		public string ProductName
		{
			set{ _productname=value;}
			get{return _productname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ExperiencePopulation
		{
			set{ _experiencepopulation=value;}
			get{return _experiencepopulation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SpecialRequirements
		{
			set{ _specialrequirements=value;}
			get{return _specialrequirements;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Plot
		{
			set{ _plot=value;}
			get{return _plot;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Imageurl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string videourl
		{
			set{ _videourl=value;}
			get{return _videourl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? VersionStartTime
		{
			set{ _versionstarttime=value;}
			get{return _versionstarttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? VersionEndTime
		{
			set{ _versionendtime=value;}
			get{return _versionendtime;}
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
		public Single Thriller
		{
			set{ _thriller=value;}
			get{return _thriller;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Instructions
		{
			set{ _instructions=value;}
			get{return _instructions;}
		}

        public List<string> TypeShowList
        {
            set { _typeShowList = value; }
            get { return _typeShowList; }
        }
        public string ListTypeCode
        {
            set { _listTypeCode = value; }
            get { return _listTypeCode; }
        }

        public string ListTypeName
        {
            set { _listTypeName = value; }
            get { return _listTypeName; }
        }

        public string Store
        {
            set { _store = value; }
            get { return _store; }
        }
        public List<DateTime> SelectDate
        {
            set { _selectDate = value; }
            get { return _selectDate; }
        }

        public int PeopleCount
        {
            set { _peopleCount = value; }
            get { return _peopleCount; }
        }
        #endregion Model

    }
}

