using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }

        public Node<T> Previous { get; set; }

        public Node(T value, Node<T> next, Node<T> previous)
        {
            Value = value;
            Next = next;
            Previous = previous;
        }
    }
    public class LinkedList<T>
    {
        public Node<T> Head { get; set; }
        public int Count { get; set; }

        public Node<T> Tail { get; set; }

        public void AddToTail(T value)
        {
            Node<T> node = new Node<T>(value, null, Tail);
            if (Count == 0)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                Tail.Next = node;
                Tail = node;
            }
            Count++;
        }

        public void AddToHead(T value)
        {
            Node<T> node = new Node<T>(value, Head.Next, null);
            if (Count == 0)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                Head.Previous = node;
                Head = node;
            }
            Count++;
        }

        public void RemoveHead()
        {
            Head.Next.Previous = null;
            Head = Head.Next;
        }

        public void RemoveTail()
        {
            Tail.Previous.Next = null;
            Tail = Tail.Previous;
        }
    }
}
