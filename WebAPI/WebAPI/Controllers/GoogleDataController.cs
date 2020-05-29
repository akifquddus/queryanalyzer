using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Business;
using Google.Cloud.Language.V1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GoogleDataController : Controller
    {
      // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var client = LanguageServiceClient.Create();
            var GoogleApi = new Business.GoogleApiCalls.GoogleApi(client);
            var response = client.AnalyzeSentiment(new Document()
            {
                Content = "André is terrible",
                Type = Document.Types.Type.PlainText
            });
            var sentiment = response.DocumentSentiment;
            Console.WriteLine($"Score: {sentiment.Score}");
            Console.WriteLine($"Magnitude: {sentiment.Magnitude}");

            return new string[] { $"Score: {sentiment.Score}", $"Magnitude: {sentiment.Magnitude}" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
