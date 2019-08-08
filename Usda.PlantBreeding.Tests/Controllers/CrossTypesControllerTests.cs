using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Usda.PlantBreeding.Site.Controllers;
using Usda.PlantBreeding.Data;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Moq;
using Usda.PlantBreeding.Data.Models;
using System.Collections.Generic;
using Usda.PlantBreeding.Site.Areas.Admin.Controllers;
using System.Web.Routing;

namespace Usda.PlantBreeding.Site.Tests.Controllers
{
    [TestClass]
    public class CrossTypesControllerTests
    {
        #region Index

        [TestMethod]
        public void IndexHappyTest()
        {
            var expected = new List<Genus>
            {
                new Genus
                {
                    Id = 3,
                    Name = "Rubus",
                    DefaultPlantsInRep = 3,
                    VirusLabel1 = "RBDV",
                    VirusLabel2 = "ToRSV",
                    VirusLabel3 = "SNSV",
                    Retired = false,
                    ExternalId = null,
                    VirusLabel4 = "BCRV"
                }
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetAllGenera()).Returns(expected);

            var controller = new CrossTypesController(mockRepo.Object);
            var response = controller.Index() as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model as List<Genus>);

            var actual = response.Model as List<Genus>;

            Assert.IsTrue(actual.Count() == expected.Count());
            Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
            //Assert.AreEqual("Index", response.RouteValues["action"]);

            //response.

            //Assert.IsTrue(actual.Count() == actual.Count());
            //Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
            ////this unit test is not working properly.
            //Assert.IsTrue(false);
        }

        [TestMethod]
        public void IndexEmptyTest()
        {
            var expected = new List<CrossType>();

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCrossTypes()).Returns(expected);

            var controller = new CrossTypesController(mockRepo.Object);
            var response = controller.Index() as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Index", response.RouteValues["action"]);
        }

        [TestMethod]
        public void IndexNullTest()
        {
            IEnumerable<CrossType> expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCrossTypes()).Returns(expected);

            var controller = new CrossTypesController(mockRepo.Object);
            var response = controller.Index() as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Index", response.RouteValues["action"]);
        }

        #endregion

        #region Details

        [TestMethod]
        public void DetailsHappyTest()
        {
            int? id = 12;
            var expected = new CrossType
            {
                Id = 12,
                Name = "bananas"
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCrossType(id.Value)).Returns(expected);

            var controller = new CrossTypesController(mockRepo.Object);

            var response = controller.Details(id) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as CrossType);

            var actual = response.Model as CrossType;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DetailsNullTest()
        {
            int? id = 12;
            CrossType expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCrossType(id.Value)).Returns(expected);

            var controller = new CrossTypesController(mockRepo.Object);

            var response = controller.Details(id) as HttpNotFoundResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 404);
        }

        [TestMethod]
        public void DetailsBadIdTest()
        {
            int? id = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            //mockRepo.Setup(r => r.GetCrossType(id.Value));
            var controller = new CrossTypesController(mockRepo.Object);

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
            var crossType = new CrossType() { Id = 12, Name = "Banana" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new CrossTypesController(mockRepo.Object);

            var response = controller.Create(crossType) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("IndexByGenus", response.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateWithBadSessionTest()
        {
            var crossType = new CrossType() { Id = 12, Name = "Banana" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new CrossTypesController(mockRepo.Object);
            controller.ModelState.AddModelError("test", "play them off, keyboard cat");

            var response = controller.Create(crossType) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as CrossType);

            var actual = response.Model as CrossType;

            Assert.AreEqual(crossType, actual);
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
            var expected = new CrossType
            {
                Id = 12,
                Name = "bananas"
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCrossType(id.Value)).Returns(expected);

            var controller = new CrossTypesController(mockRepo.Object);

            var response = controller.Edit(id) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as CrossType);

            var actual = response.Model as CrossType;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GETEditNullTest()
        {
            int? id = 12;
            CrossType expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCrossType(id.Value)).Returns(expected);

            var controller = new CrossTypesController(mockRepo.Object);

            var response = controller.Edit(id) as HttpNotFoundResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 404);
        }

        [TestMethod]
        public void GETEditBadIdTest()
        {
            int? id = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            var controller = new CrossTypesController(mockRepo.Object);

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
            var crossType = new CrossType() { Id = 12, Name = "Banana" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new CrossTypesController(mockRepo.Object);

            var response = controller.Edit(crossType) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("IndexByGenus", response.RouteValues["action"]);
        }

        [TestMethod]
        public void POSTEditWithBadSessionTest()
        {
            var crossType = new CrossType() { Id = 12, Name = "Banana" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new CrossTypesController(mockRepo.Object);
            controller.ModelState.AddModelError("test", "play them off, keyboard cat");

            var response = controller.Edit(crossType) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as CrossType);

            var actual = response.Model as CrossType;

            Assert.AreEqual(crossType, actual);
        }

        #endregion

        #region Delete

        //
        // As with Edit, this code is here for Code Coverage
        //

        [TestMethod]
        public void GETDeleteHappyTest()
        {
            var expected = new CrossType() { Id = 12, Name = "Banana" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new CrossTypesController(mockRepo.Object);

            var response1 = controller.Create(expected) as RedirectToRouteResult;

            controller.Retire(expected.Id, expected.Id);

            var response2 = controller.Details(expected.Id) as HttpNotFoundResult;

            Assert.IsNotNull(response1);
            Assert.IsNotNull(response1.RouteValues);
            Assert.AreEqual("IndexByGenus", response1.RouteValues["action"]);
            
            Assert.IsTrue(response2.StatusCode == 404);
        }

        [TestMethod]
        public void GETDeleteNullTest()
        {
            int? id = 12;
            CrossType expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCrossType(id.Value)).Returns(expected);

            var controller = new CrossTypesController(mockRepo.Object);

            var response = controller.Retire(null, null) as HttpStatusCodeResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 400);
        }

        [TestMethod]
        public void GETDeleteBadIdTest()
        {
            int? id = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            var controller = new CrossTypesController(mockRepo.Object);

            var response = controller.Retire(id, null) as HttpStatusCodeResult;
            //var response = new HttpStatusCodeResult(400);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 400);
        }

        /// <summary>
        /// Tests that the controller redirects to the Index when finished deleting.
        /// </summary>
        [TestMethod]
        public void POSTDeleteHappyCase()
        {
            int? id = 12;
            var crossType = new CrossType
            {
                Id = 12,
                Name = "bananas"
            };
            var genus = new List<Genus>
            {
                new Genus
                {
                    Id = 3,
                    Name = "Rubus",
                    DefaultPlantsInRep = 3,
                    VirusLabel1 = "RBDV",
                    VirusLabel2 = "ToRSV",
                    VirusLabel3 = "SNSV",
                    Retired = false,
                    ExternalId = null,
                    VirusLabel4 = "BCRV"
                }
            };


            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCrossType(id.Value)).Returns(crossType);
            mockRepo.Setup(r => r.GetAllGenera()).Returns(genus);

            var controller = new CrossTypesController(mockRepo.Object);

            var response = controller.Retire(id, 3) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("IndexByGenus", response.RouteValues["action"]);
        }

        #endregion
    }
}
