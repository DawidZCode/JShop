using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsModule.Contracts.Commands
{
    public class IncreaseProductsInStockCommand : IRequest<bool>
    {
        public IncreaseProductsInStockCommand(int producId, int orderProductCount)//orderProductCount << o tyle zmniejszamy wartość instock 
        {
            this.ProductId = producId;
            this.OrderProductCount = orderProductCount;
        }

        public int ProductId { get; set; }
        public int OrderProductCount { get; set; }
    }
}
