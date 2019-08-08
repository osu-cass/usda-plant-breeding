using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Util
{
    /// <summary>
    /// Represents a very basic Binary Search Tree Node.
    /// </summary>
    public class TreeNode<T> where T : class
    {
        /// <summary>
        /// The data corresponding to a particular node.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// The left child of the node.
        /// </summary>
        public TreeNode<T> Left { get; set; }

        /// <summary>
        /// The right child of the node.
        /// </summary>
        public TreeNode<T> Right { get; set; }

        /// <summary>
        /// Instantiates a new instances of a TreeNode.
        /// </summary>
        public TreeNode() { }
    }
}
