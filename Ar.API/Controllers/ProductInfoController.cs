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
using System.Transactions;

namespace Ar.API.Controllers
{
    public class ProductInfoController : BaseApiController
    {
        /// <summary>
        /// 获取所有产品
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/ProductInfo/GetProductInfoList
        [HttpGet]
        public IHttpActionResult GetProductInfoList()
        {
            LogHelper.WriteLog("GetProductInfoList start");
            SimpleResult result = new SimpleResult();
            IProductInfoService _service = new ProductInfoService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetProductInfoList();
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
                LogHelper.WriteLog("GetProductInfoList ",ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetProductInfoList result" + Json(result));
            return Json(result);

        }

        /// <summary>
        /// 获取产品详情
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/ProductInfo/GetProductInfo?code=1
        [HttpGet]
        public IHttpActionResult GetProductInfo(string code)
        {
            LogHelper.WriteLog("GetProductInfo code" + code);
            SimpleResult result = new SimpleResult();
            IProductInfoService _service = new ProductInfoService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetProductInfo(code);
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
                LogHelper.WriteLog("GetProductInfo code"+ code, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetProductInfo " + Json(result));
            return Json(result);

        }
        /// <summary>
        /// 根据类型获取产品
        /// </summary>
        /// <param name="listCode"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/ProductInfo/GetPayPage?orderCode=1
        [HttpGet]
        public IHttpActionResult GetPayPage(string orderCode)
        {
            LogHelper.WriteLog("GetPayPage " + orderCode);
            SimpleResult result = new SimpleResult();
            IOrderService _orderService = new OrderService();
           IProductInfoService _service = new ProductInfoService();
            ICouponService _couponService = new CouponService();

            string msg = "";
            try
            {
                if (UserAuthorization)
                {
                    var orderInfo = _orderService.GetOrderInfo(orderCode);
                    if (orderInfo == null)
                    {
                        msg = "订单不存在！";
                        result.Status = Result.SYSTEM_ERROR;
                        result.Resource = null;
                    }
                    else if (orderInfo.PayTime != null)
                    {
                        msg = "该订单已付款！";
                        result.Status = Result.SYSTEM_ERROR;
                        result.Resource = null;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(orderInfo.ExperienceVoucherCode))
                        {
                            //var isUseCoupon = _couponService.Exist(orderInfo.ExperienceVoucherCode);
                            //if (isUseCoupon != 3)
                            //{
                            //    orderInfo.ExperienceVoucherCode = "";
                            //}
                            using (var scope = new TransactionScope())//创建事务
                            {
                                _couponService.UpdatebycouponCode(orderInfo.ExperienceVoucherCode);
                            orderInfo.ExperienceVoucherCode = null;
                            _orderService.UpdateOrder(orderInfo);
                                scope.Complete();//这是最后提交事务
                            }
                        }
                        var productInfo = _service.GetProductInfo(orderInfo.ProductCode);
                        result.Status = Result.SUCCEED;
                        result.Resource = new  { orderInfo = orderInfo, productInfo =productInfo};
                    }
                    result.Msg = msg;
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
                LogHelper.WriteLog("GetPayPage orderCode"+ orderCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetPayPage result" + Json(result));
            return Json(result);

        }
        /// <summary>
        /// 根据类型获取产品
        /// </summary>
        /// <param name="listCode"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/ProductInfo/GetProductInfoListByListCode?listCode=1
        [HttpGet]
        public IHttpActionResult GetProductInfoListByListCode(string listCode)
        {
            LogHelper.WriteLog("GetProductInfoListByListCode listCode" + listCode);
            SimpleResult result = new SimpleResult();
            IProductInfoService _service = new ProductInfoService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetProductInfoListByListCode(listCode);
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
                LogHelper.WriteLog("GetProductInfoListByListCode listCode"+ listCode, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetProductInfoListByListCode result" + Json(result));
            return Json(result);
        }
    }
}