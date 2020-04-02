using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Model
{
   public  class FescoParameter
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public string List2JSON(List<FescoParameter> objlist)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            bool isone = false;
            foreach (var item in objlist)
            {
                if (isone)
                {
                    sb.Append(",");

                }
                sb.Append("\"");
                sb.Append(item.Key);
                sb.Append("\"");
                sb.Append(":");
                sb.Append("\"");
                sb.Append(item.Value);
                sb.Append("\"");
                isone = true;
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
