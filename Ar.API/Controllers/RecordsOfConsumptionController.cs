﻿using Ar.Common.tool;
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
using System.Transactions;

namespace Ar.API.Controllers
{
    public class RecordsOfConsumptionController : BaseApiController
    {
        /// <summary>
        /// 获取消费记录
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/RecordsOfConsumption/GetRecordsOfConsumptionList
        [HttpGet]
        public IHttpActionResult GetRecordsOfConsumptionList()
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetRecordsOfConsumptionList();
                result.Resource = list;
                result.Status = Result.SUCCEED;
                }
                else
                {
                    result.Status = Result.FAILURE;
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
        /// 获取任务
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RecordsOfConsumption/GetRecordsOfConsumptionByCode?code=1
        [HttpGet]
        public IHttpActionResult GetRecordsOfConsumptionByCode(string code)
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetRecordsOfConsumptionByCode(code);
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
        /// 获取消费记录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RecordsOfConsumption/GetRecordsOfConsumptionByUserCode?userCode=1
        [HttpGet]
        public IHttpActionResult GetRecordsOfConsumptionByUserCode(string userCode)
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetRecordsOfConsumptionListByUserCode(userCode);
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
        ///购买支付订单
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RecordsOfConsumption/PayOrder?productCode=1&userCode=1&peopleCount=1&dateTime=2019-12-07&money=9&couponCode=1ac31b4d-e383-447a-9417-9c66ca9e6004 
        [HttpGet]
        public IHttpActionResult PayOrder(string productCode, string userCode, string peopleCount, DateTime dateTime, decimal money, string couponCode = "")
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            try
            {
                if (UserAuthorization)
                {
                    var re= _service.PayOrder(productCode,userCode,peopleCount,dateTime,money,couponCode);
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
        /// <summary>
        ///微信购买下单订单
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RecordsOfConsumption/PayOrder?productCode=1&userCode=1&peopleCount=1&dateTime=2019-12-07&money=9&couponCode=1ac31b4d-e383-447a-9417-9c66ca9e6004 
        [HttpGet]
        public IHttpActionResult WxPayprOrder(string openid, string storecode, string productCode, string userCode, string peopleCount, DateTime dateTime, decimal money, string couponCode = "")
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            IStoreService _stoeservice = new StoreService();
            ICouponService _couponservice = new CouponService();
            try
            {
                if (UserAuthorization)
                {
                    using (var scope = new TransactionScope())//创建事务
                    {
                        var order = _service.WxPayOrder(productCode, userCode, peopleCount, dateTime, money, couponCode);
                        var store = _stoeservice.GetStore(storecode);
                        var couponser  =  _couponservice.GetCouponByCode(couponCode);
                        //生成微信预支付订单
                        var prepayid = Common.wxPayOrderSomething(openid, money.ToString(), couponser.CouponTypeName, store.StoreName);
                        WxOrder wxorder = new WxOrder();
                        wxorder.order = order;
                        wxorder.prepayid = prepayid;
                        result.Resource = wxorder; ;
                        result.Status = Result.SUCCEED;
                        scope.Complete();//这是最后提交事务
                    }
                    
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
        ///微信支付成功更新订单和优惠券
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RecordsOfConsumption/PayOrder?productCode=1&userCode=1&peopleCount=1&dateTime=2019-12-07&money=9&couponCode=1ac31b4d-e383-447a-9417-9c66ca9e6004 
        [HttpGet]
        public IHttpActionResult WxPayOrder(string userCode, string orderCode, string couponCode = "")
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            IStoreService _stoeservice = new StoreService();
            ICouponService _couponservice = new CouponService();
            try
            {
                if (UserAuthorization)
                {
                    using (var scope = new TransactionScope())//创建事务
                    {
                        IOrderService _orderService = new OrderService();
                        ICouponService _couponService = new CouponService();
                        var order= _orderService.GetOrderByCode(orderCode);
                        order.PayTime = DateTime.Now;
                        _orderService.UpdateOrder(order);
                        _couponService.UsedUpdate(couponCode, userCode);
                        result.Status = Result.SUCCEED;
                        scope.Complete();//这是最后提交事务
                    }

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
        ///订单核销
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RecordsOfConsumption/WriteOff?phone=18235139350&orderCode=1
        [HttpGet]
        public IHttpActionResult WriteOff(string phone, string orderCode)
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            try
            {
                if (UserAuthorization)
                {
                    var re = _service.WriteOff(phone, orderCode);
                    result.Resource = re;
                    result.Status = Result.SUCCEED;
                }
                else
                {
                    result.Status = Result.FAILURE;
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