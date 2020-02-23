using Ar.IServices;
using Ar.Repository;
using AR.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Services
{
    /// <summary>
    /// 会员店铺注册信息
    /// </summary>
    public class UserStoreService:IUserStoreService
    {
        public UserStore GetUserStoreby(string openId)
        {
         
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@OpenID", openId, System.Data.DbType.String);
            UserStore  userone = DapperSqlHelper.FindOne<UserStore>("select * from [dbo].[UserStore] where   OpenID=@OpenID", paras, false);
            
            return userone;
        }
        public int  CreateUserStore(UserStore userStore)
        {
            DynamicParameters paras = new DynamicParameters();
            if (string.IsNullOrEmpty(userStore.UserStoreCode))
            {
                userStore.UserStoreCode = GetMaxCode();
            }
            paras.Add("@UserStoreCode", userStore.UserStoreCode, System.Data.DbType.String);
            paras.Add("@OpenID", userStore.OpenID, System.Data.DbType.String);
            paras.Add("@UserCode", userStore.UserCode, System.Data.DbType.String);
            paras.Add("@MembershipCardStore", userStore.MembershipCardStore, System.Data.DbType.Int32);
          

            return DapperSqlHelper.ExcuteNonQuery<User>(@"INSERT INTO [dbo].[UserStore]([UserStoreCode],[OpenID],[UserCode]  ,[MembershipCardStore]  ) 
            VALUES  ( @UserStoreCode, 
                      @OpenID, 
                      @UserCode, 
                      @MembershipCardStore)", paras, false);


        }
        public string GetMaxCode()
        {
            var userStore = DapperSqlHelper.FindOne<UserStore>("SELECT MAX(UserStoreCode) UserStoreCode FROM [dbo].[UserStore]", null, false);
            var code = userStore != null ? Convert.ToInt32(userStore.UserStoreCode) + 1 : 1;
            return code.ToString();
        }
    }
}
