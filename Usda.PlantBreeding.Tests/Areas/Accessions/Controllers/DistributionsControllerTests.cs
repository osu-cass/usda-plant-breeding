using Microsoft.VisualStudio.TestTools.UnitTesting;
using Usda.PlantBreeding.Site.Areas.Accessions.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;
using Moq;
using Usda.PlantBreeding.Data.DataAccess;
using System.Web;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Site.App_Start;
using System.Linq.Expressions;
using System.IO;
using System.Web.SessionState;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Infrastructure;

namespace Usda.PlantBreeding.Site.Areas.Accessions.Controllers.Tests
{
    [TestClass()]
    public class DistributionsControllerTests
    {
        Mock<IPlantBreedingRepo> mockRepo = new Mock<IPlantBreedingRepo>();
        OrdersUOW ordersUOW;
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
                Id= 1,
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
            var genotypes = new List<Genotype>
            {
                new Genotype
                {
                    Id=1,
                    OriginalName="OrigName",
                    GivenName="GiveName",
                    FamilyId=1,
                    Family=families.First(),
                    CrossPlanId=1
                }
            };
            var locations = new List<Location>
            {
                new Location
                {
                    Id=1,
                    Address="SomeAddress",

                }
            };
            var growers = new List<Grower>
            {
                new Grower
                {
                    Id=1,
                    FirstName="FN",
                    LastName="LN"
                }
            };
            var orders = new List<Order>
            {
                new Order
                {
                    Id=1,
                    Name="A",
                    GenusId=3,
                    GrowerId=1,
                    LocationId=1,
                    Genus=genus,
                    Location=locations[0],
                    Grower=growers[0]
                }
            };
            var orderProducts = new List<OrderProduct>
            {
                new OrderProduct
                {
                    OrderId=1,
                    GenotypeId=1,
                    Genotype=genotypes[0],
                    Order=orders[0]
                }
            };
            mockRepo.Setup(r => r.GetGenus(3)).Returns(genus);
            mockRepo.Setup(r => r.GetCrossPlan(3)).Returns(crossPlan);
            mockRepo.Object.SaveUserPreference(userPref);
            mockRepo.Setup(r => r.GetUserPreference("fakeUser")).Returns(userPref);
            mockRepo.Setup(r => r.GetDefaultOrigin()).Returns(defaultOrigin);
            mockRepo.Setup(r => r.GetOrigin(108)).Returns(defaultOrigin);
            mockRepo.Setup(r => r.GetQueryableFamilies(It.IsAny<Expression<Func<Family, bool>>>())).Returns(families);
            mockRepo.Setup(r => r.GetQueryableFamilies(It.IsAny<string>(), It.IsAny<int>())).Returns(families);
            mockRepo.Setup(r => r.GetOrders()).Returns(orders);
            mockRepo.Setup(r => r.GetOrder(It.IsAny<Expression<Func<Order, bool>>>())).Returns(orders[0]);
            mockRepo.Setup(r => r.GetLocation(1)).Returns(locations[0]);
            mockRepo.Setup(r => r.GetLocations()).Returns(locations);
            mockRepo.Setup(r => r.GetGrower(1)).Returns(growers[0]);
            mockRepo.Setup(r => r.GetGrowers()).Returns(growers);
            mockRepo.Setup(r => r.GetGenotype(1)).Returns(genotypes[0]);
            mockRepo.Setup(r => r.GetGenotypes()).Returns(genotypes);
            mockRepo.Setup(r => r.GetOrderProduct(It.IsAny<Expression<Func<OrderProduct, bool>>>())).Returns(orderProducts[0]);
            mockRepo.Setup(r => r.GetOrderProducts()).Returns(orderProducts);
            HttpContext.Current = FakeHttpContext("fakeUser");
            ordersUOW = new OrdersUOW(mockRepo.Object);
        }
        [TestMethod()]
        public void IndexTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveOrderTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditNullTest()
        {
            DistributionsController controller = new DistributionsController();
            var response = controller.Edit(null) as HttpStatusCodeResult;
            Assert.AreEqual(response.StatusCode, 400);
        }
        [TestMethod()]
        public void EditValidTest()
        {
            DistributionsController controller = new DistributionsController(ordersUOW);
            var response = controller.Edit(1) as ViewResult;
            Assert.IsNotNull(response.Model);
            var model = response.Model as OrdersEditViewModel;
            Assert.AreEqual(model.Order.Id, 1);
        }

        [TestMethod()]
        public void GetOrderNullTest()
        {
            DistributionsController controller = new DistributionsController();
            var response = controller.GetOrder(null) as HttpStatusCodeResult;
            Assert.AreEqual(response.StatusCode, 400);
        }
        [TestMethod()]
        public void GetOrderValidTest()
        {
            DistributionsController controller = new DistributionsController(ordersUOW);
            var response = controller.GetOrder(1) as Usda.PlantBreeding.Site.CustomAttributes.JsonNetResult;
            dynamic json = response.Data;
            Assert.AreEqual(json.Id, 1);
        }

        [TestMethod()]
        public void GetOrderProductsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveOrderProductTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteOrderProductTest()
        {
            Assert.Fail();
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