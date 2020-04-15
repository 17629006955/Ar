/**  版本信息模板在安装目录下，可自行修改。
* ProductList.cs
*
* 功 能： N/A
* 类 名： ProductList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:08:57   N/A    初版
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
	/// ProductList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductList
	{
		public ProductList()
		{}
		#region Model
		private string _productlistcode;
		private string _listcode;
		private string _productcode;
		private bool _status;
        private DateTime _createTime;
        /// <summary>
        /// 
        /// </summary>
        public string ProductListCode
		{
			set{ _productlistcode=value;}
			get{return _productlistcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ListCode
		{
			set{ _listcode=value;}
			get{return _listcode;}
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
		public bool Status
		{
			set{ _status=value;}
			get{return _status;}
		}
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createTime = value; }
            get { return _createTime; }
        }
        #endregion Model

    }
}

