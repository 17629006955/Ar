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
    /// 门店
    /// </summary>
    public interface IStoreService
    {
        /// <summary>
        /// 门店列表
        /// </summary>
        /// <returns></returns>
        IList<Store> GetStoreList();

        /// <summary>
        /// 门店详情
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Store GetStore(string code);
    }
}
