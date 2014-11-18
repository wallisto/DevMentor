using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.Collections
{
    public class PriorityQueue<T>
    {
        public int Count { get { return items.Count; } }
        private List<T> items = new List<T>();
        
        public void Enqueue(T item)
        {
            this.items.Add(item);
        }

        public object Dequeue()
        {
            try
            {
                T item = items[0];
                items.RemoveAt(0);
                return item;
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }
    }
}
