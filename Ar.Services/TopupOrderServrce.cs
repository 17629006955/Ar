using Ar.IServices;
using Ar.Repository;
using AR.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxPayAPI;

namespace Ar.Services
{
    public class TopupOrderServrce: ITopupOrderServrce
    {
        public TopupOrder GetTopupOrderbyWallePrCode (string WallePrCode)
        {

            DynamicParameters paras = new DynamicParameters();
            paras.Add("@WallePrCode", WallePrCode, System.Data.DbType.String);
            TopupOrder userone = DapperSqlHelper.FindOne<TopupOrder>("select * from [dbo].[TopupOrder] where   WallePrCode=@WallePrCode", paras, false);

            return userone;
        }
      
        public void InsertTopupOrder(string userCode, string prepayid)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@TopupOrderCode", Guid.NewGuid().ToString(), System.Data.DbType.String);
            paras.Add("@UserCode", userCode, System.Data.DbType.String);
            paras.Add("@RecordsOfConsumptionCode", Guid.NewGuid().ToString(), System.Data.DbType.String);
            paras.Add("@WallePrCode", prepayid, System.Data.DbType.String);
            paras.Add("@OutTradeNo", WxPayApi.GenerateOutTradeNo().ToString(), System.Data.DbType.String);
            paras.Add("@PayDatetime", null, System.Data.DbType.DateTime);
            string sql= @"insert into [dbo].[TopupOrder](TopupOrderCode,UserCode,RecordsOfConsumptionCode,
                    WallePrCode,OutTradeNo,PayDatetime)
                    values(@TopupOrderCode,@UserCode,@RecordsOfConsumptionCode,@WallePrCode,
                    @OutTradeNo,@PayDatetime)";
            DapperSqlHelper.ExcuteNonQuery<Order>(sql, paras, false);
        }

        public void UpdateTopupOrder(string prepayid,DateTime payDatetime)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@PayDatetime", payDatetime, System.Data.DbType.String);
            paras.Add("@WallePrCode", prepayid, System.Data.DbType.DateTime);
            string sql = @"update [dbo].[TopupOrder] set PayDatetime=@PayDatetime
            where  WallePrCode=@WallePrCode";
            DapperSqlHelper.ExcuteNonQuery<Order>(sql, paras, false);
        }
    }
}
