﻿using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace TagTree
{
    class Program
    {
        public static void Main(string[] args)
        {
           string inFile = System.IO.File.ReadAllText(@"C:\Users\Ryan\Desktop\C#\input.txt");
           string sorted = sorter(inFile);
           printer(sorted);
        }

        public static void printer(string finalString)
        {
            string outFile = @"C:\Users\Ryan\Desktop\C#\output.txt";
            File.WriteAllText(outFile, finalString);
            Console.Write(finalString);
        }

        public static string sorter(string inputString)
        {
           string root = findRoot(inputString);
           Dictionary<String, List<string>> TagDict = findTagDict(inputString);
           return root;
        }

        public static string findRoot(string inputString)
        {
            string[] delimiters = new string[] {"\r\n", " ","->", ","};
            string[] rootTagArray = inputString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            var rootTagResult = new Dictionary<string,int>();
            foreach(string tag in rootTagArray)
                {
                    if(rootTagResult.ContainsKey(tag))
                        rootTagResult[tag]=rootTagResult[tag]+1;
                    else
                        rootTagResult[tag]=1;
                }
            foreach(var tagArray in rootTagResult)
            {
                if(tagArray.Value == 1)
                return tagArray.Key;
            }
            return "no bottom tag was found";
        }

        public static Dictionary<String, List<string>> findTagDict(string inputString)
        {
            string[] tags = inputString.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            Dictionary<String, List<string>> TagDict = new Dictionary<string, List<string>>();
            string[] delimiters = new string[] {" ", ","};
            foreach(string tag in tags)
            {
                if(tag.Length > 4)
                {
                    string tempKey = tag.Substring(0,4);
                    string[] tempArray = tag.Substring(8).Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    TagDict.Add(tempKey,tempArray.ToList());
                }
            }
            return TagDict;
        }

        public static string Tree(string inputString)
        {
           return " ";
        }
        

            // foreach(KeyValuePair<string,List<string>> tag in TagDict)
            // {
            //     string tempString = "";
            //     foreach(string value in tag.Value)
            //     {
            //         tempString += (" " + value + " ");
            //     }
            //     Console.WriteLine(tag.Key + " test " + tempString );
            // }
    }
}


