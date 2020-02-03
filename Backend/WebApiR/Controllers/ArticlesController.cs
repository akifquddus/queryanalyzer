using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiR.Helpers;
using WebApiR.Models;

namespace WebApiR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : Controller
    {

        readonly IArticleRepository _articleRepository;
         public ArticlesController(IArticleRepository articleRepository)
        {
            this._articleRepository = articleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Article> article = this._articleRepository.Get();
            return Ok(article);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var article = this._articleRepository.Get(id);
            if (article != null)
                return Ok(article);
            else
                return BadRequest(new List<String> { "Unable to Find request in database" });
        }

        [HttpGet("getarticles/{id}")]
        public IActionResult GetArticles(string id)
        {
            var article = this._articleRepository.GetByCategory(id);
            if (article != null)
                return Ok(article);
            else
                return BadRequest(new List<String> { "Unable to Find request in database" });
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Article _article)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var article = this._articleRepository.Add(_article);
            if (article != null)
                return Created("/", article);
            else {
                return BadRequest(new List<String> { "Could not add your article"});
            }
            
        }

        // PUT api/<controller>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]Article _article)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var article = this._articleRepository.Update(id, _article);
            if (article != null)
                return Created("/", article);
            else
            {
                return BadRequest(new List<String> { "Article not updated"});
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = this._articleRepository.Delete(id);
            if (response)
                return Ok();
            else
                return BadRequest(new List<String> { "Unable to Delete in database" });
        }
    }
}
