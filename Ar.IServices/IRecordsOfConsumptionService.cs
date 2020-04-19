using AR.Model;
using System;
using System.Collections.Generic;

namespace Ar.IServices
{

    /// <summary>
    /// 消费记录
    /// </summary>
    public interface IRecordsOfConsumptionService
    {
        bool IsWriteOffUser(string phone);
        RecordsOfConsumption GetRecordsOfConsumptionByCode(string code);
        RecordsOfConsumption GetRecordsOfConsumptionByOrderCode(string OrderCode);
        IList<RecordsOfConsumption> GetRecordsOfConsumptionList();

        IList<RecordsOfConsumption> GetRecordsOfConsumptionListByUserCode(string userCode);
        bool InsertRecore(string typeCode, string userCode, decimal? recordsMoney, string explain, decimal? recordsdonationAmount, decimal? recordsaccountPrincipal,string OrderCode, bool IsRecharging = true );

        string PayOrder(string productCode, string userCode, string peopleCount, DateTime dateTime, decimal money, string storeId,string orderCode = "" ,string couponCode = "");
        Order WxPayOrder(string productCode, string userCode, string peopleCount, DateTime dateTime, decimal money, string wxPrepayId, string storeId, string orderCode = "", string couponCode = "");
        Order WxPayNoMoneyOrder(string productCode, string userCode, string peopleCount, DateTime dateTime, decimal money, string orderCode = "", string couponCode = "");
        /// <summary>
        /// 核销
        /// </summary>
        /// <param name="phone">后台管理人员手机号</param>
        /// <param name="orderCode">订单号</param>
        /// <returns></returns>
        bool WriteOff(string phone, string orderCode);

        
    }
}
