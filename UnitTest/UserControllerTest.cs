using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class UserControllerTest
    {
        //Setup the Stub for testing -> Done once since it is the same for every method.
        private UsersController controller = new UsersController(new UserBLL(new UserRepositoryStub()));

        [TestMethod]
        public void AactiveOrder_show_partial_view()
        {
            //Arrange
            int userId = 1;

            //Act
            PartialViewResult result = (PartialViewResult) controller.ActiveOrder(userId);

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(Order));
        }

        [TestMethod]
        public void AactiveOrder_cannot_find_userID()
        {
            //Arrange
            int userId = 0;

            //Act
            PartialViewResult result = (PartialViewResult)controller.ActiveOrder(userId);

            //Assert
            Assert.AreEqual(result.ViewName, "ActiveOrder");
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void Create_db_error_post()
        {
            //Arrange
            User input = new User()
            {
                ID = 4
            };

            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(input);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues["controller"], "Users");
            Assert.AreEqual(result.RouteValues["action"], "Create");
        }

        [TestMethod]
        public void Create_ok_post()
        {
            //Arrange
            User input = new User
            {
                ID = 1,
            };

            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(input);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues["controller"],"Home");
            Assert.AreEqual(result.RouteValues["action"], "Index");
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
            controller = new UsersController(new UserBLL(new UserRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("Username", "No username entered");
            User input = new User();

            //Act
            ViewResult result = (ViewResult)controller.Create(input);

            //Assert
            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(User));
        }

        [TestMethod]
        public void Delete_cannot_find_id()
        {
            //Arrange
            int userId = 0;
            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Delete(userId);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Delete_show_view()
        {
            //Arrange
            int userId = 1;
            //Act
            ViewResult result = (ViewResult)controller.Delete(userId);
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(User));
        }

        [TestMethod]
        public void DeleteConfirmed_ok_post()
        {
            //Arrange
            int userId = 1;

            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(userId);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Details_show_view()
        {
            //Arrange
            int userId = 1;
            //Act
            ViewResult result = (ViewResult)controller.Details(userId);
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(User));
        }

        [TestMethod]
        public void Details_cannot_find_id()
        {
            //Arrange
            int userId = 0;
            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Details(userId);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }


        //PGA elendig bruk av session-variabler i controllerene, så vil det ikke fungere å unit-teste mot de, da session = null. Kunne brukt rammeverk for å mocke sessions, evt lagd validering på annen måte med mer tid.
        [TestMethod]
        public void Edit_show_view()
        {

            //Arrange
            int userId = 1;
            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(userId);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues["controller"],"Home");
            Assert.AreEqual(result.RouteValues["action"], "Index");
         
        }



        [TestMethod]
        public void Index_show_view()
        {
            //Arrange
            List<Item> excpectedResult = new List<Item>()
            {
               
            };

            //Act
            ViewResult result = (ViewResult)controller.Index();
            List<Item> resultList = (List<Item>)result.Model;

            //Assert
            Assert.AreEqual(result.ViewName, "");

            for (int i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(excpectedResult[i].Name, resultList[i].Name);
            }
        }



        //[TestMethod]
        //public void ListByCategory_show_view()
        //{
        //    //Arrange
        //    int itemCategoryID = 4;
        //    List<Item> excpectedResult = new List<Item>(){
        //           new Item{ItemCategoryID=4, Name="Rød",Price=199.5m},
        //        new Item{ItemCategoryID=4, Name="Grønn",Price=199.5m},
        //        new Item{ItemCategoryID=4, Name="Blå",Price=199.5m},
        //        new Item{ItemCategoryID=4, Name="Gul",Price=199.5m},
        //        new Item{ItemCategoryID=4, Name="Brun",Price=199.5m},
        //        new Item{ItemCategoryID=4, Name="Sort",Price=199.5m},
        //        new Item{ItemCategoryID=4, Name="Hvit",Price=189.5m},
        //    };

        //    //Act
        //    ViewResult result = (ViewResult)controller.ListByCategory(itemCategoryID);
        //    List<Item> resultList = (List<Item>)result.Model;
        //    //Assert
        //    Assert.AreEqual(result.ViewName, "List");
        //    Assert.IsInstanceOfType(result.Model, typeof(List<Item>));
        //    for (int i = 0; i < resultList.Count; i++)
        //    {
        //        Assert.AreEqual(excpectedResult[i].Name, resultList[i].Name);
        //    }
        //}



        //[TestMethod]
        //public void Delete_show_view()
        //{
        //    //Arrange

        //    //Act

        //    //Assert
        //}
    }
}