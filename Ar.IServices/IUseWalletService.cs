using AR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.IServices
{
    /// <summary>
    /// 钱包
    /// </summary>
    public interface IUseWalletService
    {
        /// <summary>
        /// 获取钱包列表
        /// </summary>
        /// <returns></returns>
        IList<UseWallet> GetUseWalletList();

        /// <summary>
        /// 根据用户获取钱包
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        IList<UseWallet> GetUseWallet(string userCode);

        /// <summary>
        /// 插入钱包信息
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
        bool InsertUseWallet(UseWallet wallet);

        /// <summary>
        /// 获取充值页面数据
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        RechargePage GetRechargePage(string userCode);
       bool UpdateData(string userCode, decimal money, string OrderCode, out decimal? recordsaccountPrincipalTemp);

        bool ExistMoney(string userCode, decimal money);
        object GetUseWalletInfoByUserCode(string userCode);

        UseWallet GetUseWalletCountMoney(string userCode);
        UseWallet GetUseWalletCountMoneyWf(string userCode);
        decimal? GetUseAccountPrincipalByUserCode(string userCode);
    }
}
