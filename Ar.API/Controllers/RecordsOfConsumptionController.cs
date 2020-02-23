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

        /// <summary>
        ///购买支付订单
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/RecordsOfConsumption/PayOrder?productCode=1&userCode=1&peopleCount=1&dateTime=2019-12-07
        [HttpGet]
        public IHttpActionResult PayOrder(string productCode, string userCode, string peopleCount, DateTime dateTime)
        {
            SimpleResult result = new SimpleResult();
            IRecordsOfConsumptionService _service = new RecordsOfConsumptionService();
            try
            {
                if (UserAuthorization)
                {
                     _service.PayOrder(productCode,userCode,peopleCount,dateTime);
                    result.Resource = true;
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