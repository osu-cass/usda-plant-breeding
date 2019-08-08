using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Site.App_Start;
using System.Web.Mvc;

using Usda.PlantBreeding.Site.Areas.Accessions.Controllers;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Models;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Security.Principal;
using System.Web.Routing;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;
using System.Net;

namespace Usda.PlantBreeding.Site.Tests.Controllers
{
    [TestClass]
    public class AccessionsDefaultControllerTest
    {
        [TestInitialize]
        public void InitializeTests()
        {
            AutoMapperConfig.Configure();
        }

        //#region Index

        ////Checks [Post] Index to see if it redirects properly. 
        //[TestMethod]
        //public void IndexHappyTest()
        //{
        //    var mockRepo = new Mock<IPlantBreedingRepo>();
        //    var controller = new DefaultController(mockRepo.Object);

        //    string searchString = "Example String";
        //    var response = controller.Index(1, 3, searchString) as RedirectToRouteResult;

        //    Assert.IsNotNull(response);
        //    Assert.AreEqual("List", response.RouteValues["action"]);

        //    //this unit test is not working properly.
        //    Assert.IsTrue(false);

        //}

        //// Passes empty string into [Post] Index and checks to see if it
        //// returns to the view as expected. 
        //[TestMethod]
        //public void IndexEmptyTest()
        //{
        //    int? id = 12;
        //    var userId = "testUser";
        //    var genotype = new Genotype
        //    {
        //        Id = 12,
        //        SelectionNum = 2,
        //        GivenName = "bananas",
        //        FamilyId = 14,
        //        Year = "2005",
        //        Note = null,
        //        Fate = null,
        //        ExternalId = 14141,
        //        IsRoot = true,
        //        Alias1 = null,
        //        Alias2 = null,
        //        OriginalName = "plantain",
        //        PatentApp = null,
        //        Patent = null,
        //        PatentYear = null,
        //        PloidyName = null,
        //        CrossPlanId = null,
        //        CreatedDate = null,
        //        IsPopulation = false
        //    };
        //    var fam = new Family
        //    {
        //        Id = 14,
        //        CrossNum = "4486",
        //        FieldPlantedNum = 144,
        //        TransplantedNum = null,
        //        GenusId = 3,
        //        IsReciprocal = null,
        //        OriginId = 108,
        //        SeedNum = 650,
        //        CrossTypeId = null,
        //        Purpose = null,
        //        FemaleParent = null,
        //        MaleParent = null,
        //        ReciprocalString = "N",
        //        BaseGenotypeId = 14141,
        //        FemaleParentOriginalName = "plant",
        //        MaleParentOriginalName = "tain",
        //        Unsuccessful = false,
        //    };
        //    genotype.Family = fam;

        //    var mockRepo = new Mock<IPlantBreedingRepo>();
        //    HttpContext.Current = FakeHttpContext(userId);

        //    var userPreference = new UserPreference();
        //    userPreference.UserId = userId;
        //    userPreference.GenusId = 3;
        //    var userPreferences = new List<UserPreference>
        //    {
        //        userPreference
        //    };
        //    //var userPreferences = new Mock<IEnumerable<UserPreference>>();
        //    //userPreferences.Setup(r => r.SingleOrDefault(t => t.UserId == userId)).Returns(userPreference);

        //    mockRepo.Setup(r => r.GetGenotype(id.Value)).Returns(genotype);
        //    mockRepo.Setup(r => r.GetFamily(id.Value)).Returns(fam);
        //    mockRepo.Setup(r => r.GetUserPreference("testUser")).Returns(userPreference);
        //    mockRepo.Setup(r => r.GetUserPreferences()).Returns(userPreferences);

        //    var controller = new DefaultController(mockRepo.Object);

        //    string searchString = "";
        //    var response = controller.Index(1, 3, searchString) as RedirectToRouteResult;

        //    Assert.IsNotNull(response);
        //    Assert.AreEqual("Index", response.RouteValues["action"]);
        //}

