/**  版本信息模板在安装目录下，可自行修改。
* Task.cs
*
* 功 能： N/A
* 类 名： Task
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:09:03   N/A    初版
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
	/// Task:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Task
	{
		public Task()
		{}
		#region Model
		private string _taskcode;
		private string _taskname;
		private string _tasktarget;
		private string _reward;
		private int? _integral;
		private DateTime? _startime;
		private DateTime? _versionendtime;
		private DateTime? _createtime;
		private int? _tasklevel;

        private string _sharePictures;
        private string _shareTitle;
        private string _shareDescriptions;
        public string SharePictures
        {
            set { _sharePictures = value; }
            get { return _sharePictures; }
        }
        public string ShareTitle
        {
            set { _shareTitle = value; }
            get { return _shareTitle; }
        }
        public string ShareDescriptions
        {
            set { _shareDescriptions = value; }
            get { return _shareDescriptions; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaskCode
		{
			set{ _taskcode=value;}
			get{return _taskcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TasKName
		{
			set{ _taskname=value;}
			get{return _taskname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TaskTarget
		{
			set{ _tasktarget=value;}
			get{return _tasktarget;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Reward
		{
			set{ _reward=value;}
			get{return _reward;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Integral
		{
			set{ _integral=value;}
			get{return _integral;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StarTime
		{
			set{ _startime=value;}
			get{return _startime;}
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
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TaskLevel
		{
			set{ _tasklevel=value;}
			get{return _tasklevel;}
		}
		#endregion Model

	}
}

