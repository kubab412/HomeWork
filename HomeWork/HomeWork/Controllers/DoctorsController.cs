using HoweWorkDb.Models;
using HoweWorkDb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers
{
    [ApiController]
    [Route("doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorsRepository _db;
        public DoctorsController(DoctorsRepository db)
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
        public IActionResult AddDoctor([FromBody]Doctors doctor)
        {
            _db.Insert(doctor);
            return Ok();
        }
        [HttpPut]
        [Route("edit")]
        public IActionResult Update(Doctors doctor)
        {
            _db.Update(doctor);
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
