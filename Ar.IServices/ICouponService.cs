using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    public interface ICouponService
    {
        Coupon GetCouponByCode(string code);
        IList<Coupon> GetCouponList(string userCode);
        bool UsedUpdate(string couponCode);
        bool GiveedUpdate(string couponCode, string userCode);
        bool Insert(Coupon coupon);

    }
}
