using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    //In interfaces you only declare the methods
    public interface IBlogRepository
    {
        public IQueryable<Blog> GetBlogs();

        public void AddBlog(Blog b);

        public void DeleteBlog(Blog b);

        public void UpdateBlog(Blog b);

        public Blog GetBlog(int id);


    }
}
