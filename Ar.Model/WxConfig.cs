using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Model
{
   public class WxConfig
    {
        public bool debug = true; // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
        public string appId; // 必填，公众号的唯一标识
        public string timestamp;// 必填，生成签名的时间戳
        public string nonceStr; // 必填，生成签名的随机串
        public string signature;// 必填，签名
        public List<string> jsApiList; // 必填，需要使用的JS接口列表
    }
}
