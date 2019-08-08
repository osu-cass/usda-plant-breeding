//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.SessionState;
//using Usda.PlantBreeding.Site.Controllers;
//using Usda.PlantBreeding.Data.DataAccess;
//using Usda.PlantBreeding.Data.Models;

//namespace Usda.PlantBreeding.Site.Tests.Controllers
//{
//    [TestClass]
//    public class FamiliesControllerTest
//    {
//        #region Index

//        [TestMethod]
//        public void IndexHappyTest()
//        {
//            //var expected = new List<Family>
//            //{
//            //    new Family
//            //    {
//            //        Id = 12,
//            //        CrossNum = "play him off, keyboard cat",
//            //        GenusId = 12
//            //    },
//            //     new Family
//            //    {
//            //        Id = 13,
//            //        CrossNum = "play him off, keyboard cat",
//            //        GenusId = 12
//            //    }
//            //};


//            //var mockRepo = new Mock<IPlantBreedingRepo>();
//            //mockRepo.Setup(r => r.GetFamilies()).Returns(expected);

//            //var controller = new FamiliesController(mockRepo.Object);

//            //// Mocking Session in test
//            //var httpContext = new Mock<ControllerContext>();
//            //var session = new Mock<HttpSessionStateBase>();

//            //httpContext.Setup(t => t.HttpContext.Session).Returns(session.Object);
//            //httpContext.SetupGet(t => t.HttpContext.Session["GenusId"]).Returns(12);

//            //controller.ControllerContext = httpContext.Object;

//            //var response = controller.Index() as ViewResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.Model);
//            //Assert.IsNotNull(response.Model as IEnumerable<Family>);

//            //var actual = response.Model as IEnumerable<Family>;

//            //Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void IndexEmptyTest()
//        {
//            //var expected = new List<Family>();

//            //var mockRepo = new Mock<IPlantBreedingRepo>();
//            //mockRepo.Setup(r => r.GetFamilies()).Returns(expected);

//            //var response = new FamiliesController(mockRepo.Object).Index() as RedirectToRouteResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.RouteValues);
//            //Assert.AreEqual("Create", response.RouteValues["action"]);
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void IndexNullTest()
//        {
//            //IEnumerable<Family> expected = null;

//            //var mockRepo = new Mock<IPlantBreedingRepo>();
//            //mockRepo.Setup(r => r.GetFamilies()).Returns(expected);

//            //var response = new FamiliesController(mockRepo.Object).Index() as RedirectToRouteResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.RouteValues);
//            //Assert.AreEqual("Create", response.RouteValues["action"]);
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void IndexNullSessionTest()
//        {
//            //var expected = new List<Family>
//            //{
//            //    new Family
//            //    {
//            //        Id = 12,
//            //        Year = "bananas",
//            //        CrossNum = "play him off, keyboard cat",
//            //        GenusId = 12
//            //    },
//            //     new Family
//            //    {
//            //        Id = 13,
//            //        Year = "bananas",
//            //        CrossNum = "play him off, keyboard cat",
//            //        GenusId = 12
//            //    }
//            //};

//            //var mockRepo = new Mock<IPlantBreedingRepo>();
//            //mockRepo.Setup(r => r.GetFamilies()).Returns(expected);

//            //var controller = new FamiliesController(mockRepo.Object);

//            //// Mocking Session in test
//            //var httpContext = new Mock<ControllerContext>();
//            //var session = new Mock<HttpSessionStateBase>();

//            //httpContext.Setup(t => t.HttpContext.Session).Returns(session.Object);
//            //// Set session variable to null
//            //httpContext.SetupGet(t => t.HttpContext.Session["GenusId"]).Returns(null);

//            //controller.ControllerContext = httpContext.Object;

//            //var response = controller.Index() as RedirectToRouteResult;

//            //Assert.IsNotNull(response);
//            //Assert.AreEqual("Index", response.RouteValues["action"]);
//            //Assert.AreEqual("Home", response.RouteValues["controller"]);
//            Assert.IsTrue(false);
//        }

//        #endregion

//        #region Details

