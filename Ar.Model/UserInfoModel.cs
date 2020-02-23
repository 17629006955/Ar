using AR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Model
{
   public class UserInfoModel
    {
        //用户信息
       public User user { get; set; }
        //客服信息
        public CustomerService customerService { get; set; }
        //未使用优惠券信息
        public int coupons { get; set; }
        //未使用的订单数据
        public int orders { get; set; }
   
       



    }
}
