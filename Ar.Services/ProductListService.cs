using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    public class ProductListService : IProductListService
    {
        public IList<ProductList> GetProductList(string listCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@listCode", listCode, System.Data.DbType.String);
            IList<ProductList> list = DapperSqlHelper.FindToList<ProductList>("select * from [dbo].[ProductList] where ListCode=@listCode and Status =1", paras, false);
            return list;
        }
    }
}
