using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.Collections
{
    class PriorityItem<T>
    {
        public PriorityItem(T item, Priority priority)
        {
            Priority = priority;
            Item = item;
        }

        public T Item { get; private set; }
        public Priority Priority { get; private set; }
    }
    public class PriorityQueue<T>
    {
        public int Count { get { return items.Count; } }

        List<PriorityItem<T>> items = new List<PriorityItem<T>>(); 
 
        public void Enqueue(T item)
        {
            Enqueue(item, Priority.Normal);
//            Count++;
        }

        public T Dequeue()
        {
            if (items.Count == 0)
            {
                throw new InvalidOperationException();
            }
//            Count--;
            PriorityItem<T> itemToReturn = null;
            int? firstNormalItemIndex = null;
            for (int i = 0; i < items.Count; i++)
            {
                PriorityItem<T> currentItem = items[i];
                if (currentItem.Priority == Priority.High)
                {
                    itemToReturn = currentItem;
                    items.RemoveAt(i);
                    break;
                }
                if (firstNormalItemIndex == null && currentItem.Priority == Priority.Normal)
                {
                    firstNormalItemIndex = i;
                }
            }

            if (itemToReturn == null)
            {
                if (firstNormalItemIndex != null)
                {
                    int i = (int) firstNormalItemIndex;
                    itemToReturn = items[i];
                    items.RemoveAt(i);
                }
                else
                {
                    itemToReturn = items[0];
                    items.RemoveAt(0);                    
                }
            }
            return itemToReturn.Item;
        }

        public void Enqueue(T val, Priority priority)
        {
            items.Add(new PriorityItem<T>(val, priority));
        }
    }
}
