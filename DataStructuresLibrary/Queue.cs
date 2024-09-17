using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary
{
    public class Queue<T>
    {
        private List<T> data;

        public int Count {  get { return data.Count; } }

        public void Enqueue(T item)
        {
            data.Add(item);
        }

        public T Dequeue()
        {
            T value = data[0];
            data.RemoveAt(0);
            return value;
        }

        public T[] ToArray()
        {
            return data.ToArray();
        }
    }
}
