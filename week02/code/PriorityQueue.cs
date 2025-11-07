using System;
using System.Collections.Generic;
using System.Linq;

namespace Week02Code
{
    public class PriorityQueue<T>
    {
        private class Node
        {
            public T Value { get; }
            public int Priority { get; }
            public long Sequence { get; }

            public Node(T value, int priority, long sequence)
            {
                Value = value;
                Priority = priority;
                Sequence = sequence;
            }
        }

        private readonly List<Node> _list = new List<Node>();
        private long _sequenceCounter = 0;

        // Enqueue: add to back (we keep insertion order by sequence)
        public void Enqueue(T value, int priority)
        {
            // Assign a sequence to maintain FIFO for equal priority
            var node = new Node(value, priority, _sequenceCounter++);
            _list.Add(node);
        }

        // Dequeue: remove item with highest priority; if tie -> earliest sequence
        public T Dequeue()
        {
            if (_list.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            // find max priority value
            int maxPriority = _list.Max(n => n.Priority);

            // among nodes with max priority choose smallest sequence (earliest)
            Node chosen = null;
            long bestSequence = long.MaxValue;
            int chosenIndex = -1;
            for (int i = 0; i < _list.Count; i++)
            {
                var node = _list[i];
                if (node.Priority == maxPriority && node.Sequence < bestSequence)
                {
                    chosen = node;
                    bestSequence = node.Sequence;
                    chosenIndex = i;
                }
            }

            // Remove the chosen node from list and return its value
            if (chosenIndex >= 0)
            {
                T value = _list[chosenIndex].Value;
                _list.RemoveAt(chosenIndex);
                return value;
            }

            // Fallback (shouldn't reach here)
            throw new InvalidOperationException("No item found to dequeue.");
        }

        public int Count => _list.Count;

        public bool IsEmpty => _list.Count == 0;
    }
}
