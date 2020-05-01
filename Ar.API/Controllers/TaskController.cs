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
using Ar.Common;

namespace Ar.API.Controllers
{
    public class TaskController : BaseApiController
    {
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/Task/GetTaskList
        [HttpGet]
        public IHttpActionResult GetTaskList()
        {
            var sharePictures = ConfigurationManager.AppSettings["sharePictures"].ToString();
            var shareDescriptions = ConfigurationManager.AppSettings["shareDescriptions"].ToString();
            var shareTitle = ConfigurationManager.AppSettings["shareTitle"].ToString();

            LogHelper.WriteLog("GetTaskList");
            SimpleResult result = new SimpleResult();
            ITaskService _service = new TaskService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetTaskList();
                    foreach(var t in list)
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
                    result.Msg=TokenMessage;
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog("GetTaskList " , ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetTaskList result" + Json(result));
            return Json(result);

        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Task/GetTaskByCode?code=1
        [HttpGet]
        public IHttpActionResult GetTaskByCode(string code)
        {
            LogHelper.WriteLog("GetTaskByCode code" + code);

            var sharePictures = ConfigurationManager.AppSettings["sharePictures"].ToString();
            var shareDescriptions = ConfigurationManager.AppSettings["shareDescriptions"].ToString();
            var shareTitle = ConfigurationManager.AppSettings["shareTitle"].ToString();

            SimpleResult result = new SimpleResult();
            ITaskService _service = new TaskService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetTaskByCode(code);
                    if (list.TaskCode == "2")
                    {
                        list.ShareDescriptions = shareDescriptions;
                        list.SharePictures = sharePictures;
                        list.ShareTitle = shareTitle;
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
            catch (Exception ex)
            {

                LogHelper.WriteLog("GetTaskByCode code" + code, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetTaskByCode result" + Json(result));
            return Json(result);

        }

    }
}