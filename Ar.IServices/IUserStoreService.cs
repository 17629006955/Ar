using AR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.IServices
{
    public interface IUserStoreService
    {
        UserStore GetUserStoreby(string openId);
        int CreateUserStore(UserStore userStore);
    }
}
