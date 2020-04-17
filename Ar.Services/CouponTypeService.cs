using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    public class CouponTypeService : ICouponTypeService
    {
        public CouponType GetCouponTypeByCode(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@code", code, System.Data.DbType.String);
            CouponType record = DapperSqlHelper.FindOne<CouponType>("select * from [dbo].[CouponType] where  CouponTypeCode=@code", paras, false);
            return record;
        }
        public CouponType GetCouponTypeByIsGivedType(string taskcode)
        {
            DynamicParameters paras = new DynamicParameters();
            CouponType record = DapperSqlHelper.FindOne<CouponType>("select * from [dbo].[CouponType] where  IsGivedType=1 and TaskType=taskcode", paras, false);
            return record;
        }

        public IList<CouponType> GetCouponTypeList()
        {
            DynamicParameters paras = new DynamicParameters();
            IList<CouponType> list = DapperSqlHelper.FindToList<CouponType>(@"select * from [dbo].[CouponType]  where  Status=1", null, false);
            return list;
        }
    }
}
