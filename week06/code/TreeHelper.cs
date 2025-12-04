using System.Collections.Generic;

public static class TreeHelper
{
    // Build a balanced BST from a sorted list using middle insertion
    public static void InsertMiddle(BinarySearchTree tree, List<int> sortedList, int first, int last)
    {
        if (first > last) return;

        int mid = first + (last - first) / 2;

        tree.Insert(sortedList[mid]);

        InsertMiddle(tree, sortedList, first, mid - 1);
        InsertMiddle(tree, sortedList, mid + 1, last);
    }

    // Public method to create entire tree
    public static BinarySearchTree CreateBalancedTree(List<int> sortedList)
    {
        BinarySearchTree tree = new BinarySearchTree();
        InsertMiddle(tree, sortedList, 0, sortedList.Count - 1);
        return tree;
    }
}
