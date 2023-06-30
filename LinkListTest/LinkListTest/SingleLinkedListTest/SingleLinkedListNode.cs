using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListTest.SingleLinkedListTest
{
    internal class SingleLinkedListNode<T>
    {
        public T Value { get; set; }
        public SingleLinkedListNode<T> Next { get; set; }
        public SingleLinkedListNode()
        {
            Value = default;
            Next = null;
        }
    }
}