        //// Passes empty string into [Post] Index and checks to see if it
        //// returns to the view as expected. 
        //[TestMethod]
        //public void IndexNullTest()
        //{
        //    var mockRepo = new Mock<IPlantBreedingRepo>();
        //    var controller = new DefaultController(mockRepo.Object);

        //    string searchString = null;
        //    var response = controller.Index(null, null, searchString) as RedirectToRouteResult;

        //    Assert.IsNotNull(response);
        //    Assert.AreEqual("Index", response.RouteValues["action"]);
        //}

        //#endregion
        
        #region Details

        [TestMethod]
        public void DetailsHappyTest()
        {
            int? id = 12;
            var genotype = new Genotype
            {
                Id = 12,
                SelectionNum = 2,
                GivenName = "bananas",
                FamilyId = 14,
                Year = "2005",
                Note = null,
                Fate = null,
                ExternalId = 14141,
                IsRoot = true,
                Alias1 = null,
                Alias2 = null,
                OriginalName = "plantain",
                PatentApp = null,
                Patent = null,
                PatentYear = null,
                PloidyName = null,
                CrossPlanId = null,
                CreatedDate = null,
                IsPopulation = false
            };
            var fam = new Family
            {
                Id = 14,
                CrossNum = "4486",
                FieldPlantedNum = 144,
                TransplantedNum = null,
                GenusId = 3,
                IsReciprocal = null,
                OriginId = 108,
                SeedNum = 650,
                CrossTypeId = null,
                Purpose = null,
                FemaleParent = null,
                MaleParent = null,
                ReciprocalString = "N",
                BaseGenotypeId = 14141,
                FemaleParentOriginalName = "plant",
                MaleParentOriginalName = "tain",
                Unsuccessful = false
            };
            genotype.Family = fam;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetGenotype(id.Value)).Returns(genotype);
            mockRepo.Setup(r => r.GetFamily(id.Value)).Returns(fam);

            var controller = new DefaultController(mockRepo.Object);

            var response = controller.Details(id.Value) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as AccessionViewModel);

            var actual = response.Model as AccessionViewModel;

