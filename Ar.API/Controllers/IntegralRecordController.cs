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
    /// <summary>
    /// 积分记录
    /// </summary>
    public class IntegralRecordController : BaseApiController
    {
        /// <summary>
        /// 获取积分记录
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/IntegralRecord/GetIntegralRecordByUserCode?code=1
        [HttpGet]
        public IHttpActionResult GetStoreByCode(string code)
        {
            SimpleResult result = new SimpleResult();
            IIntegralRecordService _service = new IntegralRecordService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetStoreByCode(code);
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
                LogHelper.WriteLog("GetStoreByCode获取积分code：" +code+ ex.Message);
                LogHelper.WriteLog("GetStoreByCode获取积分code：" + code  + ex.StackTrace);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }

        /// <summary>
        /// 获取积分记录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/IntegralRecord/GetIntegralRecordByUserCode?userCode=1
        [HttpGet]
        public IHttpActionResult GetIntegralRecordByUserCode(string userCode)
        {
            SimpleResult result = new SimpleResult();
            IIntegralRecordService _service = new IntegralRecordService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetIntegralRecordByUserCode(userCode);
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
                LogHelper.WriteLog("GetIntegralRecordByUserCode获取积分userCode：" + userCode + ex.Message);
                LogHelper.WriteLog("GetIntegralRecordByUserCode获取积分userCode：" + userCode + ex.StackTrace);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }

        /// <summary>
        /// 获取积分记录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/IntegralRecord/CreateUserStore
        [HttpPost]
        public IHttpActionResult CreateUserStore(IntegralRecord integralRecord)
        {
            SimpleResult result = new SimpleResult();
            IIntegralRecordService _service = new IntegralRecordService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.CreateUserStore(integralRecord);
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
                LogHelper.WriteLog("CreateUserStore创建积分："  + ex.Message);
                LogHelper.WriteLog("CreateUserStore创建积分：" + ex.StackTrace);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);
        }
    }
}