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
    public class QuestionsController : Controller
    {

        readonly IQuestionRepository _questionRepository;

        public QuestionsController(IQuestionRepository questionRepository) => this._questionRepository = questionRepository;

        public IActionResult Index()
        {
            return View();
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            List<Question> questions = this._questionRepository.Get();
            return Ok(questions);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var question = this._questionRepository.Get(id);
            if (question != null)
                return Ok(question);
            else
                return BadRequest(new List<String> { "Unable to Find request in database" });
        }

        // POST api/<controller>
        [HttpPost]
        //testar dps public void Post([FromBody]string value)
        public IActionResult Post([FromBody] Question _question)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var question = this._questionRepository.Add(_question);
            if (question.QuestionId > 0)
                return Created("/", question);
            else
                return BadRequest(new List<String> { "Unable to save in database" });
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Question _question)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var question = this._questionRepository.Update(id,_question);
            if (question !=null)
                return Created("/", question);
            else
                return BadRequest(new List<String> { "Unable to update in database" });
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = this._questionRepository.Delete(id);
            if (response)
                return Ok();
            else
                return BadRequest(new List<String> { "Unable to Delete in database" });

        }
    }
}
