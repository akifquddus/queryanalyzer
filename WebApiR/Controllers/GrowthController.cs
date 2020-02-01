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
    public class GrowthController : Controller
    {

        readonly IGrowthRepository _growthRepository;
        readonly IUserRepository _userRepository;

        public GrowthController(IGrowthRepository growthRepository, IUserRepository userRepository)
        {
            this._growthRepository = growthRepository;
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
            List<Growth> growth = this._growthRepository.Get();
            return Ok(growth);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var growth = this._growthRepository.Get(id);
            if (growth != null)
                return Ok(growth);
            else
                return BadRequest(new List<String> { "Unable to Find request in database" });
        }

        [HttpGet("getbyuser/{id}")]
        public IActionResult GetByUser(int id)
        {
            var growth = this._growthRepository.GetbyUser(id);
            if (growth != null)
            {
                var returnvar = new { birth = "2019-01-31", entries = growth };
                return Ok(returnvar);
            }
            else
                return BadRequest(new List<String> { "Unable to Find request in database" });
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Growth _growth)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = this._userRepository.GetUser(_growth.UserId);
            if (user == null)
                return BadRequest(new List<String> { "User does not exist" });

            var growth = this._growthRepository.Add(_growth);
            if (growth.GrowthId > 0)
                return Created("/", growth);
            else
                return BadRequest(new List<String> { "Unable to save in database" });
        }

        // PUT api/<controller>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]Growth _growth)
        {
            var growth = this._growthRepository.Update(id, _growth);
            if (growth != null)
                return Created("/", growth);
            else
                return BadRequest(new List<String> { "Unable to update in database" });
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = this._growthRepository.Delete(id);
            if (response)
                return Ok();
            else
                return BadRequest(new List<String> { "Unable to Delete in database" });

        }
    }
}