            Assert.AreEqual(genotype.Id, actual.Id);
        }

        [TestMethod]
        public void DetailsNullTest()
        {
            int? id = 12;
            Genotype expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetGenotype(id.Value)).Returns(expected);

            var controller = new DefaultController(mockRepo.Object);

            var response = controller.Details(id) as HttpNotFoundResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 404);
        }

        [TestMethod]
        public void DetailsBadIdTest()
        {
            int? id = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();


            var controller = new DefaultController(mockRepo.Object);

            var response = controller.Details(id) as HttpStatusCodeResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 400);
        }

        #endregion

        #region Create

        //[TestMethod]
        //public void GETCreateTest()
        //{
        //    var expectedFamilies = new List<Family>
        //    {
        //        new Family
        //        {
        //            Id = 12,
        //            CrossNum = "bananas"
        //        }
        //    };

        //    var expectedPurposes = new List<Purpose>
        //    {
        //        new Purpose
        //        {
        //            Id = 13,
        //            Name = "play them off, keyboard cat"
        //        }
        //    };

        //    var mockRepo = new Mock<IPlantBreedingRepo>();
        //    mockRepo.Setup(r => r.GetAllFamilies()).Returns(expectedFamilies);
        //    mockRepo.Setup(r => r.GetAllPurposes()).Returns(expectedPurposes);

        //    //Update this line to match test purposes.
        //    var response = new GenotypesController(mockRepo.Object).Create(expectedFamilies.First().Id) as ViewResult;

        //    Assert.IsNotNull(response);
        //    Assert.IsNotNull(response.ViewData["FamilyId"]);
        //    Assert.IsNotNull(response.ViewData["PurposeId"]);
        //}

        [TestMethod]
        public void POSTCreateHappyTest()
        {
            var genotype = new Genotype() { Id = 12, GivenName = "bananas" };
            var viewModel = new AccessionViewModel() { Id = 12, GivenName = "bananas" };
            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(m => m.GetGenotype(12)).Returns(genotype);
            
            var response = new DefaultController(mockRepo.Object).Create(viewModel) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Details", response.RouteValues["action"]);
        }

        [TestMethod]
        public void POSTCreateNullTest()
        {
            var mockRepo = new Mock<IPlantBreedingRepo>();
            var response = new DefaultController(mockRepo.Object).Create(null) as HttpStatusCodeResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(400, response.StatusCode);
        }

        [TestMethod]
        public void POSTCreateInvalidSessionTest()
        {
            var genotype = new Genotype
            {
                Id = 12,
            };
            var viewModel = new AccessionViewModel
            {
                Id = 12,
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(m => m.GetGenotype(12)).Returns(genotype);

            var controller = new DefaultController(mockRepo.Object);
            controller.ModelState.AddModelError("err", "errerr");

            var response = controller.Create(viewModel) as ViewResult;

            Assert.IsNotNull(response);
            Console.WriteLine(response.ViewData.ToArray());
            Assert.IsNotNull((response.ViewData.Model as AccessionViewModel).FamilyId);
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
            var expected = new Genotype
            {
                Id = 12,
                GivenName = "bananas"
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetGenotype(id.Value)).Returns(expected);

            var controller = new DefaultController(mockRepo.Object);

            var response = controller.Delete(id) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as Genotype);

            var actual = response.Model as Genotype;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GETDeleteNullTest()
        {
            int? id = 12;
            Genotype expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetGenotype(id.Value)).Returns(expected);

            var controller = new DefaultController(mockRepo.Object);

            var response = controller.Delete(id) as HttpNotFoundResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 404);
        }

        [TestMethod]
        public void GETDeleteBadIdTest()
        {
            int? id = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new DefaultController(mockRepo.Object);

            var response = controller.Delete(id) as HttpStatusCodeResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 400);
        }

        /// <summary>
        /// Tests that the controller redirects to the Index when finished deleting.
        /// </summary>
        [TestMethod]
        public void POSTDeleteHappyCase()
        {
            int id = 12;
            var Genotype = new Genotype
            {
                Id = 12,
                GivenName = "bananas"
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(m => m.GetGenotype(12)).Returns(Genotype);

            var controller = new DefaultController(mockRepo.Object);

            var response = controller.DeleteConfirmed(id) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Index", response.RouteValues["action"]);
        }
        [TestMethod]
        public void POSTUpdateIsPopulation()
        {
            var aivm = new AccessionIndexViewModel
            {
                Id = 12,
                FamilyId = 13,
                IsPopulation = false
            };
            var genotype = new Genotype
            {
                Id = 12,
                FamilyId = 13,
                IsPopulation = true
            };
            var family = new Family
            {
                Id = 13,
                Genotypes =
                {
                    genotype
                }
            };
            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(m => m.GetGenotype(12)).Returns(genotype);
            mockRepo.Setup(m => m.GetFamily(13)).Returns(family);
            var controller = new DefaultController(mockRepo.Object);
            var response = controller.UpdateIsPopulation(aivm) as JsonResult;
            dynamic json = response.Data;
            Assert.IsNotNull(response);
            Assert.IsFalse(json.Error);
            Assert.AreEqual(json.Message, String.Empty);

        }

        #endregion
        private static HttpContext FakeHttpContext(string userId)
        {
            var httpRequest = new HttpRequest("", "http://google/", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                        BindingFlags.NonPublic | BindingFlags.Instance,
                                        null, CallingConventions.Standard,
                                        new[] { typeof(HttpSessionStateContainer) },
                                        null)
                                .Invoke(new object[] { sessionContainer });

            IPrincipal principal = null;
            principal = new GenericPrincipal(new GenericIdentity(userId),
            new string[0]);
            Thread.CurrentPrincipal = principal;
            httpContext.User = principal;

            return httpContext;
        }

        
    }
}
