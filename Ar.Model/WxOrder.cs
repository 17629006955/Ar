using AR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxPayAPI;

namespace Ar.Model
{
    public class WxOrder
    {
        public Order order;
        public string wxJsApiParam;
        public string prepayid;
    }
}
