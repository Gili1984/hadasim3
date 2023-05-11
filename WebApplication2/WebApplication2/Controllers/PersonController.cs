using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    using WebApplication2.DataBaceAccess;
    using WebApplication2.Models;

    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    namespace YourNamespace.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class PersonController : ControllerBase
        {
            private readonly PersonDB _personDB;

            public PersonController()
            {
                _personDB = new PersonDB();
            }

            [HttpGet]
            public IActionResult GetPeople()
            {
                return Ok(_personDB.GetPeople());
            }

            [HttpGet("{id}")]
            public ActionResult<Person> GetPersonById(int id)
            {
                var person = _personDB.GetPersonById(id);
                if (person == null)
                {
                    return NotFound();
                }
                return person;
            }

            [HttpPost]
            public ActionResult<Person> AddPerson(Person person)
            {
                int newId = _personDB.AddPerson(person);
                return CreatedAtAction(nameof(GetPersonById), new { id = newId }, person);
            }
        }
    }


}
