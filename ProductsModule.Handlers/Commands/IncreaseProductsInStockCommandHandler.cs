using MediatR;
using ProductsModule.Contracts.Commands;
using ProductsModule.Database.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsModule.Handlers.Commands
{
    public class IncreaseProductsInStockCommandHandler : IRequestHandler<IncreaseProductsInStockCommand, bool>
    {
        private IProductsRepository productsRepo;

        public IncreaseProductsInStockCommandHandler(IProductsRepository _productsRepo)
        {
            productsRepo = _productsRepo;
        }

        public Task<bool> Handle(IncreaseProductsInStockCommand request, CancellationToken cancellationToken)
        {
            var result = productsRepo.AddProductStock(request.ProductId, request.OrderProductCount); 
            return Task.FromResult(true);
        }
    }
}
