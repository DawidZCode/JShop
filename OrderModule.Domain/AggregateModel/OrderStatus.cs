using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Domain.AggregateModel
{
    public enum OrderStatus
    {
        Draft = 1,
        Submit = 2,
        Validating = 3,
        Confirm = 4,
        Paid = 5,
        Shipping = 6,
        Shipped = 7,
        Done = 8,
        Cancelled = 9
    }
}
