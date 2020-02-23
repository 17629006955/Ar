using System;
using System.Collections.Generic;

namespace AR.Model
{
	/// <summary>
	/// 预定页面
	/// </summary>
	[Serializable]
	public partial class PayPage
    {
		public PayPage()
		{}
		#region Model
		private string _productcode;
		private string _productname;
		private string _imageurl;
		private decimal? _experienceprice;
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
		public string Imageurl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public decimal? ExperiencePrice
		{
			set{ _experienceprice=value;}
			get{return _experienceprice;}
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

