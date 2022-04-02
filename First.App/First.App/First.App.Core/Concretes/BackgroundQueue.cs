using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using First.App.Business.Abstract;
using First.App.Domain.Entities;

namespace First.App.Business.Concretes
{
    public class BackgroundQueue<T>:IBackgroundQueue<T> where T : class
    {
        private readonly ConcurrentQueue<T> _items = new ConcurrentQueue<T>();
        public T Dequeue()
        {
            var success = _items.TryDequeue(out var workItem);
            return success ? workItem : null;
        }

        public void Enqueue(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _items.Enqueue(item);
        }
    }
}
