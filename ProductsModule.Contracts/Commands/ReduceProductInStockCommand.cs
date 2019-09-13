using MediatR;
using ProductsModule.Contracts.ViewModel.Write;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsModule.Contracts.Commands
{
    public class ReduceProductInStockCommand : IRequest<bool>
    {
        public ReduceProductInStockCommand(int orderId, IEnumerable<ProductsInStockViewModel> products) 
        {
            this.OrderId = orderId;
            Products = products;
        }

        public int OrderId { get; protected set; }
        public IEnumerable<ProductsInStockViewModel> Products { get; protected set; }
    }
}
