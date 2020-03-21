using Ar.Model;
using AR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.IServices
{
    public interface IUserInfo
    {
         bool LogIn(string userName,string pwd);
        User GetUserByphone(string phone);
        User GetUserByCode(string code);
        int CreateUser(User user);
        int UpdateByPhone(string userCode, string phone, DateTime birthday, string ReferenceNumber, string recommendedPhone = null);
        int UpdateReferenceNumber(string userCode, string ReferenceNumber);
        int UpdateByuserCodePhone(string userCode, string phone, DateTime birthday);
        int UpdateIsMemberByuserCode(string userCode);

    }
}
