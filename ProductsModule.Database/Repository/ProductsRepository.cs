using Microsoft.Extensions.Configuration;
using ProductsModule.Database.Models;
using ProductsModule.Database.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsModule.Database.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private JProductContext context;

        public ProductsRepository(IConfiguration configuration)
        {
            context = new JProductContext(configuration);
        }


        public IQueryable<Products> GetAviableProducts()
        {
           return context.Products.Where(x => x.ProductAviable == true);
        }


        public async Task<Products> AddProductStock(int id, int addToStock)
        {
            var product = await  context.Products.FindAsync(id);
            if (product != null)
            {
                product.ProductsInStock += addToStock; 
            }
            context.SaveChanges();
            return product;
        }

        public async Task<Products> RemoveProductStock(int id, int removeInStock)
        {
            var product = await context.Products.FindAsync(id);
            if (product != null)
            {
                product.ProductsInStock -= removeInStock;
            }
            context.SaveChanges();
            return product;

        }

        public bool RemoveProductsStock(List<ProductToReduceViewModel> productsToReduse)
        {
            var products =  context.Products.Where(x => productsToReduse.Select(y => y.id).Contains(x.Id)).ToList();

            if(products.Count != productsToReduse.Count)
            {
                return false;
            }
            else
            {
                foreach (var p in products)
                {
                    var reduseStock = productsToReduse.First(x => x.id == p.Id).removeInStock;
                    if (p.ProductsInStock >= reduseStock)
                    {
                       p.ProductsInStock -= reduseStock;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            context.SaveChanges();
            return true;
        }
    }
}
