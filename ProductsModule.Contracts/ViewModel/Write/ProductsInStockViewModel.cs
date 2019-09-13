using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsModule.Contracts.ViewModel.Write
{
    public class ProductsInStockViewModel
    {
        public int ProductId { get; set; }
        public int OrderProductCancelCount { get; set; }
    }
}
