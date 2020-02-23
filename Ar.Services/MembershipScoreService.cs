using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    public class MembershipScoreService : IMembershipScoreService
    {
        public MembershipScore GetMembershipScoreByCode(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@code", code, System.Data.DbType.String);
            MembershipScore record = DapperSqlHelper.FindOne<MembershipScore>("select *  from [dbo].[MembershipScore]  where MembershipScoreCode=@code", paras, false);
            return record;
        }

        public IList<MembershipScore> GetMembershipScoreListByUserCode(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            IList<MembershipScore> list = DapperSqlHelper.FindToList<MembershipScore>(@"select * from [dbo].[MembershipScore] where UserCode=@userCode", paras, false);
            return list;
        }

        public void Update(string userCode, int score)
        {
           var list= GetMembershipScoreListByUserCode(userCode);
            if(list==null || list.Count == 0)
            {
                MembershipScore membershipScore = new MembershipScore() { UserCode=userCode,Score= score };
                Insert(membershipScore);
            }else
            {
                MembershipScore membershipScore = list[0];
                membershipScore.Score = membershipScore.Score + score;
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@code", membershipScore.MembershipScoreCode, System.Data.DbType.String);
                paras.Add("@Score", membershipScore.Score , System.Data.DbType.Int32);
                string sql = "update [dbo].[MembershipScore] set Score=@Score where  MembershipScoreCode=@code";
                DapperSqlHelper.ExcuteNonQuery<MembershipScore>(sql, paras, false);
            }
        }
        public void Insert(MembershipScore membershipScore)
        {
            DynamicParameters paras = new DynamicParameters();
            if (string.IsNullOrEmpty(membershipScore.MembershipScoreCode))
            {
                membershipScore.MembershipScoreCode = GetMaxCode();
            }
            paras.Add("@MembershipScoreCode", membershipScore.MembershipScoreCode, System.Data.DbType.String);
            paras.Add("@UserCode", membershipScore.UserCode, System.Data.DbType.String);
            paras.Add("@Score", membershipScore.Score, System.Data.DbType.Int16);
            string sql = (@"insert into [dbo].[MembershipScore] (MembershipScoreCode,UserCode,Score)
                values(@MembershipScoreCode,@UserCode,@Score");
            DapperSqlHelper.ExcuteNonQuery<Coupon>(sql, paras, false);
        }

        public string GetMaxCode()
        {
            var sc = DapperSqlHelper.FindOne<MembershipScore>("SELECT MAX(MembershipScoreCode) MembershipScoreCode FROM [dbo].[MembershipScore]", null, false);
            var code = sc != null ? Convert.ToInt32(sc.MembershipScoreCode) + 1 : 1;
            return code.ToString();
        }
    }
}
