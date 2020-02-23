using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    /// <summary>
    /// 充值记录
    /// </summary>
    public interface IRechargeRecordService
    {
        /// <summary>
        /// 充值详情
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        RechargeRecord GetRechargeRecordByCode(string code);

        /// <summary>
        /// 获取充值记录
        /// </summary>
        /// <returns></returns>
        IList<RechargeRecord> GetRechargeRecordList();


        /// <summary>
        /// 获取充值记录根据用户
        /// </summary>
        /// <returns></returns>
        IList<RechargeRecord> GetRechargeRecordListByUserCode(string userCode);

        /// <summary>
        ///充值
        /// </summary>
        /// <param name="typeCode">类型</param>
        /// <param name="userCode">用户</param>
        /// <returns></returns>
        bool Recharge(string typeCode, string userCode);
        bool InsertRechargeRecord(RechargeRecord record);
    }
}
