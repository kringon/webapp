using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebWarehouse.BLL;
using WebWarehouse.Controllers;
using WebWarehouse.DAL;
using WebWarehouse.Model;

namespace WebWarehouse.UnitTest
{
    [TestClass]
    public class OrderControllerTest
    {
        //Setup the Stub for testing -> Done once since it is the same for every method.
        private OrdersController controller = new OrdersController(new OrderBLL(new OrderRepositoryStub()));

        //[TestMethod]
        //public void AddItem_ajax()
        //{
        //    //Arrange
        //    int itemid = 1;
        //    int userid = 1;

        //    //Act
        //    JsonResult result = (JsonResult)controller.AddItem(itemid, userid);

        //    //Assert
        //    Assert.IsInstanceOfType(result.ContentType, typeof(JsonResult));
        //}

        [TestMethod]
        public void Create_db_error_post()
        {
            //Arrange
            Order input = new Order()
            {
                    ID=0
            };

            //Act
            ViewResult result = (ViewResult)controller.Create(input);

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(Order));
        }

        [TestMethod]
        public void Create_ok_post()
        {
            //Arrange
            Order input = new Order
            {
                ID = 1,
            };

            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(input);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Create_show_view()
        {
            //Act
            ViewResult result = (ViewResult)controller.Create();

            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void Create_validation_error_post()
        {
            //Arrange
            controller = new OrdersController(new OrderBLL(new OrderRepositoryStub()));
          
            controller.ViewData.ModelState.AddModelError("Ordered", "Must be a date");
            Order input = new Order();

            //Act
            ViewResult result = (ViewResult)controller.Create(input);

            //Assert
            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(Order));
        }

        [TestMethod]
        public void Delete_cannot_find_id()
        {
            //Arrange
            int orderId = 0;
            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Delete(orderId);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Delete_show_view()
        {
            //Arrange
            int orderId = 1;
            //Act
            ViewResult result = (ViewResult)controller.Delete(orderId);
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(Order));
        }

        [TestMethod]
        public void DeleteConfirmed_ok_post()
        {
            //Arrange
            int orderId = 1;

            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(orderId);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Details_show_view()
        {
            //Arrange
            int orderId = 1;
            //Act
            ViewResult result = (ViewResult)controller.Details(orderId);
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(Order));
        }

        [TestMethod]
        public void Details_cannot_find_id()
        {
            //Arrange
            int orderId = 0;
            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Details(orderId);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Edit_show_view()
        {
            //Arrange
            int orderId = 1;
            //Act
            ViewResult result = (ViewResult)controller.Edit(orderId);
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(Order));
        }

        [TestMethod]
        public void Edit_cannot_find_id()
        {
            //Arrange
            int orderId = 0;
            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(orderId);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Edit_ok_post()
        {
            //Arrange
            Order order = new Order
            {
                ID = 1,
            };

            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(order);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Edit_validation_error_post()
        {
            //Arrange
            controller = new OrdersController(new OrderBLL(new OrderRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("Ordered", "Must be a date");
            Order order = new Order();

            //Act
            ViewResult result = (ViewResult)controller.Edit(order);

            //Assert
            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(Order));
        }

        [TestMethod]
        public void Edit_db_error_post()
        {
            //Arrange
            Order order = new Order()
            {
                ID = 0
            };

            //Act
            ViewResult result = (ViewResult)controller.Edit(order);

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(Order));
        }



        [TestMethod]
        public void Index_show_view()
        {
            //Arrange

            List<Order> excpectedResult = new List<Order>{
                new Order{Ordered=DateTime.Parse("2014-04-04"), Delivered=DateTime.Parse("2014-05-05"),Status=OrderEnum.Ordered},
                new Order{Ordered=DateTime.Parse("2014-03-04"), Delivered=DateTime.Parse("2014-05-05"),Status=OrderEnum.Ordered},
                new Order{Ordered=DateTime.Parse("2014-02-04"), Delivered=DateTime.Parse("2014-05-05"),Status=OrderEnum.Ordered}
            };
         
            //Act
            ViewResult result = (ViewResult)controller.Index();
            List<Order> resultList = (List<Order>)result.Model;

            //Assert
            Assert.AreEqual(result.ViewName, "");

            for (int i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(excpectedResult[i].Status, resultList[i].Status);
            }
        }



    }
}