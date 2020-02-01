using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR.Helpers
{
    public class DefaultArticleRepository : IArticleRepository
    {
        private readonly WebApiRDbContext _context;
        public DefaultArticleRepository(WebApiRDbContext context)
        {
            this._context = context;
        }

        public Article Add(Article article)
        {
                String[] tags = article.CategoryId.Split(',');
                Category currentCategory;
                int currentCategoryId=-1;
            foreach (String categId in tags)
            {
                if (int.TryParse(categId, out currentCategoryId))
                {
                    currentCategory = null;
                    currentCategory = _context.Categories.SingleOrDefault(cat => cat.CategoryId == currentCategoryId);
                    if (currentCategory == null)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
                }
            try
            {
                this._context.Article.Add(article);
                this._context.SaveChanges();
            }
            catch
            {
                return null;
            }
            return (this.Get(article.ArticleId));
        }

        public bool Delete(int articleId)
        {
            var articleRemoved = _context.Article.Find(articleId);
            if (articleRemoved != null)
            {
                _context.Remove(articleRemoved);
                var performedOperations = _context.SaveChanges();
                if (performedOperations > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public List<Article> Get()
        {
            List<Article> articles = _context.Article.ToList();
            foreach (Article article in articles)
            {
                article.CategoryIds = Array.ConvertAll(article.CategoryId.Split(","), s => int.Parse(s));
            }
            return articles;
        }

        public Article Get(int articleId)
        {
            var article = _context.Article.Find(articleId);
            if (article != null)
                article.CategoryIds = Array.ConvertAll(article.CategoryId.Split(","), s => int.Parse(s));

            return article;
        }

        public List<Article> GetByCategory(string id)
        {
            List<Article> articles = _context.Article.Where(_ => _.CategoryId.Contains(id)).ToList();
            foreach (Article article in articles)
            {
                article.CategoryIds = Array.ConvertAll(article.CategoryId.Split(","), s => int.Parse(s));
            }
            return articles;
        }

        public Article Update(int id, Article articleP)
        {
            var article = _context.Article.Find(id);
            if (article != null)
            {
                String[] tags = articleP.CategoryId.Split(',');
                Category currentCategory;
                int currentCategoryId = -1;
                foreach (String categId in tags)
                {
                    if (int.TryParse(categId, out currentCategoryId))
                    {
                        currentCategory = null;
                        currentCategory = _context.Categories.SingleOrDefault(cat => cat.CategoryId == currentCategoryId);
                        if (currentCategory == null)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                try
                {
                    article.CategoryId= articleP.CategoryId;
                    article.Content= articleP.Content;
                    article.Image= articleP.Image;
                    article.Title= articleP.Title;
                    this._context.SaveChanges();
                    return (this.Get(article.ArticleId));
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
    }
}
