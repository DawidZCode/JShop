using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Domain.AggregateModel
{
    public class OrderProduct
    {
        public int Id { get; set; }

        public int ProductId { get; protected set; }

        public int OrderCount { get; protected set; }

        public double UnitPrice { get; protected set; }

        public string ProductName { get; protected set; }

        public string ProductCode { get; protected set; }


        public OrderProduct(int productId, int orderCount, double unitPrice, string productName, string productCode)
        {
            ValidateOrderCount(orderCount);
            ValidateUnitPrice(unitPrice);
            ValidateProductName(productName);
            ValidateProductCodeCode(productCode);

            ProductId = productId;
            OrderCount = orderCount;
            UnitPrice = unitPrice;
            ProductName = productName;
            ProductCode = productCode;
        }

        #region Validators
        private void ValidateOrderCount(int orderCount)
        {
            if (orderCount <= 0)
                throw new Exception("orderCount is expired");
        }

        private void ValidateUnitPrice(double unitPrice)
        {
            if (unitPrice <= 0)
                throw new Exception("unitPrice is expired");
        }

        private void ValidateProductName(string productName)
        {
            if (productName.Length < 0)
                throw new Exception("productName is expired");
        }


        private void ValidateProductCodeCode(string productCodeCode)
        {
            if (productCodeCode.Length < 0)
                throw new Exception("productCodeCode is expired");
        }


        #endregion

        public Double Price { get { return this.UnitPrice * OrderCount; } }


        public void ChangeUnitPrice(double unitPrice)
        {
            ValidateUnitPrice(unitPrice);
            this.UnitPrice = unitPrice;
        }

        public void ChangeOrderCount(int orderCount)
        {
            ValidateOrderCount(orderCount);
            this.OrderCount = orderCount;
        }

        public void ChangeProductName(string productName)
        {
            ValidateProductName(productName);
            this.ProductName = productName;
        }

        public void ChangeProductCode(string productCode)
        {
            ValidateProductCodeCode(productCode);
            this.ProductCode = productCode;
        }
    }
}
