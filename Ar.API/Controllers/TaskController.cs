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
            SimpleResult result = new SimpleResult();
            ITaskService _service = new TaskService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetTaskList();
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
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
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
            SimpleResult result = new SimpleResult();
            ITaskService _service = new TaskService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetTaskByCode(code);
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
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }

    }
}