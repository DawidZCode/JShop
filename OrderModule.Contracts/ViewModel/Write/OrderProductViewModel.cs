using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Contracts.ViewModel
{
    public class OrderProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int ProductCount { get; set; }
        public  string  ProductCode { get; set; }
    }
}
