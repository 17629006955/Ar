using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;

namespace Ar.Services
{
    public class ListTypeService : IListTypeService
    {
        public IList<ListType> GetListType()
        {
            IList<ListType> list = DapperSqlHelper.FindToList<ListType>("select * from [dbo].[ListType] where Status=1",null,false);
            return list;
        }
    }
}
