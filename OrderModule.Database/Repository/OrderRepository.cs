using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderModule.Domain.AggregateModel;

namespace OrderModule.Database.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private OrderContext context;

        public OrderRepository(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
            optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings").GetSection("OrdersDatabase").Value);

            context = new OrderContext(optionsBuilder.Options);

        }

        public Orders AddNewOrder(Orders newOrder)
        {
            context.Orders.Add(newOrder);
            context.SaveChanges();
            return newOrder;

        }

        public Orders GetOrder(int orderId)
        {
            return context.Orders.FirstOrDefault(x => x.Id == orderId);
        }

        public IQueryable<Orders> GetOrders(int userId)
        {
            return context.Orders.Where(x => x.UserId == userId);
        }


        public Orders ChangeOrderStatus(Orders order, OrderStatus newOrderStatus)
        {
            var result = GetOrder(order.Id);
            if (result != null)
            {
                if (result.ChangeStatus(newOrderStatus))
                {
                    context.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}
