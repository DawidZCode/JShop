using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderModule.Domain.AggregateModel
{
    public class Orders
    {
        public int Id { get; set; }

        public int UserId { get; protected set; }

        public DateTime OrderDate { get; protected set; }

        public OrderStatus OrderStatus { get; protected set; }

        public string ContactMail { get; protected set; }

        private List<OrderProduct> _products;
        public IReadOnlyCollection<OrderProduct> Products => _products;

        public PaymentDetails PaymentMethod { get; protected set; }


        public Orders()
        {

        }

        public Orders(int userId, DateTime orderDate, OrderStatus orderStatus, string creditCartNumber, string cvv, DateTime expiredCardDate, string ownerName, string contactMail)
        {
            UserId = userId;
            OrderDate = orderDate;
            OrderStatus = orderStatus;
            _products = new List<OrderProduct>();
            ContactMail = contactMail;

            PaymentMethod = new PaymentDetails(creditCartNumber, cvv, expiredCardDate, ownerName);
        }



        public void AddProduct(int productId, int orderCount, double unitPrice, string productName, string productCode)
        {
            if (OrderStatus == OrderStatus.Draft)
            {
                var product = _products.SingleOrDefault(x => x.ProductId == productId);
                if (product == null)
                {
                    var newProduct = new OrderProduct(productId, orderCount, unitPrice, productName, productCode);
                    _products.Add(newProduct);
                }
                else
                {
                    product.ChangeOrderCount(orderCount);
                    product.ChangeUnitPrice(unitPrice);
                    product.ChangeProductName(productName);
                    product.ChangeProductCode(productCode);
                }
            }
            else
                throw new Exception("Can not add new product if order does not have Draft status");
        }

        public bool ChangeStatus(OrderStatus newStatus)
        {
            if (this.OrderStatus == OrderStatus.Draft && newStatus == OrderStatus.Cancelled)
            {
                this.OrderStatus = newStatus;
                return true;
            }
            else
            if (this.OrderStatus == OrderStatus.Draft && newStatus == OrderStatus.Confirm)
            {
                this.OrderStatus = newStatus;
                return true;
            }
            return false;
        }
    }
}
