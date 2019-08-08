using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Data.Util;

namespace Usda.PlantBreeding.Site.Tests
{
    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void FlattenPreOrderTest()
        {
            var root = new Genotype
            {
                Id = 1,
                Family = new Family
                {
                    MaleParent = 2,
                    FemaleParent = 3
                }
            };
            var maleParent = new Genotype
            {
                Id = 2
            };
            var femaleParent = new Genotype
            {
                Id = 3
            };

            var node = new TreeNode<Genotype>
            {
                Data = root,
                Left = new TreeNode<Genotype> { Data = maleParent },
                Right = new TreeNode<Genotype> { Data = femaleParent }
            };

            var expected = new List<Genotype> { root, maleParent, femaleParent };

            List<Genotype> accumulator = new List<Genotype>();

            node.FlattenPreOrder(ref accumulator);
            Assert.AreEqual(expected.Count, accumulator.Count);
            
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, accumulator[i].Id);
            }
        }

        [TestMethod]
        public void BuildPedigreeBasicTest()
        {
            var root = new Genotype
            {
                Id = 1,
                Family = new Family
                {
                    MaleParent = 2,
                    FemaleParent = 3
                }
            };
            var maleParent = new Genotype
            {
                Id = 2
            };
            var femaleParent = new Genotype
            {
                Id = 3
            };

            var expected = new TreeNode<Genotype>
            {
                Data = root,
                Left = new TreeNode<Genotype> { Data = maleParent },
                Right = new TreeNode<Genotype> { Data = femaleParent }
            };

            var mockRepo = new Mock<IPlantBreedingRepo>();

            mockRepo.Setup(r => r.GetGenotype(1)).Returns(root);
            mockRepo.Setup(r => r.GetGenotype(2)).Returns(maleParent);
            mockRepo.Setup(r => r.GetGenotype(3)).Returns(femaleParent);
            mockRepo.Setup(r => r.GetGenotypeParents(root)).Returns(Tuple.Create(maleParent, femaleParent));

            var accumulator = new List<Genotype>();

            expected.FlattenPreOrder(ref accumulator);

            var expectedList = accumulator;            

            accumulator.Clear();            

            root.BuildPedigreeTree(mockRepo.Object).FlattenPreOrder(ref accumulator);

            var actualList = accumulator;

            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.AreEqual(expectedList[i].Id, actualList[i].Id);
            }
        }
    }
}
