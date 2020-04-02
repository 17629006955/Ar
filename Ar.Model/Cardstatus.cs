using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Model
{
    public class Cardstatus
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public card card { get; set; }
        public string user_card_status { get; set; }
    }
}
