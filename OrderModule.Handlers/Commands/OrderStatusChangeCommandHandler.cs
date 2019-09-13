using Core.Infrastructure.Mailer;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrderModule.Contracts.Command;
using OrderModule.Database.Repository;
using PaymentModule.Comtracts.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderModule.Handlers.Commands
{
    public class OrderStatusChangeCommandHandler : IRequestHandler<OrderStatusChangeCommand, bool>
    {
        IConfiguration config;
        private IOrderRepository orderRepository;
        private IShopMailer _mailerService;

        public OrderStatusChangeCommandHandler(IConfiguration iConfig, IOrderRepository _orderRepository, IShopMailer mailerService)
        {
            config = iConfig;
            orderRepository = _orderRepository;
            _mailerService = mailerService;
        }



        public Task<bool> Handle(OrderStatusChangeCommand request, CancellationToken cancellationToken)
        {
            var order = orderRepository.GetOrder(request.OrderId);
            var result = orderRepository.ChangeOrderStatus(order, request.NewOrderStatus);

            if (result != null && result.OrderStatus == Domain.AggregateModel.OrderStatus.Cancelled)
            {
                return OrderChangeStatusToCancel(order.PaymentMethod.OwnerName,order.ContactMail,"Order was cancelled", "Order was cancelled");
            }
            else 
            if (result != null && result.OrderStatus == Domain.AggregateModel.OrderStatus.Confirm)
                return Task.FromResult(OrderChangeStatusToConfirm(result.PaymentMethod.CreditCartNumber, result.PaymentMethod.Cvv, result.PaymentMethod.ExpiredCardDate, result.PaymentMethod.OwnerName, result.ContactMail));
            else
                return Task.FromResult(false);
        }

        private async Task<bool> OrderChangeStatusToCancel(string mailToName, string mailTo, string title, string desctiption)
        {
            var resultMail = await _mailerService.SendMailAsync(mailToName,mailTo,title,desctiption);
            return resultMail;
        }

        private bool OrderChangeStatusToConfirm(string creditCartNumber, string cvv, DateTime expiredDate, string ownerName, string mailAddress)
        {
            try
            {
                ExecuteBankTransactionCommand command = new ExecuteBankTransactionCommand(creditCartNumber, cvv, expiredDate, ownerName, mailAddress);
                var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(command)));

                TopicClient topicClient = new TopicClient(config.GetSection("QueueConfig").GetSection("ServiceBusConnectionString").Value, config.GetSection("QueueConfig").GetSection("PaymentQueueName").Value);
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
