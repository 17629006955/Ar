﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Model
{
    public class CardExt
    {
        public string code { get; set; }
        public string openid { get; set; }
        public string timestamp { get; set; }
        public string nonce_str { get; set; }
      
        public string outer_str { get; set; }
        public string signature { get; set; }
    }
}
