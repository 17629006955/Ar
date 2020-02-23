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
    /// <summary>
    /// 页签
    /// </summary>
    public class ListTypeController : BaseApiController
    {
        ////http://localhost:10010//api/ListType/ListType
        [HttpGet]
        public IHttpActionResult ListType()
        {
            SimpleResult result = new SimpleResult();
            IListTypeService _service = new ListTypeService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetListType();
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