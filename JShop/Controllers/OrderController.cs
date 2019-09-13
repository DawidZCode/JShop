using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrderModule.Contracts.ViewModel.Read;
using OrderModule.Contracts.ViewModel.Write;
using OrderModule.Database;

namespace JShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IConfiguration _configuration;
        private int userID = 1; //niema autentykacji więc umownie jesteśmy userem o id 1


        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("CreateOrder")]
        [HttpPost]
        public async Task<ActionResult<dynamic>> CreateOrder(NewOrderViewModel order)
        {
            var client = new HttpClient() { BaseAddress = new Uri(_configuration.GetSection("ModuleURL").GetSection("OrderModule").Value) };
            System.Net.Http.HttpResponseMessage response = client.PostAsJsonAsync("/api/Order/CreateOrder/" + userID, order).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<dynamic>().Result;
                return Ok(result);
            }
            else
            {
                return new BadRequestObjectResult("Bad request");
            }
        }

        [Route("GetOrdersStatus")]
        [HttpGet]
        public async Task<ActionResult<dynamic>> GetOrdersStatus()
        {
            var client = new HttpClient() { BaseAddress = new Uri(_configuration.GetSection("ModuleURL").GetSection("OrderModule").Value) };
            System.Net.Http.HttpResponseMessage response = client.GetAsync("/api/Order/GetOrdersStatus/" + userID).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<dynamic>().Result;
                return Ok(result);
            }
            else
            {
                return new BadRequestObjectResult("Bad request");
            }
        }

    }
}