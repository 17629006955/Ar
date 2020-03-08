using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    public interface IOrderService
    {
        Order GetOrderByCode(string code);
        IList<Order> GetOrderList(string userCode);
        IList<Order> GetPayOrderList(string userCode);
        IList<Order> GetNOPayOrderList(string userCode);

        void UpdateOrder(Order order);
        string InsertOrder(Order order);

    }
}
