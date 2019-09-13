using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderModule.Core.AggregateModel
{
    public class Orders
    {
        public int Id { get;  set; }

        public int UserId { get; protected set; }

        public DateTime OrderDate { get; protected set; }

        public OrderStatus OrderStatus { get; protected set; }

        private List<OrderProduct> _products;
        public IReadOnlyCollection<OrderProduct> Products => _products;

        public PaymentDetails PaymentMethod { get; protected set; }


        public Orders()
        {

        }

        public Orders(int userId, DateTime orderDate, OrderStatus orderStatus, string creditCartNumber, string cvv, DateTime expiredCardDate, string ownerName )
        {
            UserId = userId;
            OrderDate = orderDate;
            OrderStatus = orderStatus;
            _products = new List<OrderProduct>();

            PaymentMethod = new PaymentDetails(creditCartNumber, cvv, expiredCardDate, ownerName);
        }

        public void AddProduct(int productId, int orderCount, double unitPrice, string productName, string productCodeCode)
        {
            if (OrderStatus == OrderStatus.Draft)
            {
                var product = _products.SingleOrDefault(x => x.ProductId == productId);
                if (product == null)
                {
                    var newProduct = new OrderProduct(productId, orderCount, unitPrice, productName, productCodeCode);
                    _products.Add(newProduct);
                }
                else
                {
                    product.ChangeOrderCount(orderCount);
                    product.ChangeUnitPrice(unitPrice);
                    product.ChangeProductName(productName);
                    product.ChangeProductCodeCode(productCodeCode);
                }
            }
            else
                throw new Exception("Can not add new product if order does not have Draft status");
        }




        //private int _userId { get; set; }

        //private DateTime _orderDate { get; set; }

        //private OrderStatus _orderStatus { get; set; }



    }
}
