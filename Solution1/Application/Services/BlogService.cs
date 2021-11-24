using Application.Interfaces;
using Application.ViewModels;
using DataAccess.Repositories;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class BlogService : IBlogService
    {
        private IBlogRepository blogRepo;
        public BlogService(IBlogRepository _blogRepo )
        {
            blogRepo = _blogRepo;
        }

        public void AddBlog(BlogCreationModel b)
        {
            blogRepo.AddBlog(
                new Domain.Models.Blog()
                {
                    CategoryId = b.CategoryId,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    LogoImagePath = b.LogoImagePath,
                    Name = b.Name
                });
        }

        public void DeleteBlog(int id)
        {
            var blog = blogRepo.GetBlog(id);
            if(blog != null)
            {
                blogRepo.DeleteBlog(blog);
            }
            else
            {
                throw new Exception("Blog does not exist");
            }
        }

        public BlogViewModel GetBlog(int id)
        {
            //lazy loading is not automatically enabled
            //therefore it will not load automatically any properties
            //which relate to information which is in other tables

            BlogViewModel myModel = new BlogViewModel();
            var blog = blogRepo.GetBlog(id);
            myModel.Category = blog.Category;
            myModel.DateUpdated = blog.DateUpdated;
            //myModel.Id = blog.Id;
            myModel.LogoImagePath = blog.LogoImagePath;
            myModel.Name = blog.Name;

            return myModel;
        }

        public IQueryable<BlogViewModel> GetBlogs()
        {
            var list = blogRepo.GetBlogs().ToList();

            //without .ToList() >> method returns IQueryable
            //IQueryable >> does any operation on the (sql) server 
            //IList >> does any operation within the application

            //drawback of LazyLoading:
            //1. application is less efficient because it needs to open another connection and execute commands per information from different tables you need
            //2. it will load all the information in the referred table (it could be that you are loading too many information)

            //advantage of lazyloading
            //convenient to use because you dont need to type or formulate join (linq) commands which can be very complex

            List<BlogViewModel> myResults = new List<BlogViewModel>();

            foreach(var b in list) //this foreach loop is converting from List<Blog> to List<BlogViewModel>
            {
                myResults.Add(new BlogViewModel()
                {
                    Id = b.Id,
                    Category = b.Category,
                    DateUpdated = b.DateUpdated,
                    LogoImagePath = b.LogoImagePath,
                    Name = b.Name,
                     
                });
            }

            //instead of the foreach loop above you can use LINQ to transform from List<Blog> to List<BlogViewModel>
            //var myResultsv2 = from b in list
            //                  select new BlogViewModel
            //                  {
            //                      Id = b.Id,
            //                      Category = b.Category,
            //                      DateUpdated = b.DateUpdated,
            //                      LogoImagePath = b.LogoImagePath,
            //                      Name = b.Name
            //                  };


            return myResults.AsQueryable();
        }

        public void UpdateBlog(BlogViewModel updatedBlog)
        {
            var originalBlog = blogRepo.GetBlog(updatedBlog.Id);
            originalBlog.LogoImagePath = updatedBlog.LogoImagePath;
            originalBlog.Name = updatedBlog.Name;
            originalBlog.CategoryId = updatedBlog.Category.Id;

            blogRepo.UpdateBlog(originalBlog);
           
        }
    }
}
