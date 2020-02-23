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
    /// 获取产品列表关系
    /// </summary>
    public interface IProductListService
    {
        IList<ProductList> GetProductList(string listCode);
    }
}
