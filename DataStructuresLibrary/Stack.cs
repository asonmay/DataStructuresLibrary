using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary
{
    public class Stack<T>
    {
        private List<T> data;
        public int Count { get { return data.Count; } }
        
        public void Push(T value)
        {
            data.Add(value);
        }

        public T Pop()
        {
            T value = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);
            return value;
        }

        public T Peak()
        {
            return data[data.Count - 1];
        }

        public T[] ToArray()
        {
            return data.ToArray();
        }
    }
}
