using MediatR;
using OrderModule.Domain.CommandContract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderModule.CommandHandler
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderContractCommand, int>
    {
        public Task<int> Handle(AddOrderContractCommand request, CancellationToken cancellationToken)
        {

           throw new NotImplementedException();
        }

     
      
    }
}
