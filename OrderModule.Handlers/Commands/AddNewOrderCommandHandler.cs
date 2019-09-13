using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrderModule.Contracts.Command;
using OrderModule.Database.Repository;
using OrderModule.Domain.AggregateModel;
using ProductsModule.Contracts.Commands;
using ProductsModule.Contracts.ViewModel.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderModule.Handlers.Commands
{
    public class AddNewOrderCommandHandler : IRequestHandler<AddOrderCommand, int>
    {
        IConfiguration config;
        private IOrderRepository orderRepository;

        public AddNewOrderCommandHandler(IConfiguration iConfig, IOrderRepository _orderRepository)
        {
            config = iConfig;
            orderRepository = _orderRepository;
        }


        public Task<int> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            Orders newOrder = new Orders(request.UserId, DateTime.Now, OrderStatus.Draft, request.Payment.CreditCartNumber, request.Payment.Cvv, request.Payment.ExpiredDate, request.Payment.OwnerName, request.ContactMail);
            foreach (var product in request.Products)
            {
                newOrder.AddProduct(product.Id, product.ProductCount, product.Price, product.ProductName, product.ProductCode);
            }

            orderRepository.AddNewOrder(newOrder);

            List<ProductsInStockViewModel> productsToReserve = new List<ProductsInStockViewModel>();
            newOrder.Products.ToList().ForEach(x => productsToReserve.Add(new ProductsInStockViewModel()
            {
                ProductId = x.ProductId,
                OrderProductCancelCount = x.OrderCount
            }));

            ReserveProductsMessage(new ReduceProductInStockCommand(newOrder.Id, productsToReserve));

            return Task.FromResult(newOrder.Id);
        }


        private bool ReserveProductsMessage(ReduceProductInStockCommand productsToReserve)
        {
            try
            {
                var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(productsToReserve)));

                TopicClient topicClient = new TopicClient(config.GetSection("QueueConfig").GetSection("ServiceBusConnectionString").Value, config.GetSection("QueueConfig").GetSection("ProductsQueueName").Value);
                topicClient.SendAsync(message).Wait();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
