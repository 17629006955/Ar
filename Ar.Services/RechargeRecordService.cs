﻿using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    public class RechargeRecordService : IRechargeRecordService
    {
        public RechargeRecord GetRechargeRecordByCode(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@rechargeRecord", code, System.Data.DbType.String);
            RechargeRecord rechargeRecord = DapperSqlHelper.FindOne<RechargeRecord>("select * from [dbo].[RechargeRecord] where RechargeRecordCode=@rechargeRecord", paras, false);
            return rechargeRecord;
        }

        public IList<RechargeRecord> GetRechargeRecordList()
        {
            DynamicParameters paras = new DynamicParameters();
            IList<RechargeRecord> list = DapperSqlHelper.FindToList<RechargeRecord>(@"select * from [dbo].[RechargeRecord]", null, false);
            return list;
        }
        public IList<RechargeRecord> GetRechargeRecordListByUserCode(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            IList<RechargeRecord> list = DapperSqlHelper.FindToList<RechargeRecord>(@"select * from [dbo].[RechargeRecord] where UserCode=@userCode", paras, false);
            return list;
        }

        
        public bool Recharge(string typeCode,string userCode)
        {
            IRechargeTypeService s = new RechargeTypeService();
            IRecordsOfConsumptionService cs = new RecordsOfConsumptionService();
            IUseWalletService us = new UseWalletService();
            var type=s.GetRechargeTypeByCode(typeCode);
           var donationAmount=type.DonationAmount;
            var money = type.Money;
            var ratio = donationAmount / money;
            RechargeRecord record = new RechargeRecord()
            {
                RechargeRecordCode = "1",
                UserCode = userCode,
                RechargeAmount = money,
                CreateTime = DateTime.Now
            };
            UseWallet wallet = new UseWallet()
            {
                WalletCode = "1",
                UserCode = userCode,
                AccountPrincipal = money,
                DonationAmount = donationAmount,
                Ratio = decimal.Round(decimal.Parse(ratio.ToString()), 2).ToString(),
                Status = true,
                Sort = 1
            };
            //钱包
            us.InsertUseWallet(wallet);
            //消费记录
            cs.InsertRecore(typeCode, userCode, decimal.Parse(money.ToString()));
            //充值
            InsertRechargeRecord(record);
            return true;
        }
        public bool InsertRechargeRecord(RechargeRecord record)
        {
            if (string.IsNullOrEmpty(record.RechargeRecordCode))
            {
                record.RechargeRecordCode = GetMaxCode();
            }
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@RechargeRecordCode", record.RechargeRecordCode, System.Data.DbType.String);
            paras.Add("@UserCode", record.UserCode, System.Data.DbType.String);
            paras.Add("@RechargeAmount", record.RechargeAmount, System.Data.DbType.String);
            paras.Add("@Explain", record.Explain, System.Data.DbType.String);
            paras.Add("@CreateTime", record.CreateTime, System.Data.DbType.String);
            User userone = DapperSqlHelper.FindOne<User>(@"INSERT INTO [dbo].[RechargeRecord]
           ([RechargeRecordCode],[UserCode] ,[RechargeAmount]  ,[Explain]) 
            VALUES  ( @RechargeRecordCode, 
                     @UserCode, 
                     @RechargeAmount, 
                     @Explain, 
                     getdate())", paras, false);
            return true;
        }

        public string GetMaxCode()
        {
            var record = DapperSqlHelper.FindOne<RechargeRecord>("SELECT MAX(RechargeRecordCode) RechargeRecordCode FROM [dbo].[RechargeRecord]", null, false);
            var code = record != null ? Convert.ToInt32(record.RechargeRecordCode) + 1 : 1;
            return code.ToString();
        }
    }
}
