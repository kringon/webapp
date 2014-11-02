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
    public class ItemCategoriesControllerTest
    {
        //Setup the Stub for testing -> Done once since it is the same for every method.
        private ItemCategoriesController controller = new ItemCategoriesController(new ItemCategoryBLL(new ItemCategoryRepositoryStub()));

        [TestMethod]
        public void Create_db_error_post()
        {
            //Arrange
            ItemCategory input = new ItemCategory()
            {
                ID = 0
            };

            //Act
            ViewResult result = (ViewResult)controller.Create(input);

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(ItemCategory));
        }

        [TestMethod]
        public void Create_ok_post()
        {
            //Arrange
            ItemCategory input = new ItemCategory
            {
                ID = 1,
                Name = "Klær"
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
            controller = new ItemCategoriesController(new ItemCategoryBLL(new ItemCategoryRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("Name", "No name entered");
            ItemCategory input = new ItemCategory();

            //Act
            ViewResult result = (ViewResult)controller.Create(input);

            //Assert
            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(ItemCategory));
        }

        [TestMethod]
        public void Delete_cannot_find_id()
        {
            //Arrange
            int itemCategoryID = 0;
            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Delete(itemCategoryID);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Delete_show_view()
        {
            //Arrange
            int itemCategoryID = 1;
            //Act
            ViewResult result = (ViewResult)controller.Delete(itemCategoryID);
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(ItemCategory));
        }

        [TestMethod]
        public void DeleteConfirmed_ok_post()
        {
            //Arrange
            int itemCategoryID = 1;

            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(itemCategoryID);
         
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Details_show_view()
        {
            //Arrange
            int input = 1;
            //Act
            ViewResult result = (ViewResult)controller.Details(input);
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(ItemCategory));
        }

        [TestMethod]
        public void Details_cannot_find_id()
        {
            //Arrange
            int input = 0;
            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Details(input);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Edit_show_view()
        {
            //Arrange
            int input = 1;
            //Act
            ViewResult result = (ViewResult)controller.Details(input);
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(ItemCategory));
        }

        [TestMethod]
        public void Edit_cannot_find_id()
        {
            //Arrange
            int input = 0;
            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Details(input);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Edit_ok_post()
        {
            //Arrange
            ItemCategory input = new ItemCategory
            {
                ID = 1,
                Name = "Klær"
            };

            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(input);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Edit_validation_error_post()
        {
            //Arrange
            controller = new ItemCategoriesController(new ItemCategoryBLL(new ItemCategoryRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("Name", "No name entered");
            ItemCategory input = new ItemCategory();

            //Act
            ViewResult result = (ViewResult)controller.Edit(input);

            //Assert
            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(ItemCategory));
        }

        [TestMethod]
        public void Edit_db_error_post()
        {
            //Arrange
            ItemCategory input = new ItemCategory()
            {
                ID = 0
            };

            //Act
            ViewResult result = (ViewResult)controller.Edit(input);

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsInstanceOfType(result.Model, typeof(ItemCategory));
        }



        [TestMethod]
        public void Index_show_view()
        {
            //Arrange
            List<ItemCategory> excpectedResult = new List<ItemCategory>()
            {
                new ItemCategory{Name="Klær"},
                new ItemCategory{Name="Biler"},
                new ItemCategory{Name="Vin"},
                new ItemCategory{Name="Maling"},
                new ItemCategory{Name="Sko"},
                new ItemCategory{Name="Elektronikk"},
                new ItemCategory{Name="Musikk"},
            };

            //Act
            ViewResult result = (ViewResult)controller.Index();
            List<ItemCategory> resultList = (List<ItemCategory>)result.Model;

            //Assert
            Assert.AreEqual(result.ViewName, "");

            for (int i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(excpectedResult[i].Name, resultList[i].Name);
            }
        }
        //[TestMethod]
        //public void Delete_show_view()
        //{
        //    //Arrange

        //    //Act

        //    //Assert
        //}
    }
}