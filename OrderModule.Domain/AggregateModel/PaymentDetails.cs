using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Domain.AggregateModel
{
    public class PaymentDetails
    {
        public int Id { get; set; }

        public string CreditCartNumber { get; protected set; }

        public string Cvv { get; protected set; }

        public DateTime ExpiredCardDate { get; protected set; }

        public string OwnerName { get; protected set; }


        public PaymentDetails(string creditCartNumber, string cvv, DateTime expiredCardDate, string ownerName)
        {
            ValidateExpiredCardDate(expiredCardDate);
            ValidateCreditCardNumber(creditCartNumber);
            ValidateCvvCode(cvv);
            ValidateOwnerName(ownerName);

            CreditCartNumber = creditCartNumber;
            ExpiredCardDate = expiredCardDate;
            Cvv = cvv;
            OwnerName = ownerName;
        }

        private void ValidateExpiredCardDate(DateTime expiredCardDate)
        {
            if (expiredCardDate < DateTime.Now)
            {
                throw new Exception("expiredCardDate is expired");
            }
        }

        private void ValidateCreditCardNumber(string creditCartNumber)
        {
            if (string.IsNullOrWhiteSpace(creditCartNumber))
                throw new Exception("creditCartNumber is empry");
        }

        private void ValidateCvvCode(string cvv)
        {
            if (string.IsNullOrWhiteSpace(cvv))
                throw new Exception("cvv is empry");
        }

        private void ValidateOwnerName(string ownerName)
        {
            if (string.IsNullOrWhiteSpace(ownerName))
                throw new Exception("ownerName is empry");
        }
    }
}
