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
        int InsertTopupOrder(string userCode,string prepayid, string typeCode, decimal? money = 0);
        int UpdateTopupOrder(string prepayid, DateTime? payDatetime);
        IList<TopupOrder> GetTopupOrderbyuserCode(string userCode);
    }
}
