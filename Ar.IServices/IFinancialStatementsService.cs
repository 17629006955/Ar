using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    public interface IFinancialStatementsService
    {

        bool Insert(financialStatements model);
        financialStatements getData(string userCode, Order order, string payType);
      
        financialStatements getDataRechargeRecord(string userCode, string typeCode, UseWallet useWallet, string storeCode, decimal? ratio, string payType);

        financialStatements getWriteOff(string userCode, string orderCode, string payType);

    }
}
