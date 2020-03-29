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
    public class RechargeTypeController : BaseApiController
    {
        /// <summary>
        /// 获取充值类型
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/RechargeType/GetRechargeTypeList
        [HttpGet]
        public IHttpActionResult GetRechargeTypeList()
        {
            SimpleResult result = new SimpleResult();
            IRechargeTypeService _service = new RechargeTypeService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetRechargeTypeList();
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
                LogHelper.WriteLog("GetRechargeTypeList ", ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }

        /// <summary>
        /// 获获取充值类型详情
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RechargeType/GetRechargeTypeByCode?code=1
        [HttpGet]
        public IHttpActionResult GetRechargeTypeByCode(string code)
        {
            SimpleResult result = new SimpleResult();
            IRechargeTypeService _service = new RechargeTypeService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetRechargeTypeByCode(code);
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
                LogHelper.WriteLog("GetRechargeTypeByCode  code" + code, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }
    }
}