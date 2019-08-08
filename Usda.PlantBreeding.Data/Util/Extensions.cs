using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using BSGUtils;
namespace Usda.PlantBreeding.Data.Util
{
    public static class Extensions
    {
      

        /// <summary>
        /// Builds a pedigree tree for a Genotype.  A Pedigree tree is a BST-like structure of parents for that genotype.
        /// </summary>
        /// <param name="id">The ID of the Genotype to build a Pedigree from.</param>
        /// <returns>The root node of the Pedigree Tree, or null if the ID doesn't find a match.</returns>
        public static TreeNode<Genotype> BuildPedigreeTree(this Genotype target, IPlantBreedingRepo repo)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            Genotype root = repo.GetGenotype(target.Id);
            Tuple<Genotype, Genotype> parents = repo.GetGenotypeParents(target);

            var node = new TreeNode<Genotype> { Data = root };

            if (parents != null && parents.Item1 != null)
            {
                // Left side is always the male.
                node.Left = BuildPedigreeTree(parents.Item1, repo);
            }

            if (parents != null && parents.Item2 != null)
            {
                // Right side is always the female.
                node.Right = BuildPedigreeTree(parents.Item2, repo);
            }

            return node;
        }

        /// <summary>
        /// Traverses the target TreeNode via a Preorder traversal, accumulating the results in the given accumulator.
        /// </summary>
        public static void FlattenPreOrder<T>(this TreeNode<T> target, ref List<T> accumulator) where T : class
        {
            accumulator.Add(target.Data);

            if (target != null && target.Left != null)
            {
                FlattenPreOrder(target.Left, ref accumulator);
            }

            if (target != null && target.Right != null)
            {
                FlattenPreOrder(target.Right, ref accumulator);
            }
        }

        /// <summary>
        /// Removes - and . from a string
        /// </summary>
        public static string ParsePhoneNumber(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return str;
            return str.Replace("-", string.Empty).Replace(".", string.Empty);
        }

        /// <summary>
        /// Adds - to a string of length 10
        /// </summary>
        public static string FormatPhoneNumber(this string str)
        {
            if (str.IsNullOrWhiteSpace() || str.Length != 10) return str;
            return $"{str.Substring(0, 3)}-{str.Substring(3, 3)}-{str.Substring(6, 4)}";
        }


    }
}
