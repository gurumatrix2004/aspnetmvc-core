using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientLibrary;
using PatientRepository;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PatientDTO, PatientAddress>();
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientApi : ControllerBase
    {
        private PatientDBContext patientDBContext = null;
        private readonly IMapper _mapper;

        public PatientApi(PatientDBContext sc, IMapper mapper)
        {
            patientDBContext = sc;
            _mapper = mapper;

        }

        // GET: api/<PatientApi>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PatientApi>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PatientApi>
        [HttpPost]
        public IActionResult Post([FromBody] List<PatientDTO> IPt)
        {

            HttpContext.Session.SetString("key","add");
            foreach (var itemPatientDTO in IPt)
            {

                PatientAddress newIPT = _mapper.Map<PatientAddress>(itemPatientDTO);

                var validcontext = new System.ComponentModel.DataAnnotations.ValidationContext(newIPT, null, null);
                List<ValidationResult> errresults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(newIPT, validcontext, errresults);

                if (isValid)
                {
                    patientDBContext.patients.Add(newIPT);

                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, errresults);
                }

            }
            patientDBContext.SaveChanges();

            List<PatientAddress> pats = patientDBContext.patients.ToList<PatientAddress>();
            return Ok(pats);


        }

       

        // PUT api/<PatientApi>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientApi>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
