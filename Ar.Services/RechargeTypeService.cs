using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    /// <summary>
    /// 充值类型
    /// </summary>
    public class RechargeTypeService : IRechargeTypeService
    {
        public RechargeType GetRechargeTypeByCode(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@code", code, System.Data.DbType.String);
            RechargeType task = DapperSqlHelper.FindOne<RechargeType>("select * from [dbo].[RechargeType] where RechargeTypeCode=@code and Status=1", paras, false);
            return task;
        }

        public IList<RechargeType> GetRechargeTypeList()
        {
            DynamicParameters paras = new DynamicParameters();
            IList<RechargeType> list = DapperSqlHelper.FindToList<RechargeType>(@"select * from [dbo].[RechargeType]  where  Status =1", null, false);
            return list;
        }
    }
}
