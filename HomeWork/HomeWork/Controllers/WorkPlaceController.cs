using HoweWorkDb.Models;
using HoweWorkDb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers
{
    [ApiController]
    [Route("work-place")]

    public class WorkPlaceController : ControllerBase
    {
        private readonly WorkPlaceRepository _db;
            public WorkPlaceController(WorkPlaceRepository db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var result = _db.GetAll();
            return Ok(result);
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AddWorkPlace([FromBody] WorkPlace workPlace)
        {
            _db.Insert(workPlace);
            return Ok();
        }
        [HttpPut]
        [Route("edit")]
        public IActionResult Update(WorkPlace workPlace)
        {
            _db.Update(workPlace);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            _db.Remove(id);
            return Ok();
        }
        [HttpGet]
        [Route("filter/{name}")]
        public IActionResult Filter(string name)
        {
           var result = _db.Filter(name);
            return Ok(result);
        }
    }
}
