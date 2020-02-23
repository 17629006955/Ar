using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Auth;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Regions;
using Aliyun.Acs.Core.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Model
{
    public class SendSmsRequest 
    {
      
        public string PhoneNumbers { get; set; }
        //必填:短信签名-可在短信控制台中找到
        public string SignName { get; set; }
        //必填:短信模板-可在短信控制台中找到，发送国际/港澳台消息时，请使用国际/港澳台短信模版
        public string TemplateCode { get; set; }
        //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
        public string TemplateParam { get; set; }
        //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
        public string OutId { get; set; }

    }
}
