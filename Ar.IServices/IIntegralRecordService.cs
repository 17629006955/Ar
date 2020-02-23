using Ar.Model;
using AR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.IServices
{
    public interface IIntegralRecordService
    {
        IList<IntegralRecord> GetIntegralRecordByUserCode(string userCode);
        IntegralRecord GetStoreByCode(string code);
        int CreateUserStore(IntegralRecord record);
    }
}
