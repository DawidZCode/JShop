using MediatR;
using OrderModule.Contracts.ViewModel.Read;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Contracts.Query
{
    public class GetOrdersCotractQuery : IRequest<IEnumerable<OrderViewModel>>
    {
        public GetOrdersCotractQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; protected set; }
    }
}
