using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProductsModule.Contracts.ViewModel.Read;

namespace JShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [Route("GetProducts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProductsList()
        {
            var client = new HttpClient() { BaseAddress = new Uri(_configuration.GetSection("ModuleURL").GetSection("ProductModule").Value) };
            System.Net.Http.HttpResponseMessage response = client.GetAsync("/api/Products/GetProducts").Result;
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