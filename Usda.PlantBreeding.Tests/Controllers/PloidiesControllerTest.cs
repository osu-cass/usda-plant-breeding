using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Usda.PlantBreeding.Site.Controllers;
using System.Collections.Generic;
using Moq;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Data.DataAccess;
using System.Web.Mvc;
using System.Linq;
using Usda.PlantBreeding.Site.Areas.Admin.Controllers;


namespace Usda.PlantBreeding.Site.Tests.Controllers
{
    [TestClass]
    public class PloidiesControllerTest
    {
        #region Index
        [TestMethod]
        public void IndexHappyTest()
        {
            IEnumerable<Ploidy> expected = new List<Ploidy>
            {
                new Ploidy
                {
                    Id = 1,
                    Name = "orgrimmar"
                },
                new Ploidy 
                {
                    Id = 2,
                    Name = "stormwind"
                }
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetPloidies()).Returns(expected);

            var controller = new PloidiesController(mockRepo.Object);
            var response = controller.Index() as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model as IEnumerable<Ploidy>);

            var actual = response.Model as IEnumerable<Ploidy>;

            Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
        }

        [TestMethod]
        public void EmptyIndexTest()
        {
            IEnumerable<Ploidy> expected = new List<Ploidy> { };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetPloidies()).Returns(expected);

            var controller = new PloidiesController(mockRepo.Object);
            var response = controller.Index() as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Create", response.RouteValues["action"]);

        }

        [TestMethod]
        public void NullIndexTest()
        {
            IEnumerable<Ploidy> expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetPloidies()).Returns(expected);

            var controller = new PloidiesController(mockRepo.Object);
            var response = controller.Index() as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Create", response.RouteValues["action"]);

        }

        #endregion

        #region Details
        [TestMethod]
        public void DetailsHappyTest()
        {
            int? id = 1;
            var expected = new Ploidy
            {
                Id = 1,
                Name = "chacha"
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetPloidyById(id.Value)).Returns(expected);

            var controller = new PloidiesController(mockRepo.Object);

            var response = controller.Details(id) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as Ploidy);

            var actual = response.Model as Ploidy;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NullDetailsTest()
        {
            int? id = 12;
            Ploidy expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetPloidyById(id.Value)).Returns(expected);

            var controller = new PloidiesController(mockRepo.Object);

            var response = controller.Details(id) as HttpNotFoundResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 404);
        }

        [TestMethod]
        public void DetailsBadIdTest()
        {
            int? id = null;

            var controller = new PloidiesController();

            var response = controller.Details(id) as HttpStatusCodeResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 400);
        }

        #endregion

        #region Create
        /// <summary>
        /// Tests that when the mdoel state is valid, it calls the save() routine
        /// and redirects to the Index page.
        /// </summary>
        [TestMethod]
        public void CreateHappyTest()
        {
            var ploidy = new Ploidy() { Id = 12, Name = "Banana" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new PloidiesController(mockRepo.Object);

            var response = controller.Create(ploidy) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Index", response.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateWithBadSessionTest()
        {
            var ploidy = new Ploidy() { Id = 12, Name = "Banana" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new PloidiesController(mockRepo.Object);
            controller.ModelState.AddModelError("test", "play them off, keyboard cat");

            var response = controller.Create(ploidy) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as Ploidy);

            var actual = response.Model as Ploidy;

            Assert.AreEqual(ploidy, actual);
        }


        #endregion

        #region Edit
        //
        // These first three tests are exactly the same as the Details tests.
        // The controller logic is exactly the same, so there isn't much to it
        // aside from adding these for increased coverage.
        //

        [TestMethod]
        public void GETEditHappyTest()
        {
            int? id = 12;
            var expected = new Ploidy
            {
                Id = 12,
                Name = "bananas"
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetPloidyById(id.Value)).Returns(expected);

            var controller = new PloidiesController(mockRepo.Object);

            var response = controller.Edit(id) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as Ploidy);

            var actual = response.Model as Ploidy;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GETEditNullTest()
        {
            int? id = 12;
            Ploidy expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetPloidyById(id.Value)).Returns(expected);

            var controller = new PloidiesController(mockRepo.Object);

            var response = controller.Edit(id) as HttpNotFoundResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 404);
        }

        [TestMethod]
        public void GETEditBadIdTest()
        {
            int? id = null;

            var controller = new PloidiesController();

            var response = controller.Edit(id) as HttpStatusCodeResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 400);
        }

        //
        // These next two tests are the same as the Create tests, since the
        // controller logic is exactly the same.  They're here for code coverage
        // purposes.
        //

        [TestMethod]
        public void POSTEditHappyTest()
        {
            var ploidy = new Ploidy() { Id = 12, Name = "Banana" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new PloidiesController(mockRepo.Object);

            var response = controller.Edit(ploidy) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Index", response.RouteValues["action"]);
        }

        [TestMethod]
        public void POSTEditWithBadSessionTest()
        {
            var ploidy = new Ploidy() { Id = 12, Name = "Banana" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new PloidiesController(mockRepo.Object);
            controller.ModelState.AddModelError("test", "play them off, keyboard cat");

            var response = controller.Edit(ploidy) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as Ploidy);

            var actual = response.Model as Ploidy;

            Assert.AreEqual(ploidy, actual);
        }

        #endregion

        #region Delete

        //
        // As with Edit, this code is here for Code Coverage
        //

        [TestMethod]
        public void GETDeleteHappyTest()
        {
            //int? id = 12;
            //var expected = new Ploidy
            //{
            //    Id = 12,
            //    Name = "bananas"
            //};

            //var mockRepo = new Mock<IPlantBreedingRepo>();
            //mockRepo.Setup(r => r.GetPloidyById(id.Value)).Returns(expected);

            //var controller = new PloidiesController(mockRepo.Object);

            //var response = controller.Delete(id) as ViewResult;

            //Assert.IsNotNull(response);
            //Assert.IsNotNull(response.Model);
            //Assert.IsNotNull(response.Model as Ploidy);

            //var actual = response.Model as Ploidy;

            //Assert.AreEqual(expected, actual);

            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void GETDeleteNullTest()
        {
            //int? id = 12;
            //Ploidy expected = null;

            //var mockRepo = new Mock<IPlantBreedingRepo>();
            //mockRepo.Setup(r => r.GetPloidyById(id.Value)).Returns(expected);

            //var controller = new PloidiesController(mockRepo.Object);

            //var response = controller.Delete(id) as HttpNotFoundResult;

            //Assert.IsNotNull(response);
            //Assert.IsTrue(response.StatusCode == 404);

            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void GETDeleteBadIdTest()
        {
            //int? id = null;

            //var controller = new PloidiesController();

            //var response = controller.Delete(id) as HttpStatusCodeResult;

            //Assert.IsNotNull(response);
            //Assert.IsTrue(response.StatusCode == 400);

            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        /// <summary>
        /// Tests that the controller redirects to the Index when finished deleting.
        /// </summary>
        [TestMethod]
        public void POSTDeleteHappyCase()
        {
            //int id = 12;
            //var ploidy = new Ploidy
            //{
            //    Id = 12,
            //    Name = "bananas"
            //};

            //var mockRepo = new Mock<IPlantBreedingRepo>();

            //var controller = new PloidiesController(mockRepo.Object);

            //var response = controller.DeleteConfirmed(id) as RedirectToRouteResult;

            //Assert.IsNotNull(response);
            //Assert.IsNotNull(response.RouteValues);
            //Assert.AreEqual("Index", response.RouteValues["action"]);

            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        #endregion


    }
}
