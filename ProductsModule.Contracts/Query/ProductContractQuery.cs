using MediatR;
using ProductsModule.Contracts.ViewModel.Read;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsModule.Contracts.Query
{
    public class ProductContractQuery : IRequest<IEnumerable<ProductViewModel>>
    {
    }
}
