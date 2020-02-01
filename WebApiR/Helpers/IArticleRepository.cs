using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR.Helpers
{
    public interface IArticleRepository
    {
        Article Add(Article article);

        Article Update(int id, Article article);

        Article Get(int articleId);

        List<Article> Get();

        List<Article> GetByCategory(string id);

        bool Delete(int articleId);
    }
}