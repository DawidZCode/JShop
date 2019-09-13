using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentModule.Comtracts.Commands
{
    public class ExecuteBankTransactionCommand : IRequest<bool>
    {
        public ExecuteBankTransactionCommand(string creditCartNumber, string cvv, DateTime expiredDate, string ownerName, string mailAddress)
        {
            this.CreditCartNumber = creditCartNumber;
            this.Cvv = cvv;
            this.ExpiredDate = expiredDate;
            this.OwnerName = ownerName;
            this.MailAddress = mailAddress;
        }

        public string CreditCartNumber { get; set; }
        public string Cvv { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string OwnerName { get; set; }
        public string MailAddress { get; set; }
    }
}
