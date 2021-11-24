using Application.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interfaces
{
    public interface IBlogService
    {
        public IQueryable<BlogViewModel> GetBlogs();
        public BlogViewModel GetBlog(int id);
        public void AddBlog(BlogCreationModel b);

        public void DeleteBlog(int id);

        public void UpdateBlog(BlogViewModel updatedBlog);
        
    }
}
