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
    /// <summary>
    /// 轮询
    /// </summary>
    public class PropagandaController : BaseApiController
    {
        ////http://localhost:10010//api/Propaganda/GetPropaganda
        [HttpGet]
        public IHttpActionResult GetPropaganda()
        {
            SimpleResult result = new SimpleResult();
            IPropagandaService _service = new PropagandaService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetPropaganda();
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
                LogHelper.WriteLog("GetPropaganda", ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }

    }
}