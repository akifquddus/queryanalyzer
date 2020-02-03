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
    public class AnswersController : Controller
    {

        readonly IAnswersRepository _answerRepository;
        readonly IQuestionRepository _questionRepository;
        readonly IUserRepository _userRepository;

        public AnswersController(IAnswersRepository answerRepository, IQuestionRepository questionRepository, IUserRepository userRepository)
        {
            this._answerRepository = answerRepository;
            this._questionRepository = questionRepository;
            this._userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            List<Answers> answers = this._answerRepository.Get();
            return Ok(answers);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var question = this._answerRepository.Get(id);
            if (question != null)
                return Ok(question);
            else
                return BadRequest(new List<String> { "Unable to Find request in database" });
        }

        [HttpGet("getanswers/{userid}/{questionid}")]
        public IActionResult GetAnswers(int userid, int questionid)
        {
            var answer = this._answerRepository.GetByUserQuestion(userid, questionid);
            if (answer != null)
                return Ok(answer);
            else
                return BadRequest(new List<String> { "Unable to Find request in database" });
        }

        // POST api/<controller>
        [HttpPost]
        //testar dps public void Post([FromBody]string value)
        public IActionResult Post([FromBody] Answers _answer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (this._userRepository.GetUser(_answer.UserId) == null)
                return BadRequest(new List<String> { "User does not exist" });

            if (this._questionRepository.Get(_answer.QuestionId) == null)
                return BadRequest(new List<String> { "Question does not exist" });

            var answer = this._answerRepository.Add(_answer);
            if (answer != null)
                return Created("/", _answer);
            else
                return BadRequest(new List<String> { "Unable to save in database" });
        }

        // PUT api/<controller>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]Answers _answer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var question = this._answerRepository.Update(id, _answer);
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

            var response = this._answerRepository.Delete(id);
            if (response)
                return Ok();
            else
                return BadRequest(new List<String> { "Unable to Delete in database" });

        }
    }
}
