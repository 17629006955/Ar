using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    public interface ICouponService
    {
        object GetCoupon(string userCode);
        Coupon GetCouponByOrderCode(string OrderCode);
        
            bool checkCoupon(string userCode);
        Coupon GetCouponByCode(string code);
        IList<Coupon> GetCouponList(string userCode);
        bool UsedUpdate(string couponCode,string userCode,string orderCode);
       
        bool Insert(Coupon coupon);

        int Exist(string code);
        bool UpdatebycouponCode(string couponCode);
        bool InsertCouponByUser(string couponCode, string userCode);

        IList<CouponShow> GetUserCoupon(string userCode);


    }
}
