using MediatR;
using OrderModule.Domain.AggregateModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Contracts.Command
{
    public class OrderStatusChangeCommand : IRequest<bool>
    {
        public OrderStatusChangeCommand(int orderId, OrderStatus newOrderStatus) 
        {
            this.OrderId = orderId;
            this.NewOrderStatus = newOrderStatus;
        }

        public int OrderId { get; protected set; }
        public OrderStatus NewOrderStatus { get; protected set; }
    }
}
