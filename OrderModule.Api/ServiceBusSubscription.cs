using Core.Infrastructure.ServiceBus;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrderModule.Contracts.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderModule.Api
{
    public class ServiceBusSubscription : IServiceBusSubscription
    {
        private readonly SubscriptionClient _subscriptionClient;
        private readonly IConfiguration _configuration;
        IMediator memoryBus;


        public ServiceBusSubscription(IConfiguration configuration, IMediator _memoryBus)
        {
            memoryBus = _memoryBus;
            _configuration = configuration;



            _subscriptionClient = new SubscriptionClient(
                  _configuration.GetSection("QueueConfig").GetSection("ServiceBusConnectionString").Value,
                  _configuration.GetSection("QueueConfig").GetSection("TopicPath").Value,//"order",
                  _configuration.GetSection("QueueConfig").GetSection("SubsciptionName").Value);
        }

        public async Task CloseSubscriptionClientAsync()
        {
            await _subscriptionClient.CloseAsync();
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false,
                MaxAutoRenewDuration = TimeSpan.FromMinutes(2)
            };

            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }


        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var newCommand = JsonConvert.DeserializeObject<OrderStatusChangeCommand>(Encoding.UTF8.GetString(message.Body));

            var result = await memoryBus.Send(newCommand);
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            return Task.CompletedTask;
        }
    }
}
