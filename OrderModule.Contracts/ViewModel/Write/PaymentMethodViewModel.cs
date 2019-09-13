using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Contracts.ViewModel
{
    public class PaymentMethodViewModel
    {
        public string CreditCartNumber { get; set; }

        public string Cvv { get; set; }

        public DateTime ExpiredDate { get; set; }

        public string OwnerName { get; set; }
    }
}
