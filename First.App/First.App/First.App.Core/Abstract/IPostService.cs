using System;
using System.Collections.Generic;
using System.Text;
using First.App.Domain.Entities;

namespace First.App.Business.Abstract
{
    public interface IPostService
    {
        List<Post> GetAll();
        void Add(Post post);
        

    }
}
