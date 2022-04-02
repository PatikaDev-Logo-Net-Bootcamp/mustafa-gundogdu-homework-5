using System;
using System.Collections.Generic;
using System.Text;

namespace First.App.Business.Abstract
{
    public interface IBackgroundQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
    }
}
