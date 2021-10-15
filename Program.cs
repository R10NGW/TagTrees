using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace TagTree
{
    class Program  //application that receives an input of relations and creates a tree data structure from it. It then prints it to a file, and writes the output in the terminal
    {
        public static string treeString;   //class level string, used to print tree 

        public static void printer(string finalString)   //prints to file and writes output in terminal 
        {
            string outFile = @"./output.txt";
            File.WriteAllText(outFile, finalString);
            Console.Write(finalString);
        }

        public static string findRoot(string inputString)   //finds root of input file
        {
            string[] delimiters = new string[] { "\r\n", " ", "->", "," };
            string[] rootTagArray = inputString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);  //deliminates string into an array of 4 character strings
            Dictionary<string, int> rootTagResult = new Dictionary<string, int>();
            foreach (string tag in rootTagArray)   //foreach tag in array add tag to dict if it already exists add 1 to value. Our root will be the only key with 1 occurance 
            {
                if (rootTagResult.ContainsKey(tag))
                    rootTagResult[tag] = rootTagResult[tag] + 1;
                else
                    rootTagResult[tag] = 1;
            }
            foreach (var tagArray in rootTagResult)  //returns root
            {
                if (tagArray.Value == 1)
                    return tagArray.Key;
            }
            return "no bottom tag was found";  //if no root found return error 
        }

        public static Dictionary<String, List<string>> findTagDict(string inputString)   //find dict of parent nodes with their children nodes, filters out nodes with no children
        {
            string[] tags = inputString.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);   //deliminates string into an array of strings on carriage return
            Dictionary<String, List<string>> TagDict = new Dictionary<string, List<string>>();
            string[] delimiters = new string[] { " ", "," };
            foreach (string tag in tags)   //creates a dict of all strings in the array that are longer then 4 characers long
            {
                if (tag.Length > 4)
                {
                    string tempKey = tag.Substring(0, 4);
                    string[] tempArray = tag.Substring(8).Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    TagDict.Add(tempKey, tempArray.ToList());
                }
            }
            return TagDict;
        }

        public static TreeNode<T> treeBuilder<T>(Dictionary<T, List<T>> inputDict, T root)   //uses root and dict to create tree structure 
        {
            TreeNode<T> tree = new TreeNode<T>  //creates first node
            {
                Data = root,
            };

            tree.Children = getChildNodes(inputDict, tree);   //looks for children of our first node


            
            return tree;
        }

        public static List<TreeNode<T>> getChildNodes<T>(Dictionary<T, List<T>> inputDict, TreeNode<T> parent)  //gets children of parent node and then looks for children of the child nodes
        {
            List<TreeNode<T>> nodes = new List<TreeNode<T>>();

            if (inputDict.ContainsKey(parent.Data))  //if parent node is a key in our dict 
            {
                foreach (var item in inputDict[parent.Data])  //creats all child nodes related to parent node
                {
                    TreeNode<T> newNode = new TreeNode<T>
                    {
                        Data = item,
                        Parent = parent,
                    };

                    if (!ContainedInParent(parent, item))  //Checks to see if the child is in listed as a parent, if so create children for it
                    {
                        newNode.Children = getChildNodes(inputDict, newNode);
                    }

                    if (newNode.Children.Count == 0)  //converts node to type string and adds it to the treeString variable
                    {
                        treeString += (NodeString(newNode) + "\n"); 
                    }

                }
            }
            return nodes;
        }

        private static bool ContainedInParent<T>(TreeNode<T> parent, T item)    //Checks to see if the child is in listed as a parent
        {
            var found = false;

            if (parent != null)
            {
                if (parent.Data.Equals(item))
                {
                    found = true;
                }
                else
                {
                    found = ContainedInParent(parent.Parent, item);
                }
            }
            return found;
        }

        private static string NodeString<T>(TreeNode<T> node)  //converts node to type string and adds arrow to children
        {
            return (node.Parent != null ? NodeString(node.Parent) + "->" : string.Empty) + node.Data;
        }

        public static void Main(string[] args)  //main string in class
        {
            string inFile = System.IO.File.ReadAllText(@"./input.txt");   //gets input
            string root = findRoot(inFile);  //finds root
            Dictionary<String, List<string>> TagDict = findTagDict(inFile);  //finds dict of parents and children
            treeBuilder(TagDict, root);  //uses root and dict to create tree structure
            printer(treeString);  //sends a string version of the tree structure to an output file and prints to the terminal for testing
        }

    }
}