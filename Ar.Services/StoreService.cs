using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    public class StoreService : IStoreService
    {
        public Store GetStore(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@storeCode", code, System.Data.DbType.String);
            Store list = DapperSqlHelper.FindOne<Store>("select * from [dbo].[Store] where StoreCode=@storeCode", paras, false);
            return list;
        }
        public IList<Store> GetStoreList()
        {
            IList<Store> list = DapperSqlHelper.FindToList<Store>("select * from [dbo].[Store]", null,false);
            return list;
        }
        public int UpdateStoreaccessToken(Store store)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@accessToken", store.accessToken, System.Data.DbType.String);
            paras.Add("@jsapi_ticket", store.jsapi_ticket, System.Data.DbType.String);
            paras.Add("@accessTokenCreateTime", store.accessTokenCreateTime, System.Data.DbType.DateTime);
            paras.Add("@StoreCode", store.StoreCode, System.Data.DbType.String);
            string sql = @"update [dbo].[Store] set  accessToken=@accessToken,jsapi_ticket=@jsapi_ticket,accessTokenCreateTime=@accessTokenCreateTime
                        where StoreCode=@StoreCode";
            return DapperSqlHelper.ExcuteNonQuery<Store>(sql, paras, false);
        }
    }
}
