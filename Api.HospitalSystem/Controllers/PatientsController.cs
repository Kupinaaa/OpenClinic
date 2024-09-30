using Api.HospitalSystem.Data;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Dtos;
using Api.HospitalSystem.Dtos.PatientDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.HospitalSystem.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var patients = _context.Patients.Select(p => p.ToPatientDto()).ToList();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();
            return Ok(patient.ToPatientDto());
        }

        [HttpPost]
        public IActionResult CreatePatient([FromBody] CreatePatientDto createPatientDto)
        {
            var createPatient = createPatientDto.ToPatient();
            _context.Patients.Add(createPatient);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = createPatient.Id }, createPatient.ToPatientDto());
        }
    }
}
