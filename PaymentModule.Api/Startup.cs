using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Mailer;
using Core.Infrastructure.ServiceBus;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaymentModule.Api.Service;
using PaymentModule.Comtracts.Commands;
using PaymentModule.Handlers.Commands;
using Swashbuckle.AspNetCore.Swagger;

namespace PaymentModule.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Jshop Payment Api",
                    Description = "Przykładowy opis",
                    TermsOfService = "None"//,

                });
            });

            services.AddTransient<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => t => sp.GetService(t));
            services.AddTransient<IRequestHandler<ExecuteBankTransactionCommand, bool>, ExecuteBankTransactionCommandHandler>();

            services.AddSingleton<IServiceBusSubscription, ServiceBusSubscription>();

            services.AddSingleton<IShopMailer, ShopMailer>();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


               app.UseHttpsRedirection();
            app.UseMvc();


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JShop API V1");
            });

            var busSubscription = app.ApplicationServices.GetService<IServiceBusSubscription>();
            busSubscription.RegisterOnMessageHandlerAndReceiveMessages();
        }
    }
}
