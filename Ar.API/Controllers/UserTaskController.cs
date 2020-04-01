using Ar.Common.tool;
using Ar.Model;
using Ar.API.Controllers.BaseContolles;
using Ar.Model.BaseResult;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using Ar.IServices;
using Ar.Services;
using AR.Model;
using Ar.Common;

namespace Ar.API.Controllers
{ 
    public class UserTaskController : BaseApiController
    {
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/UserTask/GetUserTaskList?userCode=1
        [HttpGet]
        public IHttpActionResult GetUserTaskList(string userCode)
        {
            LogHelper.WriteLog("GetUserTaskList userCode" + userCode);
            var sharePictures = ConfigurationManager.AppSettings["sharePictures"].ToString();
            var shareDescriptions = ConfigurationManager.AppSettings["shareDescriptions"].ToString();
            var shareTitle = ConfigurationManager.AppSettings["shareTitle"].ToString();

            SimpleResult result = new SimpleResult();
            IUserTaskService _service = new UserTaskService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetUserTaskList(userCode);
                    foreach (var t in list)
                    {
                        if (t.TaskCode == "2")
                        {
                            t.ShareDescriptions = shareDescriptions;
                            t.SharePictures = sharePictures;
                            t.ShareTitle = shareTitle;
                        }
                    }
                    result.Resource = list;
                result.Status = Result.SUCCEED;
                }
                else
                {
                    result.Status = ResultType;
                    result.Resource = ReAccessToken;
                    result.Msg = TokenMessage;
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog("GetUserTaskList userCode" + userCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetUserTaskList result" + Json(result));
            return Json(result);

        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/UserTask/GetUserTaskByCode?UserTaskCode=1
        [HttpGet]
        public IHttpActionResult GetUserTaskByCode(string UserTaskCode)
        {
            LogHelper.WriteLog("GetUserTaskByCode UserTaskCode" + UserTaskCode);
          
            SimpleResult result = new SimpleResult();
            IUserTaskService _service = new UserTaskService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetUserTaskByCode(UserTaskCode);
                result.Resource = list;
                result.Status = Result.SUCCEED;
                }
                else
                {
                    result.Status = ResultType;
                    result.Resource = ReAccessToken;
                    result.Msg = TokenMessage;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("GetUserTaskByCode UserTaskCode" + UserTaskCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetUserTaskByCode result" + Json(result));
            return Json(result);

        }

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/UserTask/UpdateUserTask?orderCode=1&isComplete=1
        [HttpGet]
        public IHttpActionResult UpdateUserTask(string UserTaskCode, int isComplete)
        {
            LogHelper.WriteLog("UpdateUserTask UserTaskCode" + UserTaskCode);
            LogHelper.WriteLog("UpdateUserTask isComplete" + isComplete);
            SimpleResult result = new SimpleResult();
            IUserTaskService _service = new UserTaskService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.UpdateUserTask(UserTaskCode, isComplete);
                result.Resource = list;
                result.Status = Result.SUCCEED;
                }
                else
                {
                    result.Status = ResultType;
                    result.Resource = ReAccessToken;
                    result.Msg = TokenMessage;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("UpdateUserTask UserTaskCode"+ UserTaskCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("UpdateUserTask result" + Json(result));
            return Json(result);

        }

        
    }
}