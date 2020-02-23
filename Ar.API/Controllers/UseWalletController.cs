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
    public class UseWalletController : BaseApiController
    {
        /// <summary>
        /// 获取会员钱包
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/UseWallet/GetUseWalletList
        [HttpGet]
        public IHttpActionResult GetUseWalletList()
        {
            SimpleResult result = new SimpleResult();
            IUseWalletService _service = new UseWalletService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetUseWalletList();
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
        /// 获取会员钱包通过用户
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/UseWallet/GetUseWalletUserCode?userCode=1
        [HttpGet]
        public IHttpActionResult GetUseWalletUserCode(string userCode)
        {
            SimpleResult result = new SimpleResult();
            IUseWalletService _service = new UseWalletService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetUseWallet(userCode);
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
        /// 插入会员钱包
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/UseWallet/InsertUseWalle
        [HttpPost]
        public IHttpActionResult InsertUseWallet(UseWallet useWallet)
        {
            SimpleResult result = new SimpleResult();
            IUseWalletService _service = new UseWalletService();
            try
            {
                var list = _service.InsertUseWallet(useWallet);
                result.Resource = list;
                result.Status = Result.SUCCEED;
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