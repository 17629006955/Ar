using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    public interface IRechargeTypeService
    {
        RechargeType GetRechargeTypeByCode(string code);
        IList<RechargeType> GetRechargeTypeList();

    }
}
