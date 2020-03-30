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
    public class CouponTypeController : BaseApiController
    {
        /// <summary>
        /// 获取优惠券
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/CouponType/GetCouponTypeByCode?code=1
        [HttpGet]
        public IHttpActionResult GetCouponTypeByCode(string code)
        {
            LogHelper.WriteLog("GetCouponTypeByCode strat");
            LogHelper.WriteLog("GetCouponTypeByCode code" + code);
            SimpleResult result = new SimpleResult();
            ICouponTypeService _service = new CouponTypeService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetCouponTypeByCode(code);
                result.Resource = list;
                    LogHelper.WriteLog("GetCouponTypeByCode list"+ list);
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
                LogHelper.WriteLog("GetCouponTypeByCode获取优惠卷类型code" + code + ex.Message,ex);
                LogHelper.WriteLog("GetCouponTypeByCode获取优惠卷类型code" + code +ex.StackTrace,ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetCouponTypeByCode end");
            return Json(result);

        }

        /// <summary>
        /// 获取优惠券
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/CouponType/GetCouponTypeList
        [HttpGet]
        public IHttpActionResult GetCouponTypeList()
        {
            LogHelper.WriteLog("GetCouponTypeList strat");
            SimpleResult result = new SimpleResult();
            ICouponTypeService _service = new CouponTypeService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetCouponTypeList();
                    LogHelper.WriteLog("GetCouponTypeList list"+ list);
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
                LogHelper.WriteLog("GetCouponTypeList获取优惠卷类型" + ex.Message,ex);
                LogHelper.WriteLog("GetCouponTypeList获取优惠卷类型"  +ex.StackTrace,ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetCouponTypeList result"+ Json(result));
            LogHelper.WriteLog("GetCouponTypeList end");
            return Json(result);

        }

    }
}