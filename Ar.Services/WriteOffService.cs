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
    public class WriteOffService : IWriteOffService
    {
        public bool CheckWriteOff(string orderCode, string writeOffCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@orderCode", orderCode, System.Data.DbType.String);
            paras.Add("@writeOffCode", writeOffCode, System.Data.DbType.String);
            paras.Add("@CreateTime", DateTime.Now.AddMinutes(-5), System.Data.DbType.DateTime);
            WriteOff writeOff = DapperSqlHelper.FindOne<WriteOff>("select * from [dbo].[WriteOff] where orderCode=@orderCode and writeOffCode=@writeOffCode and CreateTime > @CreateTime ", paras, false);
            if (writeOff != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int CreateWriteOff(WriteOff writeOff)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@writeOffCode", writeOff.WriteOffCode, System.Data.DbType.String);
            paras.Add("@orderCode", writeOff.OrderCode, System.Data.DbType.String);
            paras.Add("@CreateTime", DateTime.Now, System.Data.DbType.DateTime);
            return DapperSqlHelper.ExcuteNonQuery<WriteOff>(@"INSERT INTO [dbo].[WriteOff]([WriteOffCode],[OrderCode],[CreateTime] ) 
            VALUES  ( @writeOffCode, 
                      @orderCode, 
                      @CreateTime )", paras, false);

        }
        public int Delete(string orderCode)
        {
            DynamicParameters paras = new DynamicParameters();

            paras.Add("@orderCode", orderCode, System.Data.DbType.String);

            return DapperSqlHelper.ExcuteNonQuery<Verification>(@"delete [dbo].[WriteOff]  where orderCode=@orderCode", paras, false);


        }
    }
}
