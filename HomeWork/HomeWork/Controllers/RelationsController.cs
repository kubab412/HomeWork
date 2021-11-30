using HoweWorkDb.Models;
using HoweWorkDb.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork.Controllers
{
    [ApiController]
    [Route("relation")]
    public class RelationsController : ControllerBase
    {
        private readonly RelationsRepository _db;
        public RelationsController(RelationsRepository db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var result =_db.GetAll();
            return Ok(result);
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AdRelation([FromBody]Relations relation)
        {
            string name = relation.FirstName;
            string placeName = relation.Name;
            _db.Insert(name, placeName);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            _db.Remove(id);
            return Ok();
        }
    }
}
