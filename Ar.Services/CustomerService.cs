using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    public class CustomerServiceS : ICustomerServiceS
    {
        public CustomerService GetCustomerService(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@storeCode", code, System.Data.DbType.String);
            CustomerService list = DapperSqlHelper.FindOne<CustomerService>("select * from [dbo].[CustomerService] where StoreCode=@storeCode and Status =1", paras, false);
            return list;
        }
        public IList<CustomerService> GetCustomerServiceList()
        {
            IList<CustomerService> list = DapperSqlHelper.FindToList<CustomerService>("select * from [dbo].[CustomerService]  where Status =1", null,false);
            return list;
        }

    }
}
