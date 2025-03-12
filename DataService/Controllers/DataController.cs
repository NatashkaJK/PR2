using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataService.Models;

namespace DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {

        private static List<Data> Datas = new List<Data>();

        [HttpGet]
        public ActionResult<IEnumerable<Data>> Get()
        {
            return Ok(Datas);
        }

        [HttpGet("{id}")]
        public ActionResult<Data> Get(int id)
        {
            var data = Datas.FirstOrDefault(u=>u.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpPost]
        public ActionResult Post([FromBody] Data data)
        {
            Datas.Add(data);
            return CreatedAtAction(nameof(Get), new { id = data.Id, data});
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Data updaredData)
        {
            var data = Datas.FirstOrDefault(u=>u.Id == id);
            if(data == null)
            {
                return NotFound();
            }
            data.Name = updaredData.Name;
            data.Email = updaredData.Email;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var data = Datas.FirstOrDefault(u =>u.Id == id);
            if(data == null)
            {
                return NotFound();
            }
            Datas.Remove(data);
            return NoContent();
        }
    }
}