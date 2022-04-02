using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using First.App.Business.Abstract;
using First.App.DataAccess.EntityFramework.Repository.Abstracts;
using First.App.Domain.Entities;

namespace First.App.Business.Concretes
{
    internal class PostService:IPostService
    {
        private readonly IRepository<Post> repository;
        private readonly IUnitOfWork unitOfWork;
        public PostService(IRepository<Post> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public List<Post> GetAll()
        {
            return repository.Get().ToList();
        }
        public void Add(Post post)
        {
           repository.Add(post);
           unitOfWork.Commit();
        }
    }
}
