using System;
using System.Collections.Generic;
using System.Linq; // Added for array output formatting

// --- PLACEHOLDER CLASSES ---
// These are required for the code to compile and run.

/// <summary>
/// Represents a single node in the Binary Search Tree.
/// </summary>
public class Node
{
    public int Value { get; set; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }

    public Node(int value)
    {
        Value = value;
    }
}

/// <summary>
/// Placeholder class for the Binary Search Tree.
/// Only contains the necessary Insert method.
/// </summary>
public class BinarySearchTree
{
    public Node? Root { get; private set; }

    /// <summary>
    /// Inserts a value into the BST (standard BST insertion logic).
    /// </summary>
    public void Insert(int value)
    {
        Root = InsertRecursive(Root, value);
    }

    private Node InsertRecursive(Node? node, int value)
    {
        if (node == null)
        {
            return new Node(value);
        }

        if (value < node.Value)
        {
            node.Left = InsertRecursive(node.Left, value);
        }
        else if (value > node.Value)
        {
            node.Right = InsertRecursive(node.Right, value);
        }

        return node;
    }

    /// <summary>
    /// Helper method to visually check the tree structure using Pre-order traversal.
    /// The output order is the order in which the nodes are visited (Root, Left, Right).
    /// </summary>
    public string GetStructure(Node? node = null)
    {
        node ??= Root;
        if (node == null)
        {
            return "[]";
        }
        var list = new List<int>();
        PreOrderTraversal(node, list);
        return "[" + string.Join(", ", list) + "]";
    }

    private void PreOrderTraversal(Node? node, List<int> list)
    {
        if (node == null) return;
        list.Add(node.Value);
        PreOrderTraversal(node.Left, list);
        PreOrderTraversal(node.Right, list);
    }
}


// --- MAIN RECURSION CLASS ---

public static class Trees
{
    /// <summary>
    /// Given a sorted list (sorted_list), create a balanced BST.
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers)
    {
        var bst = new BinarySearchTree(); // Create an empty BST to start with 
        // Start the recursive process using the full range of the array
        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);
        return bst;
    }

    /// <summary>
    /// This function will attempt to insert the item in the middle of the specified range
    /// of 'sortedNumbers' into the 'bst' tree.
    /// </summary>
    /// <param name="sortedNumbers">input numbers that are already sorted</param>
    /// <param name="first">the first index in the sortedNumbers to consider</param>
    /// <param name="last">the last index in the sortedNumbers to consider</param>
    /// <param name="bst">the BinarySearchTree in which to insert the values</param>
    private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst)
    {
        // Base Case: If the 'first' index crosses the 'last' index, the current range is invalid/empty, so we stop.
        if (first > last)
        {
            return;
        }

        // 1. Calculate the middle index.
        // Using `first + (last - first) / 2` prevents potential integer overflow compared to `(first + last) / 2`.
        int mid = first + (last - first) / 2;

        // 2. Insert the middle element into the BST. This element becomes the root of the current subtree.
        bst.Insert(sortedNumbers[mid]);

        // 3. Recursive Call for the Left Sub-array (elements smaller than the middle)
        // The new range is from 'first' up to (but not including) 'mid'.
        InsertMiddle(sortedNumbers, first, mid - 1, bst);

        // 4. Recursive Call for the Right Sub-array (elements larger than the middle)
        // The new range is from (after) 'mid' up to 'last'.
        InsertMiddle(sortedNumbers, mid + 1, last, bst);
    }
}

// --- PROGRAM ENTRY POINT FOR TESTING ---
// This class is added to make the file runnable as a complete C# application.
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- Balanced BST Creation Test ---");

        // Example 1: Even number of elements
        int[] numbersEven = { 10, 20, 30, 40, 50, 60 };
        Console.WriteLine($"Input Array (Sorted): {string.Join(", ", numbersEven)}");

        BinarySearchTree bstEven = Trees.CreateTreeFromSortedList(numbersEven);
        Console.WriteLine("Resulting BST Structure (Pre-order Traversal):");
        // Expected: [30 (root), 10 (30.left), 20 (10.right), 50 (30.right), 40 (50.left), 60 (50.right)]
        Console.WriteLine(bstEven.GetStructure());

        Console.WriteLine("\n----------------------------------");

        // Example 2: Odd number of elements
        int[] numbersOdd = { 1, 2, 3, 4, 5, 6, 7 };
        Console.WriteLine($"Input Array (Sorted): {string.Join(", ", numbersOdd)}");

        BinarySearchTree bstOdd = Trees.CreateTreeFromSortedList(numbersOdd);
        Console.WriteLine("Resulting BST Structure (Pre-order Traversal):");
        // Expected: [4 (root), 2 (4.left), 1 (2.left), 3 (2.right), 6 (4.right), 5 (6.left), 7 (6.right)]
        Console.WriteLine(bstOdd.GetStructure());
    }
}