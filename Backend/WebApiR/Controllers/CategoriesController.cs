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
    public class CategoriesController : Controller
    {

        readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository) => this._categoryRepository = categoryRepository;

        public IActionResult Index()
        {
            return View();
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            List<Category> category = this._categoryRepository.Get();
            return Ok(category);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = this._categoryRepository.Get(id);
            if (category != null)
                return Ok(category);
            else
                return BadRequest(new List<String> { "Unable to Find request in database" });
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Category _category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = this._categoryRepository.Add(_category);
            if (category.CategoryId > 0)
                return Created("/", category);
            else
                return BadRequest(new List<String> { "Unable to save in database" });
        }

        // PUT api/<controller>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]Category _category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = this._categoryRepository.Update(id, _category);
            if (category != null)
                return Created("/", category);
            else
                return BadRequest(new List<String> { "Unable to update in database" });
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = this._categoryRepository.Delete(id);
            if (response)
                return Ok();
            else
                return BadRequest(new List<String> { "Unable to Delete in database" });

        }
    }
}