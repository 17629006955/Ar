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
    public class VerificationService : IVerificationService
    {
        public bool CheckVerification(string phone,string verificationCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@phone", phone, System.Data.DbType.String);
            paras.Add("@verificationCode", verificationCode, System.Data.DbType.String);
            paras.Add("@CreateTime", DateTime.Now.AddMinutes(-15), System.Data.DbType.DateTime);
            Verification verification = DapperSqlHelper.FindOne<Verification>("select * from [dbo].[Verification] where phone=@phone and verificationCode=@verificationCode and CreateTime > @CreateTime ", paras, false);
            if (verification != null)
            {
                return true;
            }
            else
            { return false;
            }
        }
        public int CreateVerification(Verification verification)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@Code", verification.code, System.Data.DbType.String);
            paras.Add("@phone", verification.Phone, System.Data.DbType.String);
            paras.Add("@verificationCode", verification.VerificationCode, System.Data.DbType.String);
            paras.Add("@CreateTime", DateTime.Now, System.Data.DbType.DateTime);
            return DapperSqlHelper.ExcuteNonQuery<Verification>(@"INSERT INTO [dbo].[Verification]([Code],[phone],[verificationCode] ,[CreateTime] ) 
            VALUES  ( @Code, 
                      @phone, 
                      @verificationCode, 
                      @CreateTime )", paras, false);
          
        }
        public int Delete(string phone)
        {
            DynamicParameters paras = new DynamicParameters();
           
            paras.Add("@phone", phone, System.Data.DbType.String);
           
            return DapperSqlHelper.ExcuteNonQuery<Verification>(@"delete [dbo].[Verification]  where phone=@phone", paras, false);
            

        }
    }
}
