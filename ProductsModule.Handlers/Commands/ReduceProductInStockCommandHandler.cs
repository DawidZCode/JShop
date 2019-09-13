using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrderModule.Contracts.Command;
using ProductsModule.Contracts.Commands;
using ProductsModule.Database.Repository;
using ProductsModule.Database.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsModule.Handlers.Commands
{
    public class ReduceProductInStockCommandHandler : IRequestHandler<ReduceProductInStockCommand, bool>
    {
        private IProductsRepository productsRepo;
        IConfiguration config;

        public ReduceProductInStockCommandHandler(IConfiguration iConfig, IProductsRepository _productsRepo)
        {
            config = iConfig;
            productsRepo = _productsRepo;
        }
        public Task<bool>  Handle(ReduceProductInStockCommand request, CancellationToken cancellationToken)
        {
            List<ProductToReduceViewModel> productsToReduse = new List<ProductToReduceViewModel>();
            request.Products.ToList().ForEach(x => productsToReduse.Add(new ProductToReduceViewModel()
            {
                id = x.ProductId,
                removeInStock = x.OrderProductCancelCount
            }));

            var result = productsRepo.RemoveProductsStock(productsToReduse);

            if (result)
                return Task.FromResult(ChangeOrderStatus(new OrderStatusChangeCommand(request.OrderId, OrderModule.Domain.AggregateModel.OrderStatus.Confirm)));
            else
                return Task.FromResult(ChangeOrderStatus(new OrderStatusChangeCommand(request.OrderId, OrderModule.Domain.AggregateModel.OrderStatus.Cancelled)));
        }

        private bool ChangeOrderStatus(OrderStatusChangeCommand command)
        {
            try
            {
                var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(command)));

                TopicClient topicClient = new TopicClient(config.GetSection("QueueConfig").GetSection("ServiceBusConnectionString").Value, config.GetSection("QueueConfig").GetSection("OrderQueueName").Value);
                topicClient.SendAsync(message).Wait();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
