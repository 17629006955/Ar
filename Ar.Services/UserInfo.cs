using System;
using System.Collections.Generic;
using System.Linq;
using Ar.IServices;
using Ar.Repository;
using AR.Model;
using Dapper;

namespace Ar.Services
{
   public  class UserInfo:IUserInfo
    {
       
       public  bool LogIn(string userName, string pwd)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userName",userName,System.Data.DbType.String);
            paras.Add("@pwd", pwd, System.Data.DbType.String);
           var userone= DapperSqlHelper.ExecuteScalar<User>("select * from [dbo].[User] where UserName=@userName and phone=@pwd", paras,false);
            
            return userone==null?false:true;
        }

        public User GetUserByphone(string phone)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@phone", phone, System.Data.DbType.String);
            User userone = DapperSqlHelper.FindOne<User>("select * from [dbo].[User] where phone=@phone", paras, false);

            return userone;
        }
        public User GetUserByCode(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@Code", code, System.Data.DbType.String);
            User userone = DapperSqlHelper.FindOne<User>("select * from [dbo].[User] where Code=@Code", paras, false);

            return userone;
        }
        public int  CreateUser(User user)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@Code", user.Code, System.Data.DbType.String);
            paras.Add("@UserName", user.UserName, System.Data.DbType.String);
            paras.Add("@UserIamgeUrl", user.UserIamgeUrl, System.Data.DbType.String);
            paras.Add("@Sex", user.Sex, System.Data.DbType.Int32);
            paras.Add("@CreateTime", user.CreateTime, System.Data.DbType.DateTime);

            return DapperSqlHelper.ExcuteNonQuery<User>(@"INSERT INTO [dbo].[User]([Code],[UserName],[UserIamgeUrl]  ,[Sex] ,[CreateTime] ) 
            VALUES  ( @Code, 
                      @UserName, 
                      @UserIamgeUrl, 
                      @Sex, 
                      @CreateTime )", paras, false);

           
        }
        public int UpdateByPhone(string userCode ,string  phone, DateTime birthday, string ReferenceNumber ,string recommendedPhone =null)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@phone", phone, System.Data.DbType.String);
            paras.Add("@Code", userCode, System.Data.DbType.String);
            paras.Add("@birthday", birthday, System.Data.DbType.DateTime);
            paras.Add("@ReferenceNumber", ReferenceNumber, System.Data.DbType.String);
            if (!string.IsNullOrEmpty(recommendedPhone))
            {
                paras.Add("@recommendedPhone", birthday, System.Data.DbType.DateTime);
                return DapperSqlHelper.ExcuteNonQuery<User>(@"Update  [dbo].[User] set phone=@phone,birthday=@birthday,ReferenceNumber=@ReferenceNumber ,recommendedPhone=@recommendedPhone  where Code=@Code", paras, false);
            }
            else {
                return DapperSqlHelper.ExcuteNonQuery<User>(@"Update  [dbo].[User] set phone=@phone,birthday=@birthday,ReferenceNumber=@ReferenceNumber where Code=@Code", paras, false);

            }
        }
        public int UpdateByuserCodePhone(string userCode, string phone, DateTime birthday)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@phone", phone, System.Data.DbType.String);
            paras.Add("@Code", userCode, System.Data.DbType.String);
            paras.Add("@birthday", birthday, System.Data.DbType.DateTime);
                return DapperSqlHelper.ExcuteNonQuery<User>(@"Update  [dbo].[User] set phone=@phone,birthday=@birthday where Code=@Code", paras, false);

            
        }
        public int UpdateIsMemberByuserCode(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@IsMember", true, System.Data.DbType.Boolean);
            paras.Add("@Code", userCode, System.Data.DbType.String);
            return DapperSqlHelper.ExcuteNonQuery<User>(@"Update  [dbo].[User] set IsMember=@IsMember where Code=@Code", paras, false);


        }
        public int UpdateReferenceNumber(string userCode, string ReferenceNumber)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@ReferenceNumber", ReferenceNumber, System.Data.DbType.String);
            paras.Add("@Code", userCode, System.Data.DbType.String);
           return DapperSqlHelper.ExcuteNonQuery<User>(@"Update  [dbo].[User] set ReferenceNumber=@ReferenceNumber where Code=@Code", paras, false);

        }
    }
}
