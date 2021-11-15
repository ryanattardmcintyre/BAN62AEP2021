using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    //it is the gateway to the database
    //by applying inheritance i will be able to use built-in methods that will allow me to use LINQ e.g. querying, adding data, deleting, ....
    public class BloggingContext: IdentityDbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; } //table names plural but class names singular
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
