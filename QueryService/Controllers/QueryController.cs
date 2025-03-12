using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QueryService.Models;

namespace QueryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private static List<Query> Querys = new List<Query>();
        private readonly HttpClient _httpClient;
        
        public QueryController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Query>> Get()
        {
            return Ok(Querys);
        }

        [HttpGet("{id}")]
        public ActionResult<Query> Get(int id)
        {
            var query = Querys.FirstOrDefault(o=> o.Id == id);
            if (query == null)
            {
                return NotFound();
            }
            return Ok(query);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Query query)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://dataservice/api/data/{query.UserId}");
                if(!response.IsSuccessStatusCode)
                {
                    return BadRequest("User not found");
                }
                Querys.Add(query);
                return CreatedAtAction(nameof(Get), new {id = query.Id}, query);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Query updatedQuery)
        {
            var query = Querys.FirstOrDefault(o => o.Id == id);
            if(query == null)
            {
                return NotFound();
            }

            query.UserId = updatedQuery.UserId;
            query.Product = updatedQuery.Product;
            query.Quantity = updatedQuery.Quantity;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var query = Querys.FirstOrDefault(o => o.Id == id);
            if (query == null)
            {
                return NotFound();
            }

            Querys.Remove(query);
            return NoContent();
        }
    }
}