//        [TestMethod]
//        public void DetailsHappyTest()
//        {
//            //int? id = 12;
//            //var expected = new Family
//            //{
//            //    Id = 12,
//            //    Year = "bananas",
//            //    CrossNum = "play him off, keyboard cat"
//            //};

//            //var mockRepo = new Mock<IPlantBreedingRepo>();
//            //mockRepo.Setup(r => r.GetFamily(id.Value)).Returns(expected);

//            //var response = new FamiliesController(mockRepo.Object).Details(id) as ViewResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.Model);
//            //Assert.IsNotNull(response.Model as Family);

//            //var actual = response.Model as Family;

//            //Assert.AreEqual(expected, actual);
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void DetailsNullTest()
//        {
//            //int? id = 12;
//            //CrossType expected = null;

//            //var mockRepo = new Mock<IPlantBreedingRepo>();
//            //mockRepo.Setup(r => r.GetCrossType(id.Value)).Returns(expected);

//            //var response = new FamiliesController(mockRepo.Object).Details(id) as HttpNotFoundResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsTrue(response.StatusCode == 404);
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void DetailsBadIdTest()
//        {
//            //int? id = null;

//            //var response = new FamiliesController().Details(id) as HttpStatusCodeResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsTrue(response.StatusCode == 400);
//            Assert.IsTrue(false);
//        }

//        #endregion

//        #region Create

//        /// <summary>
//        /// Tests that the View Data Dictionary contains information.
//        /// </summary>
//        [TestMethod]
//        public void GETCreateTest()
//        {
//            //var expectedCrossTypes = new List<CrossType>
//            //{
//            //    new CrossType
//            //    {
//            //        Id = 12
//            //    }
//            //};
//            //var expectedGenuses = new List<Genus>
//            //{
//            //    new Genus
//            //    {
//            //        Id = 12
//            //    }
//            //};
//            //var expectedOrigins = new List<Origin>
//            //{
//            //    new Origin
//            //    {
//            //        Id = 12
//            //    }
//            //};
//            //var expectedPloidies = new List<Ploidy>
//            //{
//            //    new Ploidy
//            //    {
//            //        Id = 12
//            //    }
//            //};
//            //var expectedPurposes = new List<Purpose>
//            //{
//            //    new Purpose
//            //    {
//            //        Id = 12
//            //    }
//            //};

//            //var mockRepo = new Mock<IPlantBreedingRepo>();
//            //mockRepo.Setup(r => r.GetAllCrossTypes()).Returns(expectedCrossTypes);
//            //mockRepo.Setup(r => r.GetAllGenera()).Returns(expectedGenuses);
//            //mockRepo.Setup(r => r.GetAllOrigins()).Returns(expectedOrigins);
//            //mockRepo.Setup(r => r.GetAllPloidies()).Returns(expectedPloidies);
//            //mockRepo.Setup(r => r.GetAllPurposes()).Returns(expectedPurposes);

//            //var response = new FamiliesController(mockRepo.Object).Create() as ViewResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.ViewData["CrossTypeId"]);
//            //Assert.IsNotNull(response.ViewData["GenusId"]);
//            //Assert.IsNotNull(response.ViewData["OriginId"]);
//            //Assert.IsNotNull(response.ViewData["PloidyId"]);
//            //Assert.IsNotNull(response.ViewData["PurposeId"]);
//            //Assert.IsNotNull(response.ViewData["MaleParent"]);
//            //Assert.IsNotNull(response.ViewData["FemaleParent"]);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void POSTCreateHappyTest()
//        {
//            //    var family = new Family() { Id = 12, IsReciprocal = false };

//            //    var mockRepo = new Mock<IPlantBreedingRepo>();

//            //    var response = new FamiliesController(mockRepo.Object).Create(family) as RedirectToRouteResult;

//            //    Assert.IsNotNull(response);
//            //    Assert.IsNotNull(response.RouteValues);
//            //    Assert.AreEqual("Index", response.RouteValues["action"]);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void POSTCreateValidSessionNullFamilyTest()
//        {
//            //var mockRepo = new Mock<IPlantBreedingRepo>();

