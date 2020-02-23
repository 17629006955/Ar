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
    public class CertificationService : ICertificationService

    {

        public void CreateUserCertification(Certification certification)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@CertificationCode", certification.CertificationCode, System.Data.DbType.String);
            paras.Add("@OpenID", certification.OpenID, System.Data.DbType.String);
            paras.Add("@AccessToken", certification.AccessToken, System.Data.DbType.String);
            paras.Add("@CreateTime", certification.CreateTime, System.Data.DbType.DateTime);
            paras.Add("@ReAccessToken", certification.ReAccessToken, System.Data.DbType.String);
            Certification userone = DapperSqlHelper.FindOne<Certification>("INSERT INTO[dbo].[Certification] ([CertificationCode]  ,[OpenID] ,[AccessToken] ,[CreateTime],[ReAccessToken]) VALUES (@CertificationCode  ,@OpenID ,@AccessToken,@CreateTime,@ReAccessToken)", paras, false);
        }
        public bool CheckCertification(string OpenID)
        {
            bool checkCertification = false;
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@OpenID", OpenID, System.Data.DbType.String);
            Certification userone = DapperSqlHelper.FindOne<Certification>("SELECT * FROM[dbo].[Certification] WHERE OpenID = @OpenID", paras, false);
            if (userone!=null)
            {
                 checkCertification=true;
            }
            return checkCertification;
        }
        public void UpdateUserCertification(Certification certification)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@CertificationCode", certification.CertificationCode, System.Data.DbType.String);
            paras.Add("@OpenID", certification.OpenID, System.Data.DbType.String);
            paras.Add("@AccessToken", certification.AccessToken, System.Data.DbType.String);
            paras.Add("@ReAccessToken", certification.ReAccessToken, System.Data.DbType.String);
            paras.Add("@CreateTime", certification.CreateTime, System.Data.DbType.DateTime);
            Certification userone = DapperSqlHelper.FindOne<Certification>("UPDATE [dbo].[Certification] SET AccessToken=@AccessToken, ReAccessToken=@ReAccessToken,CreateTime=@CreateTime WHERE OpenID=@OpenID", paras, false);
        }
        public void UpdateUserCertificationByCertificationCode(Certification certification)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@CertificationCode", certification.CertificationCode, System.Data.DbType.String);
            paras.Add("@AccessToken", certification.AccessToken, System.Data.DbType.String);
            paras.Add("@ReAccessToken", certification.ReAccessToken, System.Data.DbType.String);
            paras.Add("@CreateTime", certification.CreateTime, System.Data.DbType.DateTime);
            Certification userone = DapperSqlHelper.FindOne<Certification>("UPDATE [dbo].[Certification] SET AccessToken=@AccessToken, ReAccessToken=@ReAccessToken,CreateTime=@CreateTime WHERE CertificationCode=@CertificationCode", paras, false);
        }
        public Certification ExistsUserToken(string token)
        {
            

                DynamicParameters paras = new DynamicParameters();
                paras.Add("@AccessToken", token, System.Data.DbType.String);
                
            Certification certification = DapperSqlHelper.FindOne<Certification>("SELECT * FROM[dbo].[Certification] WHERE AccessToken = @AccessToken ", paras, false);
               
                return certification;
            
        }
        
        public Certification ReAccessToken(string reAccessToken)
        {


            DynamicParameters paras = new DynamicParameters();
            paras.Add("@ReAccessToken", reAccessToken, System.Data.DbType.String);
            Certification certification = DapperSqlHelper.FindOne<Certification>("SELECT * FROM[dbo].[Certification] WHERE ReAccessToken = @ReAccessToken ", paras, false);

            return certification;

        }
       
    }
}
