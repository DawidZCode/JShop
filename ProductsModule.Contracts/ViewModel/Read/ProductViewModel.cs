using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsModule.Contracts.ViewModel.Read
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int ProductsInStock { get; set; }
    }
}
