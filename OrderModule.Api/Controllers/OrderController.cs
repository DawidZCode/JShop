using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderModule.Contracts.Command;
using OrderModule.Contracts.Query;
using OrderModule.Contracts.ViewModel.Read;
using OrderModule.Contracts.ViewModel.Write;

namespace OrderModule.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IMediator memoryBus;


        public OrderController(IMediator _memoryBus)
        {
            memoryBus = _memoryBus;
        }

        [Route("CreateOrder/{userId}")]
        [HttpPost]
        public async Task<ActionResult<string>> CreateOrder(int userId, NewOrderViewModel order)
        {
            AddOrderCommand newOrder = new AddOrderCommand(userId, order.Products, order.Payment, order.contactMail);

            var result = await memoryBus.Send(newOrder);
            return Ok(result);
        }


        [Route("GetOrdersStatus/{userId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrdersStatus(int userId)
        {
            var result = await memoryBus.Send(new GetOrdersCotractQuery(userId));
            return Ok(result);
        }
    }
}