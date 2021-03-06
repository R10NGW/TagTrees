using System.Collections.Generic;

namespace TagTree
{
    public class TreeNode<T>  // node object
    {
        public T Data { get; set; }
        public TreeNode<T> Parent { get; set; }
        public List<TreeNode<T>> Children { get; set; }

    }
}