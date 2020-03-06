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
    public class StoreController : BaseApiController
    {
        /// <summary>
        /// 获取门店
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Store/GetStore?code=1
        [HttpGet]
        public IHttpActionResult GetStore(string code)
        {
            SimpleResult result = new SimpleResult();
            IStoreService _service = new StoreService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetStore(code);
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
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }

        /// <summary>
        /// 获取门店列表
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/Store/GetStoreList
        [HttpGet]
        public IHttpActionResult GetStoreList()
        {
            SimpleResult result = new SimpleResult();
            IStoreService _service = new StoreService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetStoreList();
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