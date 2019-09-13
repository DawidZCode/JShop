using MediatR;
using OrderModule.Contracts.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Contracts.Command
{
    public class AddOrderCommand: IRequest<int>
    {
        public AddOrderCommand(int userId,
             IEnumerable<OrderProductViewModel> products,
             PaymentMethodViewModel payment,
             string contactMail
            )
        {
            UserId = userId;
            Products = products;
            Payment = payment;
            ContactMail = contactMail;
        }

        public int UserId { get; protected set; }

        public IEnumerable<OrderProductViewModel> Products { get; protected set; }

        public PaymentMethodViewModel Payment { get; protected set; }

        public string ContactMail { get; protected set; }
    }
}

