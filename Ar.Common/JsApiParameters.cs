namespace WxPayAPI
{
 
        public class JsApiParameters
        {
            public string appId { get; set; }
            public string timeStamp { get; set; }
            public string nonceStr { get; set; }
            public string package { get; set; }

            public string signType { get; set; }
            public string paySign { get; set; }


        }
    
}