using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR.Helpers
{
    public class DefaultCategoryRepository : ICategoryRepository
    {
        private readonly WebApiRDbContext _context;
        public DefaultCategoryRepository(WebApiRDbContext context)
        {
            this._context = context;
        }

        public Category Add(Category category)
        {
            this._context.Categories.Add(category);
            if (this._context.SaveChanges() != 1)
                throw new Exception("Unable to save category in Database.");

            return this.Get(category.CategoryId);
        }

        public bool Delete(int categoryId)
        {
            var CategoryRemoved = _context.Categories.Find(categoryId);
            if (CategoryRemoved != null)
            {
                _context.Remove(CategoryRemoved);
                var performedOperations = _context.SaveChanges();
                if (performedOperations > 0)
                    return true;
                else
                    return false;
                }
                else
                return false;
        }

        public List<Category> Get()
        {
            return _context.Categories.ToList();
        }

        public Category Get(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            return category;
        }

        public Category Get(String categoryName)
        {
            var category = _context.Categories.SingleOrDefault(user => user.CategoryName == categoryName);
            return category;
        }

        public Category Update(int categoryId,Category categoryP)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null)
            {
                category.CategoryName = categoryP.CategoryName;
                category.Image = categoryP.Image;
                _context.SaveChanges();
                return this.Get(category.CategoryId);
            }
            else
                return null;
        }
    }
}
