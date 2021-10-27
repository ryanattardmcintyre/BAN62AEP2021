using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class CategoryService: ICategoryService
    {
        private ICategoryRepository categoryRepo;
        public CategoryService(ICategoryRepository _categoryRepo)
        {
            categoryRepo = _categoryRepo;
        }

        public IQueryable<Category> GetCategories()
        {
            return categoryRepo.GetCategories();
        }
    }
}
