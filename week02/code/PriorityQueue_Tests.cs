using System;
using NUnit.Framework;
using Week02Code; // adjust to your namespace

namespace Week02Tests
{
    [TestFixture]
    public class PriorityQueueTests
    {
        /* TEST SUMMARY
           These tests verify:
           - Enqueue/Dequeue return highest priority.
           - Among equal priorities, FIFO is preserved.
           - Exception thrown when dequeuing empty queue with exact message.
         */

        [Test]
        public void EnqueueThenDequeue_ReturnsHighestPriority()
        {
            var pq = new PriorityQueue<string>();
            pq.Enqueue("low", 1);
            pq.Enqueue("medium", 5);
            pq.Enqueue("high", 10);

            string result = pq.Dequeue();
            Assert.AreEqual("high", result);
        }

        [Test]
        public void Dequeue_WithTieOnPriority_RemovesEarliestEnqueuedAmongTies()
        {
            var pq = new PriorityQueue<string>();
            pq.Enqueue("a", 3); // earliest with priority 3
            pq.Enqueue("b", 5); // priority 5
            pq.Enqueue("c", 5); // same priority 5, enqueued later
            pq.Enqueue("d", 2);

            string first = pq.Dequeue(); // should be "b" (first with priority 5)
            Assert.AreEqual("b", first);

            string second = pq.Dequeue(); // should be "c" (next with priority 5)
            Assert.AreEqual("c", second);
        }

        [Test]
        public void Dequeue_EmptyQueue_ThrowsInvalidOperationExceptionWithMessage()
        {
            var pq = new PriorityQueue<int>();
            var ex = Assert.Throws<InvalidOperationException>(() => pq.Dequeue());
            Assert.AreEqual("The queue is empty.", ex.Message);
        }

        [Test]
        public void Enqueue_MaintainsOrderForDifferentPriorities()
        {
            var pq = new PriorityQueue<string>();
            pq.Enqueue("first", 1);
            pq.Enqueue("second", 2);
            pq.Enqueue("third", 1);

            // highest priority is 2 -> "second"
            Assert.AreEqual("second", pq.Dequeue());

            // next highest priority is 1; among "first" and "third", "first" is earlier
            Assert.AreEqual("first", pq.Dequeue());
            Assert.AreEqual("third", pq.Dequeue());
        }
    }
}
