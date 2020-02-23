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
    /// 轮询
    /// </summary>
    public interface IPropagandaService
    {
        //获取轮询
        IList<Propaganda> GetPropaganda();
    }
}
