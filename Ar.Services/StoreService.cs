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
    }
}
