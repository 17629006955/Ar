using AR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.IServices
{
    public interface IVerificationService
    {
        bool CheckVerification(string phone, string verificationCode);
        int Delete(string phone);
        int CreateVerification(Verification verification);
    }
}
