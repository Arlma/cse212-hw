using System;
using System.Collections.Generic;

public class Node
{
    public int Value;
    public Node? Left;
    public Node? Right;

    public Node(int value)
    {
        Value = value;
    }
}

public class BinarySearchTree
{
    public Node? Root;

    // -------------------------------------
    // PROBLEM 1: INSERT (NO DUPLICATES)
    // -------------------------------------
    public void Insert(int value)
    {
        Root = Insert(Root, value);
    }

    private Node Insert(Node? node, int value)
    {
        if (node == null)
            return new Node(value);

        if (value < node.Value)
            node.Left = Insert(node.Left, value);
        else if (value > node.Value)
            node.Right = Insert(node.Right, value);

        return node;
    }

    // -------------------------------------
    // PROBLEM 2: CONTAINS
    // -------------------------------------
    public bool Contains(int value)
    {
        return ContainsRecursive(Root, value);
    }

    private bool ContainsRecursive(Node? node, int value)
    {
        if (node == null)
            return false;

        if (node.Value == value)
            return true;

        return value < node.Value
            ? ContainsRecursive(node.Left, value)
            : ContainsRecursive(node.Right, value);
    }

    // -------------------------------------
    // PROBLEM 3: REVERSE TRAVERSAL
    // -------------------------------------
    public IEnumerable<int> Reverse()
    {
        return ReverseTraverse(Root);
    }

    private IEnumerable<int> ReverseTraverse(Node? node)
    {
        if (node == null)
            yield break;

        foreach (var v in ReverseTraverse(node.Right))
            yield return v;

        yield return node.Value;

        foreach (var v in ReverseTraverse(node.Left))
            yield return v;
    }

    // -------------------------------------
    // PROBLEM 4: TREE HEIGHT
    // -------------------------------------
    public int GetHeight()
    {
        return Height(Root);
    }

    private int Height(Node? node)
    {
        if (node == null)
            return 0;

        return 1 + Math.Max(Height(node.Left), Height(node.Right));
    }

    // -------------------------------------
    // ToString() — MUST MATCH TEST FORMAT
    // -------------------------------------
    public override string ToString()
    {
        List<int> values = new();
        InOrder(Root, values);
        return "<Bst>{" + string.Join(", ", values) + "}";
    }

    private void InOrder(Node? node, List<int> list)
    {
        if (node == null)
            return;

        InOrder(node.Left, list);
        list.Add(node.Value);
        InOrder(node.Right, list);
    }
}

public static class Trees
{
    // ---------------------------------------------------
    // PROBLEM 5: CREATE BALANCED TREE FROM SORTED LIST
    // ---------------------------------------------------
    public static BinarySearchTree CreateTreeFromSortedList(int[] sorted)
    {
        BinarySearchTree tree = new();

        if (sorted.Length == 0)
            return tree;

        tree.Root = BuildBalanced(sorted, 0, sorted.Length - 1);
        return tree;
    }

    private static Node? BuildBalanced(int[] sorted, int start, int end)
    {
        if (start > end)
            return null;

        int mid = (start + end) / 2;

        Node root = new(sorted[mid])
        {
            Left = BuildBalanced(sorted, start, mid - 1),
            Right = BuildBalanced(sorted, mid + 1, end)
        };

        return root;
    }
}
