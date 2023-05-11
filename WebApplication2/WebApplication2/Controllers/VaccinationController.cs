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
        public class VaccinationController : ControllerBase
        {
            private readonly VaccinationDB _db;

            public VaccinationController()
            {
                _db = new VaccinationDB();
            }

            [HttpGet]
            public ActionResult<List<Vaccination>> GetVaccinations()
            {
                List<Vaccination> vaccinations = _db.GetVaccination();
                return Ok(vaccinations);
            }

            [HttpGet("{id}")]
            public ActionResult<Vaccination> GetVaccinationById(int id)
            {
                Vaccination vaccination = _db.GetVaccinationById(id);

                if (vaccination == null)
                {
                    return NotFound();
                }

                return Ok(vaccination);
            }

            [HttpPost]
            public ActionResult AddVaccination(Vaccination vaccination)
            {
                _db.AddVaccination(vaccination);
                return Ok();
            }
        }
    }
    }

