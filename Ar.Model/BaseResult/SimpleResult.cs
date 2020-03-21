using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Model.BaseResult
{
    public class SimpleResult
    {

       

        public Result Status { get; set; }
        /// <summary>
        /// 消息
        /// </summary>

        public string Msg { get; set; }
        public object Resource { get; set; }



    }


       /// 错误码 

        /// 1表示成功 
        /// 
        /// 100***：系统级基础错误   
        /// 
        /// 100000: NULL 
        /// 100001: SIGN_ERROR 
        /// 100002: APP_AUTH_ERROR 
        /// 100003: USER_AUTH_ERROR 
        /// 100004: SYSTEM_ERROR 
        /// 100005: 参数验证失败 
        /// 100006：服务器操作失败 
       ///  
       /// 200***：用户相关错误 
       /// 200001：登录名或密码错误 
       /// 200002: 您的账户已经被禁用，请联系管理员 
       /// 200003: 用户授权失败 
       /// 200004: 文件不合法 
    public enum Result
    {/// <summary>
    /// 成功
    /// </summary>
        SUCCEED = 1,
        
        FAILURE = 2, 
        /// <summary>
        /// 重新微信认证
        /// </summary>
        SIGN_TIMESTAMP_ERROR = 3,
        /// <summary>
        /// 重新认证
        /// </summary>
        USER_AUTH_ERROR = 4,
        SYSTEM_ERROR=5,
        PARAMERS_VERIFY_ERROR=6 , //参数验证失败,
         /// <summary>
         /// 会员认证专用，其他不能使用
         /// </summary>
        PARAMERS_WX = 7  //微信
    }
}
