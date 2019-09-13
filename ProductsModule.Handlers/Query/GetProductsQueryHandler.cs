using MediatR;
using ProductsModule.Contracts.Query;
using ProductsModule.Contracts.ViewModel.Read;
using ProductsModule.Database.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsModule.Handlers.Query
{
    public class GetProductsQueryHandler : IRequestHandler<ProductContractQuery, IEnumerable<ProductViewModel>>
    {
        private IProductsRepository productsRepo;

        public GetProductsQueryHandler(IProductsRepository _productsRepo)
        {
            productsRepo = _productsRepo;
        }

        public Task<IEnumerable<ProductViewModel>> Handle(ProductContractQuery request, CancellationToken cancellationToken)
        {
            var products = productsRepo.GetAviableProducts().ToList();
            List<ProductViewModel> listResult = new List<ProductViewModel>();
            foreach (var p in products)
            {
                listResult.Add(new ProductViewModel()
                {
                    Price = p.Price,
                    ProductId = p.Id,
                    ProductName = p.ProductName,
                    ProductsInStock = p.ProductsInStock
                });
            }
            return Task.FromResult<IEnumerable<ProductViewModel>>(listResult);
        }
    }
}
