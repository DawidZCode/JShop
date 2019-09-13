using ProductsModule.Database.Models;
using ProductsModule.Database.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsModule.Database.Repository
{
    public interface IProductsRepository
    {
        IQueryable<Products> GetAviableProducts();

        Task<Products> AddProductStock(int id, int addToStock);

        Task<Products> RemoveProductStock(int id, int removeInStock);

        bool RemoveProductsStock(List<ProductToReduceViewModel> productsToReduse);
    }
}
