using MediatR;
using Microsoft.Extensions.Configuration;
using OrderModule.Contracts.Query;
using OrderModule.Contracts.ViewModel.Read;
using OrderModule.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderModule.Handlers.Query
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrdersCotractQuery, IEnumerable<OrderViewModel>>
    {
        IConfiguration config;
        private IOrderRepository orderRepository;

        public GetOrderQueryHandler(IConfiguration iConfig, IOrderRepository _orderRepository)
        {
            config = iConfig;
            orderRepository = _orderRepository;
        }


        public Task<IEnumerable<OrderViewModel>> Handle(GetOrdersCotractQuery request, CancellationToken cancellationToken)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();

            var orders = orderRepository.GetOrders(request.UserId).ToList();

            foreach (var o in orders)
            {
                result.Add(new OrderViewModel()
                {
                    Id = o.Id,
                    OrderStatus = o.OrderStatus.ToString("F")
                });
            }
            return Task.FromResult(result.AsEnumerable());  
        }
    }
}
