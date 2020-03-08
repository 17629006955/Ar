using Ar.Repository;
using AR.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Services
{
    public class TopupOrderServrce
    {
        public TopupOrder GetTopupOrderbyWallePrCode (string WallePrCode)
        {

            DynamicParameters paras = new DynamicParameters();
            paras.Add("@WallePrCode", WallePrCode, System.Data.DbType.String);
            TopupOrder userone = DapperSqlHelper.FindOne<TopupOrder>("select * from [dbo].[TopupOrder] where   WallePrCode=@WallePrCode", paras, false);

            return userone;
        }
        public TopupOrder GetTopupOrderbyWallePrCode(string WallePrCode)
        {

            DynamicParameters paras = new DynamicParameters();
            paras.Add("@WallePrCode", WallePrCode, System.Data.DbType.String);
            TopupOrder userone = DapperSqlHelper.FindOne<TopupOrder>("select * from [dbo].[TopupOrder] where   WallePrCode=@WallePrCode", paras, false);

            return userone;
        }
    }
}
