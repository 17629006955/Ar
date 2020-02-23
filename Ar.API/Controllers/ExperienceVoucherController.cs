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

namespace Ar.API.Controllers
{
    public class ExperienceVoucherController : BaseApiController
    {
        /// <summary>
        /// 获取体验卷
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/ExperienceVoucher/GetExperienceVoucherList
        [HttpGet]
        public IHttpActionResult GetExperienceVoucherList()
        {
            SimpleResult result = new SimpleResult();
            IExperienceVoucherService _service = new ExperienceVoucherService();
            try
            {

                if (UserAuthorization)
                {
                    var list = _service.GetExperienceVoucherList();
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

        /// <summary>
        /// 获取体验卷通过编码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/ExperienceVoucher/GetExperienceVoucherByCode?code=1
        [HttpGet]
        public IHttpActionResult GetExperienceVoucherByCode(string code)
        {
            SimpleResult result = new SimpleResult();
            IExperienceVoucherService _service = new ExperienceVoucherService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetExperienceVoucherByCode(code);
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
        /// <summary>
        /// 体验卷
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/ExperienceVoucher/Insert
        [HttpPost]
        public IHttpActionResult Insert(ExperienceVoucher experienceVoucherv)
        {
            SimpleResult result = new SimpleResult();
            IExperienceVoucherService _service = new ExperienceVoucherService();
            try
            {
                if (UserAuthorization)
                {
                    _service.Insert(experienceVoucherv);
                    result.Resource = null;
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