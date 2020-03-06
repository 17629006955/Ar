using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    public interface ICouponService
    {
        object GetCoupon(string userCode);
        Coupon GetCouponByCode(string code);
        IList<Coupon> GetCouponList(string userCode);
        bool UsedUpdate(string couponCode,string userCode);
        bool GiveedUpdate(string couponCode, string userCode);
        bool Insert(Coupon coupon);

        int Exist(string code);

        bool InsertCouponByUser(string couponCode, string userCode);

    }
}
