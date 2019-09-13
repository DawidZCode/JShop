using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Contracts.ViewModel.Read
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string OrderStatus { get; set; }
    }
}
