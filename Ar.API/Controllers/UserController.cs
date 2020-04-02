using Ar.API.Controllers.BaseContolles;
using Ar.Common;
using Ar.IServices;
using Ar.Model;
using Ar.Model.BaseResult;
using Ar.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Ar.API.Controllers
{
    public class UserController : BaseApiController
    {

        //http://localhost:10010//api/WeixinUser/access_token
        [HttpGet]
        public IHttpActionResult Signin(string SigninCode)
        {

            SimpleResult result = new SimpleResult();
            result.Status = Result.SUCCEED;  
            return Json(result);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        //http://localhost:10010//api/User/GetUserByCode?code=1
        [HttpGet]
        public IHttpActionResult GetUserByCode(string code)
        {
            LogHelper.WriteLog("GetUserByCode code" + code);
            SimpleResult result = new SimpleResult();
            IUserInfo _service = new UserInfo();
            try
            {
                if (UserAuthorization)
                {
                    var list = _service.GetUserByCode(code);
                    result.Resource = list;
                    result.Status = Result.SUCCEED;
                }
                else
                {
                    result.Status = ResultType;
                    result.Resource = ReAccessToken;
                    result.Msg=TokenMessage;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("GetUserByCode code" + code , ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetUserByCode result" + Json(result));
            return Json(result);
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        //http://localhost:10010//api/User/GetUserInfoByCode?usercode=1&store=1
        [HttpGet]
        public IHttpActionResult GetUserInfoByCode(string usercode,string store)
        {
            LogHelper.WriteLog("GetUserInfoByCode usercode" + usercode);
            LogHelper.WriteLog("GetUserInfoByCode store" + store);
            SimpleResult result = new SimpleResult();
            IUserInfo _service = new UserInfo();
            ICustomerServiceS _customerServiceS = new CustomerServiceS();
            IOrderService _OrderService = new OrderService();
            ICouponService _CouponService = new CouponService();
            UserInfoModel userInfo = new UserInfoModel();
            IUseWalletService _useWalletService = new UseWalletService();
            IUserTaskService _userTaskService = new UserTaskService();
            try
            {
                if (UserAuthorization)
                {
                    var user = _service.GetUserByCode(usercode);
                    userInfo.user = user;
                    var customerService = _customerServiceS.GetCustomerService(store);
                    userInfo.customerService = customerService;
                    var orders = _OrderService.GetOrderList(usercode);
                    userInfo.useWalletInfo = _useWalletService.GetUseWalletInfoByUserCode(usercode);
                    var conponList=_CouponService.GetUserCoupon(usercode);
                    userInfo.useCouponCount = conponList.Any()?conponList.Count:0;
                    if (orders != null)
                    {
                        userInfo.orders = orders.Where(p=>p.PayTime!=null && p.IsWriteOff==false).Count();
                    }
                    var coupons = _CouponService.GetCouponList(usercode);
                    if (coupons != null)
                    {
                        userInfo.coupons = coupons.Where(c=>c.IsUsed==false).Count();
                    }
                    var userTask= _userTaskService.GetUserTaskList(user.Code);
                    if (userTask != null)
                    {
                        userInfo.userTask= userTask.Where(t=>t.IsComplete==false).Count();
                    }
                    userInfo.openCard = new OpenCard();
                    var cardId = ConfigurationManager.AppSettings["cardId"].ToString();
                    userInfo.openCard.cardId = cardId;
                    userInfo.openCard.code = userInfo.user?.ReferenceNumber;
                    result.Resource = userInfo;
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
                LogHelper.WriteLog("GetUserInfoByCode usercode" + usercode+ "store"+ store, ex);
                result.Status = Result.FAILURE;
                result.Msg = ex.Message;
            }
            LogHelper.WriteLog("GetUserInfoByCode result" + Json(result));
            return Json(result);
        }

    }

}
