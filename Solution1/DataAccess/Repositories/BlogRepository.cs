using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        public BloggingContext context { get; set; }
        public BlogRepository(BloggingContext _context)
        {
            context = _context;
        }

        public void AddBlog(Blog b)
        {
            b.DateCreated = DateTime.Now;
            context.Blogs.Add(b);
            context.SaveChanges();
        }

        public void DeleteBlog(Blog b)
        {
            context.Blogs.Remove(b);
            context.SaveChanges();

        }

        public Blog GetBlog(int id)
        {
            //lambda expression

            //take x as an argument which will represent all the Blogs in the db
            //for each x (Blog), evaluate the condition: x.Id == id

            return context.Blogs.SingleOrDefault(x => x.Id == id);

            //foreach (Blog x in context.Blogs)
            //    if (x.Id == id) return x;
        }

        public IQueryable<Blog> GetBlogs()
        {
            return context.Blogs;
        }

        public void UpdateBlog(Blog b)
        {
            Blog originalBlog = GetBlog(b.Id);

            originalBlog.DateCreated = b.DateCreated;
            originalBlog.DateUpdated = DateTime.Now;
            originalBlog.LogoImagePath = b.LogoImagePath;
            originalBlog.Name = b.Name;
            originalBlog.CategoryId = b.CategoryId;
        }
    }
}
