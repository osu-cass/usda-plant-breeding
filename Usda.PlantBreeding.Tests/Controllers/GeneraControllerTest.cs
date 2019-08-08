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
using System.Web;
using Usda.PlantBreeding.Site.Areas.Admin.Controllers;

namespace Usda.PlantBreeding.Site.Tests.Controllers
{
    [TestClass]
    public class GeneraControllerTest
    {
        #region Index

        [TestMethod]
        public void IndexHappyTest()
        {
            IEnumerable<Genus> expected = new List<Genus>
                {
                    new Genus
                    {
                        Id = 1,
                        Name = "bananas"
                    },
                    new Genus
                    {
                        Id = 2,
                        Name = "play him off, keyboard cat!"
                    }
                };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetAllGenera()).Returns(expected);

            var controller = new GeneraController(mockRepo.Object);
            var response = controller.Index() as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model as IEnumerable<Genus>);

            var actual = response.Model as IEnumerable<Genus>;

            Assert.IsTrue(actual.Count() == actual.Count());
            Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
        }

        [TestMethod]
        public void IndexEmptyTest()
        {
            IEnumerable<Genus> expected = new List<Genus>();

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetAllGenera()).Returns(expected);

            var controller = new GeneraController(mockRepo.Object);
            var response = controller.Index() as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Create", response.RouteValues["action"]);
        }

        [TestMethod]
        public void IndexNullTest()
        {
            IEnumerable<Genus> expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetAllGenera()).Returns(expected);

            var controller = new GeneraController(mockRepo.Object);

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
            int? id = 12;
            var expected = new Genus
            {
                Id = 12,
                Name = "bananas"
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetGenus(id.Value)).Returns(expected);

            var controller = new GeneraController(mockRepo.Object);

            var response = controller.Details(id) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as Genus);

            var actual = response.Model as Genus;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DetailsNullTest()
        {
            int? id = 12;
            Genus expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetGenus(id.Value)).Returns(expected);

            var controller = new GeneraController(mockRepo.Object);

            var response = controller.Details(id) as HttpNotFoundResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 404);
        }

        [TestMethod]
        public void DetailsBadIdTest()
        {
            int? id = null;

            var controller = new GeneraController();

            var response = controller.Details(id) as HttpStatusCodeResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 400);
        }

        #endregion

        #region Create

        /// <summary>
        /// Tests that when the mdoel state is valid, it calls the save() routine
        /// and redirects to the Details Page.
        /// </summary>
        [TestMethod]
        public void CreateHappyTest()
        {
            var genus = new Genus() { Id = 12, Name = "Banana", Value = "Orange" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new GeneraController(mockRepo.Object);

            //Session Variable Mocking
            var httpContext = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>(); 

            httpContext.Setup(t => t.HttpContext.Session).Returns(session.Object);
            httpContext.SetupGet(t => t.HttpContext.Session["genusSet"]).Returns(true);
                        
            controller.ControllerContext = httpContext.Object;

            var response = controller.Create(genus) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Details", response.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateWithBadSessionTest()
        {
            var genus = new Genus() { Id = 12, Name = "Banana", Value="Orange" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new GeneraController(mockRepo.Object);

            //Session Variable Mocking
            var httpContext = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();

            httpContext.Setup(t => t.HttpContext.Session).Returns(session.Object);
            httpContext.SetupGet(t => t.HttpContext.Session["genusSet"]).Returns(true);

            controller.ControllerContext = httpContext.Object;

            controller.ModelState.AddModelError("test", "play them off, keyboard cat");

            var response = controller.Create(genus) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as Genus);

            var actual = response.Model as Genus;

            Assert.AreEqual(genus, actual);
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
            var expected = new Genus
            {
                Id = 12,
                Name = "bananas"
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetGenus(id.Value)).Returns(expected);

            var controller = new GeneraController(mockRepo.Object);

            var response = controller.Edit(id) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as Genus);

            var actual = response.Model as Genus;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GETEditNullTest()
        {
            int? id = 12;
            Genus expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetGenus(id.Value)).Returns(expected);

            var controller = new GeneraController(mockRepo.Object);

            var response = controller.Edit(id) as HttpNotFoundResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 404);
        }

        [TestMethod]
        public void GETEditBadIdTest()
        {
            int? id = null;

            var controller = new GeneraController();

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
            var genus = new Genus() { Id = 12, Name = "Banana", Value = "Orange" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new GeneraController(mockRepo.Object);

            //Session Variable Mocking
            var httpContext = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();

            httpContext.Setup(t => t.HttpContext.Session).Returns(session.Object);
            httpContext.SetupGet(t => t.HttpContext.Session["genusSet"]).Returns(true);

            controller.ControllerContext = httpContext.Object;

            var response = controller.Edit(genus) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Details", response.RouteValues["action"]);
        }

        [TestMethod]
        public void POSTEditWithBadSessionTest()
        {
            var genus = new Genus() { Id = 12, Name = "Banana", Value = "Orange" };
            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new GeneraController(mockRepo.Object);

            //Session Variable Mocking
            var httpContext = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();

            httpContext.Setup(t => t.HttpContext.Session).Returns(session.Object);
            httpContext.SetupGet(t => t.HttpContext.Session["genusSet"]).Returns(true);

            controller.ControllerContext = httpContext.Object;

            controller.ModelState.AddModelError("test", "play them off, keyboard cat");

            var response = controller.Edit(genus) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as Genus);

            var actual = response.Model as Genus;

            Assert.AreEqual(genus, actual);
        }

        #endregion

        #region Delete

        //
        // As with Edit, this code is here for Code Coverage
        //

        [TestMethod]
        public void GETDeleteHappyTest()
        {
            int? id = 12;
            var expected = new Genus
            {
                Id = 12,
                Name = "bananas"
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetGenus(id.Value)).Returns(expected);

            var controller = new GeneraController(mockRepo.Object);

            // var response = controller.Delete(id) as ViewResult;
            var response = new ViewResult();

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as Genus);

            var actual = response.Model as Genus;

            Assert.AreEqual(expected, actual);

            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void GETDeleteNullTest()
        {
            int? id = 12;
            Genus expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetGenus(id.Value)).Returns(expected);

            var controller = new GeneraController(mockRepo.Object);

            //  response = controller.Delete(id) as HttpNotFoundResult;
            var response = new HttpNotFoundResult();

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 404);

            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void GETDeleteBadIdTest()
        {
            int? id = null;

            var controller = new GeneraController();

            // var response = controller.Delete(id) as HttpStatusCodeResult;
            var response = new HttpStatusCodeResult(404);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 400);


            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        /// <summary>
        /// Tests that the controller redirects to the Index when finished deleting.
        /// </summary>
        [TestMethod]
        public void POSTDeleteHappyCase()
        {
            int id = 12;
            var genus = new Genus
            {
                Id = 12,
                Name = "bananas"
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new GeneraController(mockRepo.Object);

            //Session Variable Mocking
            var httpContext = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();

            httpContext.Setup(t => t.HttpContext.Session).Returns(session.Object);
            httpContext.SetupGet(t => t.HttpContext.Session["genusSet"]).Returns(true);

            controller.ControllerContext = httpContext.Object;

            //var response = controller.DeleteConfirmed(id) as RedirectToRouteResult;
            var response = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary());

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Index", response.RouteValues["action"]);

            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        #endregion

        #region Questions

        //Creates a question object, saves it, and returns to genus details screen
        [TestMethod]
        public void QuestionCreateHappyTest()
        {
            //Test parameters
            var required = true;
            var genusId = 1;
            var questionText = "Favorite Color";
            var questionLabel = "Color";
            //Initialize controller and repo
            var mockRepo = new Mock<IPlantBreedingRepo>();
            var controller = new GeneraController(mockRepo.Object);

            var response = controller.QuestionCreate(questionText, genusId, required, questionLabel) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Details", response.RouteValues["action"]);
        }

        [TestMethod]
        public void QuestionCreateInvalidModelState()
        {
            //Test parameters
            var required = true;
            var genusId = 1;
            var questionText = "Favorite Color";
            var questionLabel = "Color";

            //Initialize controller and repo
            var mockRepo = new Mock<IPlantBreedingRepo>();
            var controller = new GeneraController(mockRepo.Object);

            //Add modelstate error
            controller.ModelState.AddModelError("Example Error", "Something broke");

            var response = controller.QuestionCreate(questionText, genusId, required, questionLabel) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.IsTrue(controller.ModelState.IsValid.Equals(false));
            Assert.AreEqual("Index", response.RouteValues["action"]);
        }

        [TestMethod]
        public void QuestionRetireHappyTest()
        {
            Question testQuestion = new Question()
            {
                Id = 1,
                GenusId = 1,
                Label = "Color",
                Text = "Favorite Color"
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            var controller = new GeneraController(mockRepo.Object);

            mockRepo.Setup(r => r.GetQuestion(testQuestion.Id)).Returns(testQuestion);

            var response = controller.QuestionRetire(testQuestion.Id) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.AreEqual("Details", response.RouteValues["action"]);
            Assert.AreEqual(1, response.RouteValues["id"]);
        }

        [TestMethod]
        public void QuestionRetireNullTest()
        {
            int? testInt = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            var controller = new GeneraController(mockRepo.Object);

            var response = controller.QuestionRetire(testInt) as HttpStatusCodeResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 400);
        }

        [TestMethod]
        public void QuestionUnRetireHappyTest()
        {
            Question testQuestion = new Question()
            {
                Id = 1,
                GenusId = 1,
                Label = "Color",
                Text = "Favorite Color",
                Retired = true
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            var controller = new GeneraController(mockRepo.Object);

            mockRepo.Setup(r => r.GetQuestion(testQuestion.Id)).Returns(testQuestion);

            var response = controller.QuestionUnRetire(testQuestion.Id) as RedirectToRouteResult;
            
            Assert.IsNotNull(response);
            Assert.AreEqual("Details", response.RouteValues["action"]);
            Assert.AreEqual(1, response.RouteValues["id"]);
        }

        [TestMethod]
        public void QuestionUnRetireNullTest()
        {
            int? testInt = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            var controller = new GeneraController(mockRepo.Object);

            var response = controller.QuestionUnRetire(testInt) as HttpStatusCodeResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 400);
        }

        [TestMethod]
        public void GETQuestionUpdateHappyTest()
        {
            Question testQuestion = new Question()
            {
                Id = 1,
                GenusId = 1,
                Label = "Color",
                Text = "Favorite Color",
                Retired = false
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            var controller = new GeneraController(mockRepo.Object);

            mockRepo.Setup(r => r.GetQuestion(testQuestion.Id)).Returns(testQuestion);

            var response = controller.QuestionUpdate(testQuestion.Id) as PartialViewResult;

            Assert.IsNotNull(response);
            Assert.AreEqual("_QuestionEdit", response.ViewName);
        }
        #endregion
    }
}
