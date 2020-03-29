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
using Ar.Common;

namespace Ar.API.Controllers
{
    /// <summary>
    /// 会员积分
    /// </summary>
    public class MembershipScoreController : BaseApiController
    {
        /// <summary>
        /// 获取积分
        /// </summary>
        /// <returns></returns>
        ////http://localhost:10010//api/MembershipScore/GetMembershipScoreByCode?code=1
        [HttpGet]
        public IHttpActionResult GetMembershipScoreByCode(string code)
        {
            SimpleResult result = new SimpleResult();
            IMembershipScoreService _service = new MembershipScoreService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetMembershipScoreByCode(code);
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
                LogHelper.WriteLog("GetMembershipScoreByCode获取积分code："+code + ex.Message);
                LogHelper.WriteLog("GetMembershipScoreByCode获取积分code："+code + ex.StackTrace);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }

        /// <summary>
        /// 获取积分
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/MembershipScore/GetMembershipScoreListByUserCode?userCode=1
        [HttpGet]
        public IHttpActionResult GetMembershipScoreListByUserCode(string userCode)
        {
            SimpleResult result = new SimpleResult();
            IMembershipScoreService _service = new MembershipScoreService();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetMembershipScoreListByUserCode(userCode);
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
                LogHelper.WriteLog("GetMembershipScoreListByUserCode获取积分usercode：" +userCode+ ex.Message);
                LogHelper.WriteLog("GetMembershipScoreListByUserCode获取积分usercode：" + userCode + ex.StackTrace);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/MembershipScore/Insert
        [HttpPost]
        public IHttpActionResult Insert(MembershipScore membershipScore)
        {
            SimpleResult result = new SimpleResult();
            IMembershipScoreService _service = new MembershipScoreService();
            try
            {
                if (UserAuthorization)
                {
                    _service.Insert(membershipScore);
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
                LogHelper.WriteLog("Insert获取积分："  + ex.Message);
                LogHelper.WriteLog("Insert获取积分："  + ex.StackTrace);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ////http://localhost:10010//api/MembershipScore/Update?userCode=1&score=100
        [HttpPost]
        public IHttpActionResult Update(string userCode,int score)
        {
            SimpleResult result = new SimpleResult();
            IMembershipScoreService _service = new MembershipScoreService();
            try
            {
                if (UserAuthorization)
                {
                    _service.Update(userCode,score);
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
                LogHelper.WriteLog("Update获取积分：userCode:" +userCode+",score;"+score+ ex.Message);
                LogHelper.WriteLog("Update获取积分：userCode:" + userCode + ",score;" + score + ex.StackTrace);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            return Json(result);

        }
    }
}