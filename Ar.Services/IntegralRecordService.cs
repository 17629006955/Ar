using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;
using System;

namespace Ar.Services
{
    //积分记录
    public class IntegralRecordService : IIntegralRecordService
    {
        public IList<IntegralRecord> GetIntegralRecordByUserCode(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@UserCode", userCode, System.Data.DbType.String);
            IList<IntegralRecord> list = DapperSqlHelper.FindToList<IntegralRecord>("select * from [dbo].[IntegralRecord] where UserCode=@UserCode", paras, false);
            return list;
        }
        public IntegralRecord GetStoreByCode(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@code", code, System.Data.DbType.String);
            IntegralRecord list = DapperSqlHelper.FindOne<IntegralRecord>("select * from [dbo].[IntegralRecord] where IntegralRecordCode=@code", null,false);
            return list;
        }
        public int CreateUserStore(IntegralRecord record)
        {
            DynamicParameters paras = new DynamicParameters();
            if (string.IsNullOrEmpty(record.IntegralRecordCode))
            {
                record.IntegralRecordCode = GetMaxCode();
            }
            paras.Add("@IntegralRecordCode", record.IntegralRecordCode, System.Data.DbType.String);
            paras.Add("@UserCode", record.UserCode, System.Data.DbType.String);
            paras.Add("@RecordType", record.RecordType, System.Data.DbType.String);
            paras.Add("@Integral", record.Integral, System.Data.DbType.Int32);
            paras.Add("@Explain", record.Explain, System.Data.DbType.String);

            return DapperSqlHelper.ExcuteNonQuery<IntegralRecord>(@"INSERT INTO [dbo].[IntegralRecord]
                     ([IntegralRecordCode],[UserCode],[RecordType]  ,[Integral],[Explain],[CreateTime]  ) 
            VALUES  ( @IntegralRecordCode, 
                      @UserCode, 
                      @RecordType, 
                      @Integral,
                      @Explain,getdate())", paras, false);


        }
        public string GetMaxCode()
        {
            var record = DapperSqlHelper.FindOne<IntegralRecord>("SELECT MAX(IntegralRecordCode) IntegralRecordCode FROM [dbo].[IntegralRecord]", null, false);
            var code = record != null ? Convert.ToInt32(record.IntegralRecordCode) + 1 : 1;
            return code.ToString();
        }
    }
}
