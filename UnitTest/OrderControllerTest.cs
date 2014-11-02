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
    public class OrderControllerTest
    {
        //Setup the Stub for testing -> Done once since it is the same for every method.
        private OrdersController controller = new OrdersController(new OrderBLL(new OrderRepositoryStub()));

        [TestMethod]
        public void AactiveOrder_show_partial_view()
        {

        }

        //[TestMethod]
        //public void Create_db_error_post()
        //{
        //    //Arrange
        //    Item input = new Item()
        //    {
        //        ID = 0
        //    };

        //    //Act
        //    ViewResult result = (ViewResult)controller.Create(input);

        //    //Assert
        //    Assert.AreEqual(result.ViewName, "");
        //    Assert.IsInstanceOfType(result.Model, typeof(Item));
        //}

        //[TestMethod]
        //public void Create_ok_post()
        //{
        //    //Arrange
        //    Item input = new Item
        //    {
        //        ID = 1,
        //    };

        //    //Act
        //    RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(input);

        //    //Assert
        //    Assert.AreEqual(result.RouteName, "");
        //    Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        //}

        //[TestMethod]
        //public void Create_show_view()
        //{
        //    //Act
        //    ViewResult result = (ViewResult)controller.Create();

        //    //Assert
        //    Assert.AreEqual(result.ViewName, "");
        //}

        //[TestMethod]
        //public void Create_validation_error_post()
        //{
        //    //Arrange
        //    controller = new ItemsController(new ItemBLL(new ItemRepositoryStub()), new ItemCategoryBLL(new ItemCategoryRepositoryStub()));
        //    controller.ViewData.ModelState.AddModelError("Name", "No name entered");
        //    Item input = new Item();

        //    //Act
        //    ViewResult result = (ViewResult)controller.Create(input);

        //    //Assert
        //    Assert.IsTrue(result.ViewData.ModelState.Count == 1);
        //    Assert.AreEqual(result.ViewName, "");
        //    Assert.IsInstanceOfType(result.Model, typeof(Item));
        //}

        //[TestMethod]
        //public void Delete_cannot_find_id()
        //{
        //    //Arrange
        //    int itemId = 0;
        //    //Act
        //    RedirectToRouteResult result = (RedirectToRouteResult)controller.Delete(itemId);

        //    //Assert
        //    Assert.AreEqual(result.RouteName, "");
        //    Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        //}

        //[TestMethod]
        //public void Delete_show_view()
        //{
        //    //Arrange
        //    int itemId = 1;
        //    //Act
        //    ViewResult result = (ViewResult)controller.Delete(itemId);
        //    //Assert
        //    Assert.AreEqual(result.ViewName, "");
        //    Assert.IsInstanceOfType(result.Model, typeof(Item));
        //}

        //[TestMethod]
        //public void DeleteConfirmed_ok_post()
        //{
        //    //Arrange
        //    int itemId = 1;

        //    //Act
        //    RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(itemId);

        //    //Assert
        //    Assert.AreEqual(result.RouteName, "");
        //    Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        //}

        //[TestMethod]
        //public void Details_show_view()
        //{
        //    //Arrange
        //    int itemId = 1;
        //    //Act
        //    ViewResult result = (ViewResult)controller.Details(itemId);
        //    //Assert
        //    Assert.AreEqual(result.ViewName, "");
        //    Assert.IsInstanceOfType(result.Model, typeof(Item));
        //}

        //[TestMethod]
        //public void Details_cannot_find_id()
        //{
        //    //Arrange
        //    int itemId = 0;
        //    //Act
        //    RedirectToRouteResult result = (RedirectToRouteResult)controller.Details(itemId);

        //    //Assert
        //    Assert.AreEqual(result.RouteName, "");
        //    Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        //}

        //[TestMethod]
        //public void Edit_show_view()
        //{
        //    //Arrange
        //    int itemId = 1;
        //    //Act
        //    ViewResult result = (ViewResult)controller.Details(itemId);
        //    //Assert
        //    Assert.AreEqual(result.ViewName, "");
        //    Assert.IsInstanceOfType(result.Model, typeof(Item));
        //}

        //[TestMethod]
        //public void Edit_cannot_find_id()
        //{
        //    //Arrange
        //    int input = 0;
        //    //Act
        //    RedirectToRouteResult result = (RedirectToRouteResult)controller.Details(input);

        //    //Assert
        //    Assert.AreEqual(result.RouteName, "");
        //    Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        //}

        //[TestMethod]
        //public void Edit_ok_post()
        //{
        //    //Arrange
        //    Item item = new Item
        //    {
        //        ID = 1,
        //    };

        //    //Act
        //    RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(item);

        //    //Assert
        //    Assert.AreEqual(result.RouteName, "");
        //    Assert.AreEqual(result.RouteValues.Values.First(), "Index");
        //}

        //[TestMethod]
        //public void Edit_validation_error_post()
        //{
        //    //Arrange
        //    controller = new ItemsController(new ItemBLL(new ItemRepositoryStub()), new ItemCategoryBLL(new ItemCategoryRepositoryStub()));
        //    controller.ViewData.ModelState.AddModelError("Name", "No name entered");
        //    Item item = new Item();

        //    //Act
        //    ViewResult result = (ViewResult)controller.Edit(item);

        //    //Assert
        //    Assert.IsTrue(result.ViewData.ModelState.Count == 1);
        //    Assert.AreEqual(result.ViewName, "");
        //    Assert.IsInstanceOfType(result.Model, typeof(Item));
        //}

        //[TestMethod]
        //public void Edit_db_error_post()
        //{
        //    //Arrange
        //    Item item = new Item()
        //    {
        //        ID = 0
        //    };

        //    //Act
        //    ViewResult result = (ViewResult)controller.Edit(item);

        //    //Assert
        //    Assert.AreEqual(result.ViewName, "");
        //    Assert.IsInstanceOfType(result.Model, typeof(Item));
        //}



        //[TestMethod]
        //public void Index_show_view()
        //{
        //    //Arrange
        //    List<Item> excpectedResult = new List<Item>()
        //    {
        //           new Item{ItemCategoryID=1, Name="Bukse",Price=99.99m},
        //        new Item{ItemCategoryID=1, Name="Genser",Price=199.99m},
        //        new Item{ItemCategoryID=1, Name="Jakke",Price=299.99m},

        //        new Item{ItemCategoryID=3, Name="Rødvin",Price=100m},
        //        new Item{ItemCategoryID=3, Name="Hvitvin",Price=200m},
        //        new Item{ItemCategoryID=3, Name="Rosévin",Price=350m},

        //        new Item{ItemCategoryID=2, Name="Mazda",Price=350546m},
        //        new Item{ItemCategoryID=2, Name="Kia",Price=251546m},
        //        new Item{ItemCategoryID=2, Name="Toyota",Price=387500m},
        //        new Item{ItemCategoryID=2, Name="Opel",Price=245000m},

        //        new Item{ItemCategoryID=4, Name="Rød",Price=199.5m},
        //        new Item{ItemCategoryID=4, Name="Grønn",Price=199.5m},
        //        new Item{ItemCategoryID=4, Name="Blå",Price=199.5m},
        //        new Item{ItemCategoryID=4, Name="Gul",Price=199.5m},
        //        new Item{ItemCategoryID=4, Name="Brun",Price=199.5m},
        //        new Item{ItemCategoryID=4, Name="Sort",Price=199.5m},
        //        new Item{ItemCategoryID=4, Name="Hvit",Price=189.5m},

        //        new Item{ItemCategoryID=5, Name="Pensko",Price=150m},
        //        new Item{ItemCategoryID=5, Name="Støvler",Price=250m},
        //        new Item{ItemCategoryID=5, Name="Joggesko",Price=349m},
        //        new Item{ItemCategoryID=5, Name="Tøfler",Price=199m},

        //        new Item{ItemCategoryID=6, Name="Mobiltelefon",Price=2948m},
        //        new Item{ItemCategoryID=6, Name="Tannbørste",Price=123m},

        //        new Item{ItemCategoryID=7, Name="Gitar",Price=2478},
        //        new Item{ItemCategoryID=7, Name="CD",Price=159m},
        //    };

        //    //Act
        //    ViewResult result = (ViewResult)controller.Index();
        //    List<Item> resultList = (List<Item>)result.Model;

        //    //Assert
        //    Assert.AreEqual(result.ViewName, "");

        //    for (int i = 0; i < resultList.Count; i++)
        //    {
        //        Assert.AreEqual(excpectedResult[i].Name, resultList[i].Name);
        //    }
        //}



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