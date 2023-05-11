using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using WebApplication2.DataBaceAccess;
    using WebApplication2.Models;

    [ApiController]
    [Route("[controller]")]
    public class FinalController : ControllerBase
    {
        private readonly Final _final;

        public FinalController()
        {
            _final = new Final();
        }

        [HttpGet]
        public ActionResult<List<(Person, Vaccination, PerVecc)>> GetPersonVaccinationJoin()
        {
            var result = _final.GetPersonVaccinationJoin();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<List<(Person, Vaccination, PerVecc)>> GetById(int id)
        {
            var result = _final.GetById(id);
            if (result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Add([FromBody] (Person, PerVecc, Vaccination) data)
        {
            var person = data.Item1;
            var perVecc = data.Item2;
            var vaccination = data.Item3;

            _final.Add(person, perVecc, vaccination);

            return Ok();
        }
    }
    
}
