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
    /// 产品信息
    /// </summary>
    public interface IProductInfoService
    {
        /// <summary>
        /// 获取产品信息列表
        /// </summary>
        /// <returns></returns>
        IList<ProductInfo> GetProductInfoList();

         bool IsExistProduct(string code);
        /// <summary>
        /// 根据code获取产品信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ProductInfo GetProductInfo(string code);

        /// <summary>
        /// 根据类别code获取产品信息
        /// </summary>
        /// <param name="listCode"></param>
        /// <returns></returns>
        IList<ProductInfo> GetProductInfoListByListCode(string listCode);
        /// <summary>
        /// 获取预定页面信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        PayPage GetPayPage(string productCode);
    }
}
