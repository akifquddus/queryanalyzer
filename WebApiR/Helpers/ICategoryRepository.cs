using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR.Helpers
{
    public interface ICategoryRepository
    {
        Category Add(Category category);

        Category Update(int categoryId, Category category);

        Category Get(int categoryId);

        List<Category> Get();

        bool Delete(int categoryId);
    }
}
