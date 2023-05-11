using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using WebApplication2.DataBaceAccess;
    using WebApplication2.Models;

    namespace YourNamespace.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class PerVeccController : ControllerBase
        {
            private readonly PerVeccDB _perVeccDB;

            public PerVeccController()
            {
                _perVeccDB = new PerVeccDB();
            }

            [HttpGet]
            public IActionResult GetPerVeccs()
            {
                return Ok(_perVeccDB.GetPerVecc());
            }

            [HttpGet("{id}")]
            public IActionResult GetPerVeccById(int id)
            {
                var perVecc = _perVeccDB.GetPerVeccById(id);

                if (perVecc == null)
                {
                    return NotFound();
                }

                return Ok(perVecc);
            }

            [HttpPost]
            public IActionResult AddPerVecc([FromBody] PerVecc perVecc)
            {
                if (perVecc == null)
                {
                    return BadRequest();
                }

                _perVeccDB.AddPerVecc(perVecc);

                return CreatedAtAction(nameof(GetPerVeccById), new { id = perVecc.Id }, perVecc);
            }
        }
    }

}
