﻿using Ar.Model;
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
        int UpdateByPhone(string userCode, string phone);

    }
}
