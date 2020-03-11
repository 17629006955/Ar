using AR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.IServices
{
    public interface ITopupOrderServrce
    {
        TopupOrder GetTopupOrderbyWallePrCode(string WallePrCode);
        void InsertTopupOrder(string userCode,string prepayid);
        void UpdateTopupOrder(string prepayid, DateTime payDatetime);
    }
}
