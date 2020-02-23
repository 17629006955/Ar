using Ar.Model;
using AR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.IServices
{
    public interface ICustomerServiceS
    {
        IList<CustomerService> GetCustomerServiceList();
        CustomerService GetCustomerService(string code);
    
    }
}
