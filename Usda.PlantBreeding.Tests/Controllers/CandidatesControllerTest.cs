using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Usda.PlantBreeding.Site.Controllers;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Data.Util;

namespace Usda.PlantBreeding.Site.Tests.Controllers
{
    [TestClass]
    public class CandidatessControllerTest
    {
        #region Index

        [TestMethod]
        public void IndexHappyTest()
        {
            IEnumerable<Candidate> expected = new List<Candidate>
                {
                    new Candidate
                    {
                        Id = 1,
                        GenotypeId = 1
                    },
                    new Candidate
                    {
                        Id = 2,
                        GenotypeId = 2
                    }
                };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCandidates()).Returns(expected);

            var controller = new CandidatesController(mockRepo.Object);
            var response = controller.Index() as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.AreEqual("List", response.RouteValues["action"]);
        }

        [TestMethod]
        public void IndexEmptyTest()
        {
            IEnumerable<Candidate> expected = new List<Candidate>();

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCandidates()).Returns(expected);

            var controller = new CandidatesController(mockRepo.Object);
            var response = controller.Index() as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("List", response.RouteValues["action"]);
        }

        [TestMethod]
        public void IndexNullTest()
        {
            IEnumerable<Candidate> expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCandidates()).Returns(expected);

            var controller = new CandidatesController(mockRepo.Object);

            var response = controller.Index() as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("List", response.RouteValues["action"]);
        }

        #endregion

        #region Details
        [TestMethod]
        public void DetailsHappyTest()
        {
            int? id = 12;
            var expected = new Candidate
            {
                Id = 1,
                GenotypeId = 1
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCandidate(id.Value)).Returns(expected);

            var controller = new CandidatesController(mockRepo.Object);

            var response = controller.Details(id) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as Candidate);

            var actual = response.Model as Candidate;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DetailsNullTest()
        {
            int? id = 12;
            Candidate expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCandidate(id.Value)).Returns(expected);

            var controller = new CandidatesController(mockRepo.Object);

            var response = controller.Details(id) as HttpNotFoundResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 404);
        }

        [TestMethod]
        public void DetailsBadIdTest()
        {
            int? id = null;

            var controller = new CandidatesController();

            var response = controller.Details(id) as HttpStatusCodeResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 400);
        }

        #endregion

        #region Delete

        [TestMethod]
        public void GETDeleteHappyTest()
        {
            int? id = 1;
            var expected = new Candidate
            {
                Id = 1,
                GenotypeId = 1
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCandidate(id.Value)).Returns(expected);

            var controller = new CandidatesController(mockRepo.Object);

            var response = controller.Delete(id) as ViewResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Model);
            Assert.IsNotNull(response.Model as Candidate);

            var actual = response.Model as Candidate;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GETDeleteNullTest()
        {
            int? id = 12;
            Candidate expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCandidate(id.Value)).Returns(expected);

            var controller = new CandidatesController(mockRepo.Object);

            var response = controller.Delete(id) as HttpNotFoundResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 404);
        }

        [TestMethod]
        public void GETDeleteBadIdTest()
        {
            int? id = null;

            var controller = new CandidatesController();

            var response = controller.Delete(id) as HttpStatusCodeResult;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == 400);
        }

        [TestMethod]
        public void POSTDeleteHappyCase()
        {
            int id = 1;
            var crossType = new Candidate
            {
                Id = 1,
                GenotypeId = 1
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();

            var controller = new CandidatesController(mockRepo.Object);

            var response = controller.DeleteConfirmed(id) as RedirectToRouteResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.RouteValues);
            Assert.AreEqual("Index", response.RouteValues["action"]);
        }

        #endregion

        #region Candidates For Cross

        [TestMethod]
        public void GETCandidatesForCrossHappyTest()
        {
            var expected = new List<Candidate>
            {
                new Candidate
                {
                    Year = DateTime.Now.Year
                },
                new Candidate
                {
                    Year = DateTime.Now.Year
                }
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCandidates()).Returns(expected);

            var result = new CandidatesController(mockRepo.Object).CandidatesForCross() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsNotNull(result.Model as IEnumerable<Candidate>);

            var actual = result.Model as IEnumerable<Candidate>;

            Assert.AreEqual(expected.Count(), actual.Count());
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod]
        public void GETCandidatesForCrossNullTest()
        {
            List<Candidate> expected = null;

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCandidates()).Returns(expected);

            var result = new CandidatesController(mockRepo.Object).CandidatesForCross() as HttpNotFoundResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GETCandidatesForCrossEmptyTest()
        {
            var expected = new List<Candidate>();

            var mockRepo = new Mock<IPlantBreedingRepo>();
            mockRepo.Setup(r => r.GetCandidates()).Returns(expected);

            var result = new CandidatesController(mockRepo.Object).CandidatesForCross() as HttpNotFoundResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void POSTCandidatesForCrossTest()
        {
            int candidate1 = 12, candidate2 = 13;

            var result = new CandidatesController().CandidatesForCross(candidate1, candidate2) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.RouteValues);
            Assert.IsNotNull(result.RouteValues["action"]);
            Assert.IsNotNull(result.RouteValues["controller"]);
            Assert.IsNotNull(result.RouteValues["action"].ToString() == "CreateFromCandidate");
            Assert.IsNotNull(result.RouteValues["controller"].ToString() == "Families");
        }

        #endregion
    }
}
