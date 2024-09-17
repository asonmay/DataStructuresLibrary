using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary
{
    public class BSTNode<T>
    {
        public T Value;
        public BSTNode<T> Left;
        public BSTNode<T> Right;
        public BSTNode<T> Parent;

        public bool IsLeftChild => Parent == null ? false : Parent.Left == this;
        public bool IsRightChild => Parent == null ? false : Parent.Right == this;

        public int ChildCount
        {
            get
            {
                int count = 0;
                if(IsLeftChild)
                {
                    count++;
                }
                if (IsRightChild)
                {
                    count++;
                }
                return count;
            }
        }

        public BSTNode(T value, BSTNode<T> parent = null)
        {
            Value = value;
            Parent = parent;
        }
    }
    public class BST<T> where T : IComparable<T> 
    {
        private BSTNode<T> root;
        public int Count { get; private set; }
        public T RootValue => root.Value;

        public BST()
        {
            root = null;
            Count = 0;
        }

        public void Insert(T value)
        {
            Count++;
            if (root == null)
            {
                root = new BSTNode<T>(value);
                return;
            }

            BSTNode<T> current = root;
            while (current != null)
            {
                if (value.CompareTo(current.Value) < 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new BSTNode<T>(value, current);
                        break;
                    }
                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = new BSTNode<T>(value, current);
                        break;
                    }
                    current = current.Right;
                }
            }
        }

        private BSTNode<T> Find(T value)
        {
            BSTNode<T> current = root;
            while (current != null)
            {
                int comp = value.CompareTo(current.Value);

                if (comp < 0)
                {
                    current = current.Left;
                }
                else if (comp > 0)
                {
                    current = current.Right;
                }
                else if (comp == 0)
                {
                    return current;
                }
            }

            return null;
        }

        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        public bool Delete(T value)
        {
            BSTNode<T> toDelete = Find(value);
            if (toDelete == null)
            {
                return false;
            }

            Delete(toDelete);
            Count--;
            return true;
        }

        private void Delete(BSTNode<T> node)
        {
            if (node.ChildCount == 2)
            {
                BSTNode<T> candidate = Minimum(node.Right);
                node.Value = candidate.Value;
                node = candidate;
            }

            if (node.ChildCount == 0)
            {
                if (node == root)
                {
                    root = null;
                }
                else if (node.IsLeftChild)
                {
                    node.Parent.Left = null;
                }
                else if (node.IsRightChild)
                {
                    node.Parent.Right = null;
                }
            }
            else if (node.ChildCount == 1) 
            {
                BSTNode<T> child = node.Left == null ? node.Right : node.Left;

                if (node == root)
                {
                    root = child;
                }
                else if (node.IsLeftChild)
                {
                    node.Parent.Left = child;
                }
                else if (node.IsRightChild)
                {
                    node.Parent.Right = child;
                }
                child.Parent = node.Parent;
            }
        }

        private BSTNode<T> Minimum(BSTNode<T> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        private BSTNode<T> Maximum(BSTNode<T> node)
        {
            while (node.Right != null)
            {
                node = node.Right;
            }

            return node;
        }

        public T[] PreOrder()
        {
            List<T> nodes = new List<T>();

            Stack<BSTNode<T>> stack = new Stack<BSTNode<T>>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                BSTNode<T> curr = stack.Pop();

                nodes.Add(curr.Value);

                if (curr.Right != null)
                {
                    stack.Push(curr.Right);
                }

                if (curr.Left != null)
                {
                    stack.Push(curr.Left);
                }
            }

            return nodes.ToArray();
        }

        public T[] InOrder()
        {
            List<T> nodes = new List<T>();

            Stack<BSTNode<T>> stack = new Stack<BSTNode<T>>();

            BSTNode<T> curr = root;

            while (curr != null || stack.Count != 0)
            {
                if (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.Left;
                }
                else
                {
                    curr = stack.Pop();
                    nodes.Add(curr.Value);
                    curr = curr.Right;
                }
            }

            return nodes.ToArray();
        }

        public IEnumerable<T> BreadthFirst()
        {
            List<BSTNode<T>> temp = new List<BSTNode<T>>();
            temp.Add(root);
            Queue<T> nodes = new Queue<T>();

            while (temp.Count != 0)
            {
                BSTNode<T> cur = temp[temp.Count - 1];

                if (cur.Left != null)
                {
                    temp.Add(cur.Left);
                }
                if (cur.Right != null)
                {
                    temp.Add(cur.Right);
                }
            }

            return nodes.ToArray();
        }
    }
}
