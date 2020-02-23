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
        RecordsOfConsumption GetRecordsOfConsumptionByCode(string code);
        IList<RecordsOfConsumption> GetRecordsOfConsumptionList();

        IList<RecordsOfConsumption> GetRecordsOfConsumptionListByUserCode(string userCode);
        bool InsertRecore(string typeCode, string userCode, decimal? recordsMoney, string explain);

        bool PayOrder(string productCode, string userCode, string peopleCount, DateTime dateTime);

        /// <summary>
        /// 核销
        /// </summary>
        /// <param name="phone">后台管理人员手机号</param>
        /// <param name="orderCode">订单号</param>
        /// <returns></returns>
        bool WriteOff(string phone, string orderCode);
    }
}
