// ...
public class LinkedList
{
    public Node? Head { get; set; }
    public Node? Tail { get; set; }
    public int Count { get; set; }

    public void InsertHead(int value)
    {
        var newNode = new Node(value);

        if (Head == null)
        {
            Head = Tail = newNode;
        }
        else
        {
            newNode.Next = Head;
            Head.Prev = newNode;
            Head = newNode;
        }
        Count++;
    }

    // -------------------------
    // INSERT TAIL
    // -------------------------
    public void InsertTail(int value)
    {
        var newNode = new Node(value);

        if (Tail == null)
        {
            Head = Tail = newNode;
        }
        else
        {
            Tail.Next = newNode;
            newNode.Prev = Tail;
            Tail = newNode;
        }
        Count++;
    }

    // -------------------------
    // INSERT AFTER
    // -------------------------
    public void InsertAfter(int afterValue, int newValue)
    {
        var current = Head;
        while (current != null)
        {
            if (current.Data == afterValue)
            {
                var newNode = new Node(newValue);
                newNode.Prev = current;
                newNode.Next = current.Next;
                if (current.Next != null)
                {
                    current.Next.Prev = newNode;
                }
                current.Next = newNode;
                if (Tail == current)
                {
                    Tail = newNode;
                }
                Count++;
                return;
            }
            current = current.Next;
        }
    }

    // -------------------------
    // REMOVE TAIL
    // -------------------------
    public bool RemoveTail()
    {
        if (Tail == null)
            return false;

        if (Head == Tail)
        {
            Head = Tail = null;
            Count = 0;
            return true;
        }

        Tail = Tail.Prev;
        Tail!.Next = null;
        Count--;
        return true;
    }

    // -------------------------
    // REMOVE VALUE
    // -------------------------
    public bool Remove(int value)
    {
        if (Head == null)
            return false;

        var current = Head;

        while (current != null)
        {
            if (current.Data == value)
            {
                // remove head
                if (current == Head)
                {
                    if (Head == Tail)
                        Head = Tail = null;
                    else
                    {
                        Head = Head.Next;
                        Head!.Prev = null;
                    }
                    Count--;
                    return true;
                }

                // remove tail
                if (current == Tail)
                    return RemoveTail();

                // remove middle
                current.Prev!.Next = current.Next;
                current.Next!.Prev = current.Prev;
                Count--;
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    // -------------------------
    // REPLACE
    // -------------------------
    public int Replace(int oldValue, int newValue)
    {
        int replaced = 0;
        var current = Head;

        while (current != null)
        {
            if (current.Data == oldValue)
            {
                current.Data = newValue;
                replaced++;
            }
            current = current.Next;
        }

        return replaced;
    }

    // -------------------------
    // REVERSE ITERATOR
    // -------------------------
    public IEnumerable<int> Reverse()
    {
        var current = Tail;
        while (current != null)
        {
            yield return current.Data;
            current = current.Prev;
        }
    }

    // helpers for tests
    public bool HeadAndTailAreNull() => Head == null && Tail == null;
    public bool HeadAndTailAreNotNull() => Head != null && Tail != null;

    public override string ToString()
    {
        var list = new List<int>();
        var cur = Head;

        while (cur != null)
        {
            list.Add(cur.Data);
            cur = cur.Next;
        }

        return $"<LinkedList>{{{string.Join(", ", list)}}}";
    }
}