//            //var response = new FamiliesController(mockRepo.Object).Create(null) as RedirectToRouteResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.RouteValues);
//            //Assert.AreEqual("Create", response.RouteValues["action"]);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void POSTCreateInvalidSessionTest()
//        {
//            //var family = new Family()
//            //{
//            //    Id = 12,
//            //    CrossTypeId = 13,
//            //    GenusId = 14,
//            //    PloidyId = 15,
//            //    Purpose = new Purpose { Id = 16 },
//            //    MaleGenotype = new Genotype { Id = 17 },
//            //    FemaleGenotype = new Genotype { Id = 18 }
//            //};

//            //var mockRepo = new Mock<IPlantBreedingRepo>();

//            //var controller = new FamiliesController(mockRepo.Object);
//            //controller.ModelState.AddModelError("err", "errerr");

//            //var response = controller.Create(family) as ViewResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.ViewData["CrossTypeId"]);
//            //Assert.IsNotNull(response.ViewData["GenusId"]);
//            //Assert.IsNotNull(response.ViewData["OriginId"]);
//            //Assert.IsNotNull(response.ViewData["PloidyId"]);
//            //Assert.IsNotNull(response.ViewData["PurposeId"]);
//            //Assert.IsNotNull(response.ViewData["MaleParent"]);
//            //Assert.IsNotNull(response.ViewData["FemaleParent"]);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        #endregion

//        #region Edit

//        [TestMethod]
//        public void GETEditHappyTest()
//        {
//            //int? id = 12;
//            //var expected = new Family
//            //{
//            //    Id = 12,
//            //    Year = "bananas",
//            //    CrossNum = "play him off, keyboard cat"
//            //};

//            //var mockRepo = new Mock<IPlantBreedingRepo>();
//            //mockRepo.Setup(r => r.GetFamily(id.Value)).Returns(expected);

//            //var response = new FamiliesController(mockRepo.Object).Edit(id) as ViewResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.Model);
//            //Assert.IsNotNull(response.Model as Family);

//            //var actual = response.Model as Family;

//            //Assert.AreEqual(expected, actual);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void GETEditNullTest()
//        {
//            //int? id = 12;
//            //Family expected = null;

//            //var mockRepo = new Mock<IPlantBreedingRepo>();
//            //mockRepo.Setup(r => r.GetFamily(id.Value)).Returns(expected);

//            //var response = new FamiliesController(mockRepo.Object).Edit(id) as HttpNotFoundResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsTrue(response.StatusCode == 404);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void GETEditBadIdTest()
//        {
//            //    int? id = null;

//            //    var response = new FamiliesController().Edit(id) as HttpStatusCodeResult;

//            //    Assert.IsNotNull(response);
//            //    Assert.IsTrue(response.StatusCode == 400);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        /// <summary>
//        // Tests that the View Data Dictionary contains information.
//        // </summary>
//        [TestMethod]
//        public void GETEditTest()
//        {
//            //var expectedCrossTypes = new List<CrossType>
//            //{
//            //    new CrossType
//            //    {
//            //        Id = 12
//            //    }
//            //};
//            //var expectedGenuses = new List<Genus>
//            //{
//            //    new Genus
//            //    {
//            //        Id = 12
//            //    }
//            //};
//            //var expectedOrigins = new List<Origin>
//            //{
//            //    new Origin
//            //    {
//            //        Id = 12
//            //    }
//            //};
//            //var expectedPloidies = new List<Ploidy>
//            //{
//            //    new Ploidy
//            //    {
//            //        Id = 12
//            //    }
//            //};
//            //var expectedPurposes = new List<Purpose>
//            //{
//            //    new Purpose
//            //    {
//            //        Id = 12
//            //    }
//            //};

//            //var mockRepo = new Mock<IPlantBreedingRepo>();
//            //mockRepo.Setup(r => r.GetAllCrossTypes()).Returns(expectedCrossTypes);
//            //mockRepo.Setup(r => r.GetAllGenera()).Returns(expectedGenuses);
//            //mockRepo.Setup(r => r.GetAllOrigins()).Returns(expectedOrigins);
//            //mockRepo.Setup(r => r.GetAllPloidies()).Returns(expectedPloidies);
//            //mockRepo.Setup(r => r.GetAllPurposes()).Returns(expectedPurposes);

