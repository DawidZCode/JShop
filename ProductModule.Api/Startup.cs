using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.ServiceBus;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductsModule.Contracts.Commands;
using ProductsModule.Contracts.Query;
using ProductsModule.Contracts.ViewModel.Read;
using ProductsModule.Database.Models;
using ProductsModule.Database.Repository;
using ProductsModule.Handlers.Commands;
using ProductsModule.Handlers.Query;
using Swashbuckle.AspNetCore.Swagger;

namespace ProductModule.Api
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
                    Title = "Jshop Products Api",
                    Description = "Przykładowy opis",
                    TermsOfService = "None"//,

                });
            });

            services.AddDbContext<JProductContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:ProductsDatabase"]);

            });

            services.AddTransient<IProductsRepository, ProductsRepository>();

            services.AddTransient<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => t => sp.GetService(t));
            services.AddTransient<IRequestHandler<ProductContractQuery, IEnumerable<ProductViewModel>>, GetProductsQueryHandler>();
            services.AddTransient<IRequestHandler<ReduceProductInStockCommand, bool>, ReduceProductInStockCommandHandler>();

            services.AddSingleton<IServiceBusSubscription, ServiceBusSubscription>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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
