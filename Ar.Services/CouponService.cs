﻿using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    public class CouponService : ICouponService
    {
        public object GetCoupon(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            IList<Coupon> isUserList = DapperSqlHelper.FindToList<Coupon>("select *  from [dbo].[Coupon]  where IsUsed=1 and UserCode=@userCode", paras, false);
            IList<Coupon> lapseList = DapperSqlHelper.FindToList<Coupon>("select *  from [dbo].[Coupon] where  UserCode=@userCode and  (IsUsed=1   or (isnull(VersionEndTime,'9999-9-9')<getdate()))", paras, false);
            IList<Coupon> takeEffectList = DapperSqlHelper.FindToList<Coupon>("select *  from [dbo].[Coupon] where UserCode=@userCode and   isnull(IsUsed,0)=0 and  isnull(VersionEndTime,'9999-9-9')>=getdate()", paras, false);
            return new 
            {
                isUserList= isUserList,
                lapseList= lapseList,
                takeEffectList= takeEffectList
            };
        }

        public Coupon GetCouponByCode(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@CouponUseCode", code, System.Data.DbType.String);
            Coupon record = DapperSqlHelper.FindOne<Coupon>("select a.*,b.CouponTypeName from [dbo].[Coupon] a,[dbo].[CouponType] b  where a.CouponUseCode=@CouponUseCode  and a.CouponTypeCode=b.CouponTypeCode", paras, false);
            return record;
        }

        public IList<Coupon> GetCouponList(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@UserCode", userCode, System.Data.DbType.String);
            IList<Coupon> list = DapperSqlHelper.FindToList<Coupon>(@"select a.*,b.CouponTypeName from [dbo].[Coupon] a,[dbo].[CouponType] b  where a.UserCode=@userCode and a.CouponTypeCode=b.CouponTypeCode", paras, false);
            return list;
        }
        /// <summary>
        /// 使用
        /// </summary>
        /// <param name="couponCode"></param>
        public bool UsedUpdate(string couponCode,string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@CouponUseCode", couponCode, System.Data.DbType.String);
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            string sql = "update [dbo].[Coupon] set IsUsed=1, UseTime=getdate(),userCode=@userCode where CouponUseCode=@CouponUseCode";
            DapperSqlHelper.ExcuteNonQuery<Coupon>(sql, paras, false);
            return true;
        }
        /// <summary>
        /// 赠送
        /// </summary>
        /// <param name="couponCode"></param>
        public bool GiveedUpdate(string couponCode,string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@CouponUseCode", couponCode, System.Data.DbType.String);
            string sql1 = "select * from [dbo].[Coupon] where CouponUseCode=@code ";
            Coupon coupon= DapperSqlHelper.FindOne<Coupon>(sql1, paras, false);
            string sql = "update [dbo].[Coupon] set IsGiveed=1, GiveedTime=getdate() where CouponUseCode=@CouponUseCode";
            DapperSqlHelper.ExcuteNonQuery<Coupon>(sql, paras, false);
            coupon.CouponCode = GetMaxCode();
            coupon.UserCode = userCode;
            Insert(coupon);
            return true;
        }

        public  bool InsertCouponByUser(string couponCode, string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@CouponUseCode", couponCode, System.Data.DbType.String);
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            string sql1 = "select * from [dbo].[Coupon] where CouponUseCode=@CouponUseCode ";
            Coupon coupon = DapperSqlHelper.FindOne<Coupon>(sql1, paras, false);
            if (coupon == null|| coupon.VersionEndTime<DateTime.Now)
            {
                return false;
            }
            else
            {
                string sql = "update [dbo].[Coupon] set UserCode=@userCode  where CouponUseCode=@CouponUseCode";
                DapperSqlHelper.ExcuteNonQuery<Coupon>(sql, paras, false);
            }
            return true;
        }
        public bool Insert(Coupon coupon)
        {
            DynamicParameters paras = new DynamicParameters();
            if (string.IsNullOrEmpty(coupon.CouponCode))
            {
                coupon.CouponCode = GetMaxCode();
            }
            paras.Add("@CouponCode", coupon.CouponCode, System.Data.DbType.String);
            paras.Add("@UserCode", coupon.UserCode, System.Data.DbType.String);
            paras.Add("@CouponTypeCode", coupon.CouponTypeCode, System.Data.DbType.String);
            paras.Add("@StratTime", coupon.StratTime, System.Data.DbType.String);
            paras.Add("@VersionEndTime", coupon.VersionEndTime, System.Data.DbType.String);
            paras.Add("@CouponUseCode", coupon.CouponUseCode, System.Data.DbType.String);
            string sql=(@"insert into [dbo].[Coupon] (CouponCode,UserCode,CouponTypeCode,CreateTime,
                    StratTime,VersionEndTime,IsUsed,IsGiveed,UseTime,GiveedTime,CouponUseCode)
                values(@CouponCode,@UserCode,@CouponTypeCode,getdate(),@StratTime,@VersionEndTime,
                 0,0,null,null,@CouponUseCode)");
            DapperSqlHelper.ExcuteNonQuery<Coupon>(sql, paras, false);
            return true;
        }
        public string GetMaxCode()
        {
            var coupon = DapperSqlHelper.FindOne<Coupon>("SELECT MAX(CouponCode) CouponCode FROM [dbo].[Coupon]", null, false);
            //var code = coupon != null ? Convert.ToInt32(coupon.CouponCode) + 1 : 1;
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 判断优惠卷是否存在是否被使用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int Exist(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@CouponCode", code, System.Data.DbType.String);
            var isExist= GetCouponByCode(code)!=null;
            if (!isExist)
            {
                return 1;//优惠卷不存在
            }
            else
            {
                Coupon record = DapperSqlHelper.FindOne<Coupon>("select * from [dbo].[Coupon]  where IsUsed=1 and CouponCode=@CouponCode", paras, false);
                if (record != null)
                {
                    return 1;//优惠卷被使用
                }
            }
            return 3;
        }
    }
}
