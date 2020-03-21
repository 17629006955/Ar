using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    public class FinancialStatementService : IFinancialStatementsService
    {
        public bool Insert(financialStatements model)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@Code", model.Code, System.Data.DbType.String);
            paras.Add("@CreateTime", model.CreateTime, System.Data.DbType.String);
            paras.Add("@UserPhone", model.UserPhone, System.Data.DbType.String);
            paras.Add("@UserCreateTime", model.UserCreateTime, System.Data.DbType.String);
            paras.Add("@StoreName", model.StoreName, System.Data.DbType.String);
            paras.Add("@ProductionType", model.ProductionType, System.Data.DbType.String);
            paras.Add("@Cstname", model.Cstname, System.Data.DbType.String);
            paras.Add("@ProductionCode", model.ProductionCode, System.Data.DbType.String);
            paras.Add("@ProductionName", model.ProductionName, System.Data.DbType.String);
            paras.Add("@Iquantity", model.Iquantity, System.Data.DbType.String);
            paras.Add("@Itaxunitprice", model.Itaxunitprice, System.Data.DbType.String);
            paras.Add("@Isum", model.Isum, System.Data.DbType.String);
            paras.Add("@CpersonName", model.CpersonName, System.Data.DbType.String);
            paras.Add("@PayType", model.PayType, System.Data.DbType.String);
            paras.Add("@AmountOfIncome", model.AmountOfIncome, System.Data.DbType.String);
            paras.Add("@DonationAmount", model.DonationAmount, System.Data.DbType.String);
            paras.Add("@CouponUseCode", model.CouponUseCode, System.Data.DbType.String);
            paras.Add("@CouponUseMoney", model.CouponUseMoney, System.Data.DbType.String);
            paras.Add("@GetCouponTime", model.GetCouponTime, System.Data.DbType.String);
            paras.Add("@UseWalletMoney", model.UseWalletMoney, System.Data.DbType.String);
            paras.Add("@Ratio", model.Ratio, System.Data.DbType.String);
            paras.Add("@RecordsOfConsumptionCreateTime", model.RecordsOfConsumptionCreateTime, System.Data.DbType.String);
            paras.Add("@WriteOffUser", model.WriteOffUser, System.Data.DbType.String);
            paras.Add("@ProductionCode1", model.ProductionCode1, System.Data.DbType.String);
            paras.Add("@ProductionName1", model.ProductionName1, System.Data.DbType.String);
            paras.Add("@ExperiencePrice", model.ExperiencePrice, System.Data.DbType.String);
            paras.Add("@Iquantity1", model.Iquantity1, System.Data.DbType.String);
            paras.Add("@RecordsMoney", model.RecordsMoney, System.Data.DbType.String);
            paras.Add("@CouponUseMoney1", model.CouponUseMoney1, System.Data.DbType.String);
            paras.Add("@ActualConsumption", model.ActualConsumption, System.Data.DbType.String);
            paras.Add("@UseWalletMoney1", model.UseWalletMoney1, System.Data.DbType.String);
            paras.Add("@UseWalletAccountPrincipal", model.UseWalletAccountPrincipal, System.Data.DbType.String);
            paras.Add("@FinancialRevenueAccounting", model.FinancialRevenueAccounting, System.Data.DbType.String);
            paras.Add("@Imoney", model.Imoney, System.Data.DbType.String);
            paras.Add("@ProductInfoRate", model.ProductInfoRate, System.Data.DbType.String);
            paras.Add("@Itax", model.Itax, System.Data.DbType.String);
            paras.Add("@GrossProfit", model.GrossProfit, System.Data.DbType.String);
            string sql = (@"INSERT INTO dbo.financialStatements
           (Code,CreateTime ,UserPhone,UserCreateTime,StoreName ,ProductionType,Cstname ,ProductionCode,ProductionName
           ,Iquantity,Itaxunitprice,Isum,CpersonName,PayType,AmountOfIncome ,DonationAmount
           ,CouponUseCode,CouponUseMoney,GetCouponTime,UseWalletMoney,Ratio,RecordsOfConsumptionCreateTime
           ,WriteOffUser,ProductionCode1,ProductionName1,ExperiencePrice,Iquantity1
           ,RecordsMoney ,CouponUseMoney1 ,ActualConsumption ,UseWalletMoney1,UseWalletAccountPrincipal
           ,FinancialRevenueAccounting,Imoney ,ProductInfoRate ,Itax ,GrossProfit)
                values(@Code,@CreateTime ,@UserPhone,@UserCreateTime,@StoreName ,@ProductionType,@Cstname ,@ProductionCode,@ProductionName
           ,@Iquantity,@Itaxunitprice,@Isum,@CpersonName,@PayType,@AmountOfIncome ,@DonationAmount
           ,@CouponUseCode,@CouponUseMoney,@GetCouponTime,@UseWalletMoney,@Ratio,@RecordsOfConsumptionCreateTime
           ,@WriteOffUser,@ProductionCode1,@ProductionName1,@ExperiencePrice,@Iquantity1
           ,@RecordsMoney ,@CouponUseMoney1 ,@ActualConsumption ,@UseWalletMoney1,@UseWalletAccountPrincipal
           ,@FinancialRevenueAccounting,@Imoney ,@ProductInfoRate ,@Itax ,@GrossProfit)");
            DapperSqlHelper.ExcuteNonQuery<Coupon>(sql, paras, false);
            return true;
        }
    }
}
