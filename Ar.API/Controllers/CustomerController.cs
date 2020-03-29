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
    public class CustomerController : BaseApiController
    {
        /// <summary>
        /// 获取客服列表
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/Customer/GetCustomerServiceList
        [HttpGet]
        public IHttpActionResult GetCustomerServiceList()
        {
            SimpleResult result = new SimpleResult();
            ICustomerServiceS _service = new CustomerServiceS();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetCustomerServiceList();
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
                LogHelper.WriteLog("GetCustomerServiceList获取客服列表" + ex.Message);
                LogHelper.WriteLog("GetCustomerServiceList获取客服列表" + ex.StackTrace);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }

        /// <summary>
        /// 获取门店客服
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Customer/GetCustomerService?code=1
        [HttpGet]
        public IHttpActionResult GetCustomerService(string code)
        {
            SimpleResult result = new SimpleResult();
            ICustomerServiceS _service = new CustomerServiceS();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetCustomerService(code);
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
                LogHelper.WriteLog("GetCustomerService获取门店客服列表" + ex.Message);
                LogHelper.WriteLog("GetCustomerService获取门店客服列表" + ex.StackTrace);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }

    }
}