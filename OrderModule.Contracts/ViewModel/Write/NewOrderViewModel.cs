using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Contracts.ViewModel.Write
{
    public class NewOrderViewModel
    {
        public IEnumerable<OrderProductViewModel> Products { get; set; }

        public PaymentMethodViewModel Payment { get; set; }

        public string contactMail { get; set; }
    }
}
