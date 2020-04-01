/**  版本信息模板在安装目录下，可自行修改。
* UserTask.cs
*
* 功 能： N/A
* 类 名： UserTask
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/16 17:09:05   N/A    初版
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
	/// UserTask:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserTask
	{
		public UserTask()
		{}
		#region Model
		private string _userTaskcode;
		private string _taskcode;
		private bool _iscomplete;
		private DateTime? _taskstarttime;
		private DateTime? _taskendtime;
		private string _usercode;
		/// <summary>
		/// 
		/// </summary>
		public string UserTaskCode
        {
			set{ _userTaskcode = value;}
			get{return _userTaskcode; }
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
		public bool IsComplete
		{
			set{ _iscomplete=value;}
			get{return _iscomplete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? TaskStartTime
		{
			set{ _taskstarttime=value;}
			get{return _taskstarttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? TaskEndTime
		{
			set{ _taskendtime=value;}
			get{return _taskendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		#endregion Model

	}
    public partial class UserTaskshow
    {
        public UserTaskshow()
        { }
        #region Model
        private string _userTaskcode;
        private string _tasKName;
        private bool _iscomplete;
        private DateTime? _taskstarttime;
        private DateTime? _taskendtime;
        private string _usercode;
        private string _tasktarget;
        private string _reward;
        private int? _integral;
        private string _taskcode;


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
        public string UserTaskCode
        {
            set { _userTaskcode = value; }
            get { return _userTaskcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaskCode
        {
            set { _taskcode = value; }
            get { return _taskcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TasKName
        {
            set { _tasKName = value; }
            get { return _tasKName; }
        }
        /// <summary>
		/// 
		/// </summary>
		public string TaskTarget
        {
            set { _tasktarget = value; }
            get { return _tasktarget; }
        }
        /// <summary>
		/// 
		/// </summary>
		public string Reward
        {
            set { _reward = value; }
            get { return _reward; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsComplete
        {
            set { _iscomplete = value; }
            get { return _iscomplete; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TaskStartTime
        {
            set { _taskstarttime = value; }
            get { return _taskstarttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TaskEndTime
        {
            set { _taskendtime = value; }
            get { return _taskendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Integral
        {
            set { _integral = value; }
            get { return _integral; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserCode
        {
            set { _usercode = value; }
            get { return _usercode; }
        }
        #endregion Model

    }
}

