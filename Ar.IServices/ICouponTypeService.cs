using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    public interface ICouponTypeService
    {
        CouponType GetCouponTypeByCode(string code);
        IList<CouponType> GetCouponTypeList();
        CouponType GetCouponTypeByIsGivedType();

    }
}
