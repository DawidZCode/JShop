using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderModule.Domain.AggregateModel;
using System;

namespace OrderModule.Test
{
    [TestClass]
    public class OrdersUnitTest
    {
        [TestMethod]
        public void OrderChangeStatusTest()
        {
            Orders order = new Orders(1, DateTime.Now, OrderStatus.Draft, "23423", "323", DateTime.Now.AddYears(1), "Adam Nowak", "adam.nowak@mail.com");
            Assert.AreEqual(order.ChangeStatus(OrderStatus.Cancelled), true);

            order = new Orders(1, DateTime.Now, OrderStatus.Draft, "23423", "323", DateTime.Now.AddYears(1), "Adam Nowak", "adam.nowak@mail.com");
            Assert.AreEqual(order.ChangeStatus(OrderStatus.Confirm), true);

            order = new Orders(1, DateTime.Now, OrderStatus.Draft, "23423", "323", DateTime.Now.AddYears(1), "Adam Nowak", "adam.nowak@mail.com");
            Assert.AreEqual(order.ChangeStatus(OrderStatus.Cancelled), true);

            order = new Orders(1, DateTime.Now, OrderStatus.Draft, "23423", "323", DateTime.Now.AddYears(1), "Adam Nowak", "adam.nowak@mail.com");
            Assert.AreEqual(order.ChangeStatus(OrderStatus.Done), false);
        }


    }
}
