﻿using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    public interface IOrderService
    {
        Order GetOrderInfo(string orderCode);
        Order GetOrderByCode(string code);
        IList<Order> GetOrderList(string userCode);
        IList<Order> GetPayOrderList(string userCode);
        IList<Order> GetNOPayOrderList(string userCode);
        int UpdateOrderbyWxorder(Order order);
        int UpdateOrder(Order order);
        string InsertOrder(Order order);
        int DeletOrderInfo(string orderCode);
    }
}
