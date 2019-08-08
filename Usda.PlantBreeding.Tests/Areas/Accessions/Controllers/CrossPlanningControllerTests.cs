using Microsoft.VisualStudio.TestTools.UnitTesting;
using Usda.PlantBreeding.Site.Areas.Accessions.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Site.App_Start;
using Usda.PlantBreeding.Data.Models;
using Moq;
using Usda.PlantBreeding.Data.DataAccess;
using System.Web.Mvc;
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
using System.Linq.Expressions;

namespace Usda.PlantBreeding.Site.Areas.Accessions.Controllers.Tests
{
    [TestClass()]
    public class CrossPlanningControllerTests
    {
        Mock<IPlantBreedingRepo> mockRepo = new Mock<IPlantBreedingRepo>();
        [TestInitialize]
        public void InitializeTests()
        {
            AutoMapperConfig.Configure();
            var genus = new Genus
            {
                Id = 3,
                Name = "R",
                Value = "Rubus",
                DefaultPlantsInRep = 3,
                VirusLabel1 = "RBDV",
                VirusLabel2 = "ToRSV",
                VirusLabel3 = "SNSV",
                Retired = false,
                ExternalId = null,
                VirusLabel4 = "BCRV"
            };
            var cpivm = new CrossPlanIndexViewModel
            {
                GenusId = 3,
                DefaultOriginName = "testDefaultName",
                NextCrossNumber = "nextCrossNum",
                CurrentYear = "2017",
            };
            var crossPlan = new CrossPlan
            {
                GenusId = 3,
                Year = "2017",
            };
            var userPref = new UserPreference
            {
                UserId = "fakeUser",
                GenusId = 3
            };
            var defaultOrigin = new Origin
            {
                Id = 108,
                Name = "ORUS",
                Retired = false,
                IsDefault = true
            };
            var families = new List<Family>
            {
                new Family
                {
                    Id=1,
                    CrossNum="4989",
                    FieldPlantedNum=144,
                    TransplantedNum=null,
                    GenusId=3,
                    IsReciprocal=null,
                    OriginId=108,
                    SeedNum=749,
                    CrossTypeId=17,
                    Purpose="Exe SS Y",
                    FemaleParent=17290,
                    MaleParent=10005,
                    ReciprocalString="N",
                }
            }.AsQueryable();
            mockRepo.Setup(r => r.GetGenus(3)).Returns(genus);
            mockRepo.Setup(r => r.GetCrossPlan(3)).Returns(crossPlan);
            mockRepo.Object.SaveUserPreference(userPref);
            mockRepo.Setup(r => r.GetUserPreference("fakeUser")).Returns(userPref);
            mockRepo.Setup(r => r.GetDefaultOrigin()).Returns(defaultOrigin);
            mockRepo.Setup(r => r.GetOrigin(108)).Returns(defaultOrigin);
            mockRepo.Setup(r => r.GetQueryableFamilies(It.IsAny< Expression<Func<Family, bool>>>())).Returns(families);
            mockRepo.Setup(r => r.GetQueryableFamilies(It.IsAny<string>(), It.IsAny<int>())).Returns(families);

            HttpContext.Current = FakeHttpContext("fakeUser");
        }
        [TestMethod()]
        public void CrossPlanningControllerTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CrossPlanningControllerTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void IndexByValidYearTest()
        {
            var Year = "2017";
            var controller = new CrossPlanningController(mockRepo.Object);
            var response = controller.Index(Year, 3) as ViewResult;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as CrossPlanIndexViewModel);

            var actual = response.Model as CrossPlanIndexViewModel;
            Assert.IsNotNull(actual.CurrentYear = "2017");
        }

        [TestMethod()]
        public void IndexTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void AddRowTest()
        {
            var controller = new CrossPlanningController(mockRepo.Object);
            var response = controller.AddRow(DateTime.Now.Year.ToString(), 3);
            Assert.IsNotNull(response as PartialViewResult);
            var responseView = response as PartialViewResult;
            Assert.IsNotNull(responseView.Model as CrossPlanViewModel);
            var model = responseView.Model as CrossPlanViewModel;
            Assert.AreEqual(model.Year, DateTime.Now.Year.ToString());
            Assert.AreEqual(model.GenusId, 3);
            Assert.AreEqual(model.DateCreated.Day, DateTime.Now.Day);
        }

        [TestMethod()]
        public void OrderByDefaultTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void OrderByParentTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CopyPlanToYearTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RemovePlanTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void SavePlanTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetParentsTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void SearchAccessionsTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CreateFamilyTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GenerateReciprocalsTest()
        {
            var controller = new CrossPlanningController(mockRepo.Object);
            var response = controller.GenerateReciprocals(DateTime.Now.Year.ToString(), 3) as JsonResult;
            Assert.IsNotNull(response);
            dynamic json = response.Data;
            Assert.AreEqual("All reciprocals already created", json.Message);
        }
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