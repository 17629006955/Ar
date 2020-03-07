using AR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.IServices
{
    public interface IWriteOffService
    {
        bool CheckWriteOff(string orderCode, string writeOffCode);
        int CreateWriteOff(WriteOff writeOff);
        int Delete(string orderCode);
    }
}
