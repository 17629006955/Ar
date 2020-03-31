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
        public IList<TopupOrder> GetTopupOrderbyuserCode(string userCode)
        {

            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            IList<TopupOrder> userone = DapperSqlHelper.FindToList<TopupOrder>("select * from [dbo].[TopupOrder] where   userCode=@userCode", paras, false);

            return userone;
        }

        public int InsertTopupOrder(string userCode, string prepayid, string typeCode, decimal? money = 0)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@TopupOrderCode", Guid.NewGuid().ToString(), System.Data.DbType.String);
            paras.Add("@UserCode", userCode, System.Data.DbType.String);
            paras.Add("@RecordsOfConsumptionCode", Guid.NewGuid().ToString(), System.Data.DbType.String);
            paras.Add("@WallePrCode", prepayid, System.Data.DbType.String);
            paras.Add("@OutTradeNo", WxPayApi.GenerateOutTradeNo().ToString(), System.Data.DbType.String);
            paras.Add("@PayDatetime", null, System.Data.DbType.DateTime);
            paras.Add("@RechargeTypeCode", typeCode, System.Data.DbType.String);
            paras.Add("@RecordsMoney", money, System.Data.DbType.String);
            paras.Add("@CreateTime", null, System.Data.DbType.DateTime);
            string sql= @"insert into [dbo].[TopupOrder](TopupOrderCode,UserCode,RecordsOfConsumptionCode,
                    WallePrCode,OutTradeNo,PayDatetime,RechargeTypeCode,RecordsMoney,CreateTime)
                    values(@TopupOrderCode,@UserCode,@RecordsOfConsumptionCode,@WallePrCode,
                    @OutTradeNo,@PayDatetime,@RechargeTypeCode,@RecordsMoney,@CreateTime)";
           return  DapperSqlHelper.ExcuteNonQuery<Order>(sql, paras, false);
        }

        public int UpdateTopupOrder(string prepayid,DateTime? payDatetime)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@PayDatetime", payDatetime, System.Data.DbType.DateTime);
            paras.Add("@WallePrCode", prepayid, System.Data.DbType.String);
            string sql = @"update [dbo].[TopupOrder] set PayDatetime=@PayDatetime
            where  WallePrCode=@WallePrCode";
           return  DapperSqlHelper.ExcuteNonQuery<Order>(sql, paras, false);
        }
    }
}