//            //var response = new FamiliesController(mockRepo.Object).Create() as ViewResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.ViewData["CrossTypeId"]);
//            //Assert.IsNotNull(response.ViewData["GenusId"]);
//            //Assert.IsNotNull(response.ViewData["OriginId"]);
//            //Assert.IsNotNull(response.ViewData["PloidyId"]);
//            //Assert.IsNotNull(response.ViewData["PurposeId"]);
//            //Assert.IsNotNull(response.ViewData["MaleParent"]);
//            //Assert.IsNotNull(response.ViewData["FemaleParent"]);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void POSTEditHappyTest()
//        {
//            //var family = new Family() { Id = 12, IsReciprocal = false };

//            //var mockRepo = new Mock<IPlantBreedingRepo>();

//            //var response = new FamiliesController(mockRepo.Object).Edit(family) as RedirectToRouteResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.RouteValues);
//            //Assert.AreEqual("Index", response.RouteValues["action"]);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void POSTEditValidSessionNullFamilyTest()
//        {
//            //var mockRepo = new Mock<IPlantBreedingRepo>();

//            //// Yes, this code is needed to resolve the ambiguity in passing null,
//            //// due to an overload taking a nullable int.
//            //var response = new FamiliesController(mockRepo.Object).Edit(null as Family) as HttpStatusCodeResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsTrue(response.StatusCode == 400);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void POSTEditInvalidSessionTest()
//        {
//            //var family = new Family()
//            //{
//            //    Id = 12,
//            //    CrossTypeId = 13,
//            //    GenusId = 14,
//            //    PloidyId = 15,
//            //    Purpose = new Purpose { Id = 16 },
//            //    MaleGenotype = new Genotype { Id = 17 },
//            //    FemaleGenotype = new Genotype { Id = 18 }
//            //};

//            //var mockRepo = new Mock<IPlantBreedingRepo>();

//            //var controller = new FamiliesController(mockRepo.Object);
//            //controller.ModelState.AddModelError("err", "errerr");

//            //var response = controller.Edit(family) as ViewResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.ViewData["CrossTypeId"]);
//            //Assert.IsNotNull(response.ViewData["GenusId"]);
//            //Assert.IsNotNull(response.ViewData["OriginId"]);
//            //Assert.IsNotNull(response.ViewData["PloidyId"]);
//            //Assert.IsNotNull(response.ViewData["PurposeId"]);
//            //Assert.IsNotNull(response.ViewData["MaleParent"]);
//            //Assert.IsNotNull(response.ViewData["FemaleParent"]);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        #endregion

//        #region Candidates

//        [TestMethod]
//        public void GETCreateFromCandidateHappyTest()
//        {
//            //Candidate testCandidate1 = new Candidate()
//            //{
//            //    Id = 1,
//            //    GenotypeId = 4,
//            //    Genotype = new Genotype { Id = 4 }
//            //};

//            //Candidate testCandidate2 = new Candidate()
//            //{
//            //    Id = 2,
//            //    GenotypeId = 5,
//            //    Genotype = new Genotype { Id = 5, }
//            //};

//            //var mockRepo = new Mock<IPlantBreedingRepo>();

//            ////Define expected results from function calls
//            //mockRepo.Setup(r => r.GetCandidate(testCandidate1.Id)).Returns(testCandidate1);
//            //mockRepo.Setup(r => r.GetCandidate(testCandidate2.Id)).Returns(testCandidate2);

//            //var controller = new FamiliesController(mockRepo.Object);

