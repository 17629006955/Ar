using AR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.IServices
{
    public interface ICertificationService
    {
         void CreateUserCertification(Certification certification);
        bool CheckCertification(string OpenID);
        void UpdateUserCertification(Certification certification);
        void UpdateUserCertificationByCertificationCode(Certification certification);
        Certification ExistsUserToken(string token);
        Certification ReAccessToken(string reAccessToken);
    }
}
