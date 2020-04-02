using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Model
{
   public class cardModel
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public List<card> card_list { get; set; }
        public string user_card_status { get; set; }
      
    }
    public class card
    {
        public string card_id { get; set; }
        public string code { get; set; }
    }
    public class Getcardlist
    {
        public string openid { get; set; }
        public string card_id { get; set; }
    }
    public class Getcardstatus
    {
        public string card_id { get; set; }
        public string code { get; set; }
        public bool check_consume { get; set; }
    }
    

}
