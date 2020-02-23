using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    /// <summary>
    /// 体验券
    /// </summary>
    public class ExperienceVoucherService : IExperienceVoucherService
    {
        public ExperienceVoucher GetExperienceVoucherByCode(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@code", code, System.Data.DbType.String);
            ExperienceVoucher v = DapperSqlHelper.FindOne<ExperienceVoucher>("select * from [dbo].[ExperienceVoucher] where ExperienceVoucherCode=@code ", paras, false);
            return v;
        }

        public IList<ExperienceVoucher> GetExperienceVoucherList()
        {
            DynamicParameters paras = new DynamicParameters();
            IList<ExperienceVoucher> list = DapperSqlHelper.FindToList<ExperienceVoucher>(@"select * from [dbo].[ExperienceVoucher]  where  VersionEndTime is null ", null, false);
            return list;
        }

        public void Insert(ExperienceVoucher v)
        {
            DynamicParameters paras = new DynamicParameters();
            if (string.IsNullOrEmpty(v.ExperienceVoucherCode))
            {
                v.ExperienceVoucherCode = GetMaxCode();
            }
            paras.Add("@ExperienceVoucherCode", v.ExperienceVoucherCode, System.Data.DbType.String);
            paras.Add("@ProductCode", v.ProductCode, System.Data.DbType.String);
            paras.Add("@ExperienceNo", v.ExperienceNo, System.Data.DbType.Int32);
            paras.Add("@ExperiencePrice", v.ExperiencePrice, System.Data.DbType.Decimal);
            paras.Add("@VersionStartTime", v.VersionStartTime, System.Data.DbType.DateTime);
            paras.Add("@VersionEndTime", v.VersionEndTime, System.Data.DbType.DateTime);
            string sql = @"insert into [dbo].[ExperienceVoucher](ExperienceVoucherCode,ProductCode,ExperienceNo,ExperiencePrice,VersionStartTime,VersionEndTime)
             values(@ExperienceVoucherCode,@ProductCode,@ExperienceNo,@ExperiencePrice,@VersionStartTime,@VersionEndTime)";
            DapperSqlHelper.ExcuteNonQuery<ExperienceVoucher>(sql, paras, false);
        }

        public string GetMaxCode()
        {
            var v = DapperSqlHelper.FindOne<ExperienceVoucher>("SELECT MAX(ExperienceVoucherCode) ExperienceVoucherCode FROM [dbo].[ExperienceVoucher]", null, false);
            var code = v != null ? Convert.ToInt32(v.ExperienceVoucherCode) + 1 : 1;
            return code.ToString();
        }
    }
}
