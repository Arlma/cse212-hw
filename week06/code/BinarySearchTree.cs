using System.Collections.Generic;

public class BinarySearchTree
{
    public Node? Root { get; private set; }

    public void Insert(int data)
    {
        Root = InsertRec(Root, data);
    }

    private Node InsertRec(Node? root, int data)
    {
        if (root == null)
            return new Node(data);

        if (data < root.Data)
            root.Left = InsertRec(root.Left, data);
        else if (data > root.Data)
            root.Right = InsertRec(root.Right, data);

        return root;
    }

    // In-order traversal (helps in debugging or testing)
    public List<int> ToList()
    {
        List<int> result = new List<int>();
        InOrder(Root, result);
        return result;
    }

    private void InOrder(Node? root, List<int> result)
    {
        if (root == null) return;

        InOrder(root.Left, result);
        result.Add(root.Data);
        InOrder(root.Right, result);
    }
}
