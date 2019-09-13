using OrderModule.Domain.AggregateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderModule.Database.Repository
{
    public interface IOrderRepository
    {
        IQueryable<Orders> GetOrders(int userId);

        Orders GetOrder(int orderId);

        Orders AddNewOrder(Orders order);

        Orders ChangeOrderStatus(Orders order, OrderStatus newOrderStatus);

    }
}