//            //var response = controller.CreateFromCandidate(testCandidate1.Id, testCandidate2.Id) as ViewResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.ViewData["CrossTypeId"]);
//            //Assert.IsNotNull(response.ViewData["GenusId"]);
//            //Assert.IsNotNull(response.ViewData["OriginId"]);
//            //Assert.IsNotNull(response.ViewData["PloidyId"]);
//            //Assert.IsNotNull(response.ViewData["PurposeId"]);
//            //Assert.IsNotNull(response.ViewData["MaleParent"]);
//            //Assert.IsNotNull(response.ViewData["FemaleParent"]);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        public void GETCreateFromCandidateInvalidModelTest()
//        {
//            //var testFamily = new Family()
//            //{
//            //    Id = 12,
//            //    CrossTypeId = 13,
//            //    GenusId = 14,
//            //    PloidyId = 15,
//            //    Purpose = new Purpose { Id = 16 },
//            //    MaleGenotype = new Genotype { Id = 17 },
//            //    FemaleGenotype = new Genotype { Id = 18 }
//            //};

//            //var mockRepo = new Mock<IPlantBreedingRepo>();

//            //var controller = new FamiliesController(mockRepo.Object);

//            //controller.ModelState.AddModelError("Error", "Something Broke");

//            //var response = controller.CreateFromCandidate(testFamily) as ViewResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.ViewData["CrossTypeId"]);
//            //Assert.IsNotNull(response.ViewData["GenusId"]);
//            //Assert.IsNotNull(response.ViewData["OriginId"]);
//            //Assert.IsNotNull(response.ViewData["PloidyId"]);
//            //Assert.IsNotNull(response.ViewData["PurposeId"]);
//            //Assert.IsNotNull(response.ViewData["MaleParent"]);
//            //Assert.IsNotNull(response.ViewData["FemaleParent"]);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }


//        [TestMethod]
//        public void POSTCreateFromCandidateHappyTest()
//        {
//            //var testFamily = new Family()
//            //{
//            //    Id = 12,
//            //    CrossTypeId = 13,
//            //    GenusId = 14,
//            //    PloidyId = 15,
//            //    Purpose = new Purpose { Id = 16 },
//            //    MaleGenotype = new Genotype { Id = 17 },
//            //    FemaleGenotype = new Genotype { Id = 18 }
//            //};

//            //var mockRepo = new Mock<IPlantBreedingRepo>();

//            //var controller = new FamiliesController(mockRepo.Object);

//            //var response = controller.CreateFromCandidate(testFamily) as RedirectToRouteResult;

//            //Assert.IsNotNull(response);
//            //Assert.AreEqual("Index", response.RouteValues["action"]);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void POSTCreateFromCandidateNullTest()
//        {
//            //Family testFamily = null;

//            //var mockRepo = new Mock<IPlantBreedingRepo>();

//            //var controller = new FamiliesController(mockRepo.Object);

//            //var response = controller.CreateFromCandidate(testFamily) as RedirectToRouteResult;

//            //Assert.IsNotNull(response);
//            //Assert.AreEqual("Create", response.RouteValues["action"]);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        [TestMethod]
//        public void POSTCreateFromCandidateInvalidModelTest()
//        {
//            //var testFamily = new Family()
//            //{
//            //    Id = 12,
//            //    CrossTypeId = 13,
//            //    GenusId = 14,
//            //    PloidyId = 15,
//            //    Purpose = new Purpose { Id = 16 },
//            //    MaleGenotype = new Genotype { Id = 17 },
//            //    FemaleGenotype = new Genotype { Id = 18 }
//            //};

//            //var mockRepo = new Mock<IPlantBreedingRepo>();

//            //var controller = new FamiliesController(mockRepo.Object);

//            //controller.ModelState.AddModelError("Error", "Something Broke");

//            //var response = controller.CreateFromCandidate(testFamily) as ViewResult;

//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response);
//            //Assert.IsNotNull(response.ViewData["CrossTypeId"]);
//            //Assert.IsNotNull(response.ViewData["GenusId"]);
//            //Assert.IsNotNull(response.ViewData["OriginId"]);
//            //Assert.IsNotNull(response.ViewData["PloidyId"]);
//            //Assert.IsNotNull(response.ViewData["PurposeId"]);
//            //Assert.IsNotNull(response.ViewData["MaleParent"]);
//            //Assert.IsNotNull(response.ViewData["FemaleParent"]);

//            //this unit test is not working properly.
//            Assert.IsTrue(false);
//        }

//        #endregion
//    }
//}
