using Ar.Model;
using AR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.IServices
{
    /// <summary>
    /// 页签
    /// </summary>
    public interface IListTypeService
    {
        //获取页签
        IList<ListType> GetListType();
    }
}
