using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsModule.Contracts.Query;
using ProductsModule.Contracts.ViewModel.Read;

namespace ProductModule.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IMediator memoryBus;



        public ProductsController(IMediator _memoryBus)
        {
            memoryBus = _memoryBus;
        }

        [Route("GetProducts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProductsList()
        {
            var result = await memoryBus.Send(new ProductContractQuery());
            return Ok(result);
        }
    }
}