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
    public class CouponController : BaseApiController
    {
        /// <summary>
        /// 获取优惠券
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/Coupon/GetCoupon
        [HttpGet]
        public IHttpActionResult GetCoupon()
        {
            SimpleResult result = new SimpleResult();
            ICouponService _service = new CouponService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetCoupon();
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
        /// 获取优惠券
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/Coupon/GetCouponByCode?code=1
        [HttpGet]
        public IHttpActionResult GetCouponByCode(string code)
        {
            SimpleResult result = new SimpleResult();
            ICouponService _service = new CouponService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetCouponByCode(code);
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
        /// 获取优惠券
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Coupon/GetCouponList?userCode=1
        [HttpGet]
        public IHttpActionResult GetCouponList(string userCode)
        {
            SimpleResult result = new SimpleResult();
            ICouponService _service = new CouponService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetCouponList(userCode);
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
        /// 给用户添加优惠卷
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Coupon/InsertCouponByUser?couponCode=1&userCode=1
        [HttpGet]
        public IHttpActionResult InsertCouponByUser(string couponCode,string userCode)
        {
            SimpleResult result = new SimpleResult();
            ICouponService _service = new CouponService();
            try
            {
                if (UserAuthorization)
                {
                    var re = _service.InsertCouponByUser(couponCode,userCode);
                    result.Resource = re;
                    result.Status = re?Result.SUCCEED: Result.FAILURE;
                    result.Msg = re ?"":"优惠卷不存在！";
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
        /// 获取优惠券
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/Coupon/GiveedUpdate?couponCode=c0e2af18-3019-4ba7-82b1-9dadba34b07d&userCode=1
        [HttpGet]
        public IHttpActionResult GiveedUpdate(string couponCode, string userCode)
        {
            SimpleResult result = new SimpleResult();
            ICouponService _service = new CouponService();
            try
            {
                if (UserAuthorization)
                {
                    bool re = _service.GiveedUpdate(couponCode,userCode);
                    result.Resource = re;
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