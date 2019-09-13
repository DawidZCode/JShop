using System;
using System.Collections.Generic;

namespace ProductsModule.Database.Models
{
    public partial class Products
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int ProductsInStock { get; set; }
        public bool ProductAviable { get; set; }
    }
}
