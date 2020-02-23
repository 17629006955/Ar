using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;

namespace Ar.Services
{
    public class PropagandaService : IPropagandaService
    {
        public IList<Propaganda> GetPropaganda()
        {
            IList<Propaganda> list = DapperSqlHelper.FindToList<Propaganda>("select * from [dbo].[Propaganda] where Status=1", null,false);
            return list;
        }
    }
}
