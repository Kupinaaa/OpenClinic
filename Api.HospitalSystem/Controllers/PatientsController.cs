using Api.HospitalSystem.Data;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Dtos;
using Api.HospitalSystem.Dtos.PatientDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.HospitalSystem.Enums;

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
        public IActionResult CreatePatient([FromBody] CreatePatientRequestDto createPatientDto)
        {
            var createPatient = createPatientDto.ToPatient();
            _context.Patients.Add(createPatient);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = createPatient.Id }, createPatient.ToPatientDto());
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdatePatient([FromRoute] int id, [FromBody] UpdatePatientRequestDto updatePatientDto)
        {
            var updatePatient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (updatePatient == null) return NotFound();

            updatePatient.Name = updatePatientDto.Name;
            updatePatient.AddressLine = updatePatientDto.AddressLine;
            updatePatient.DOB = updatePatientDto.DOB;
            updatePatient.Gender = updatePatientDto.Gender;
            updatePatient.Race = updatePatientDto.Race;

            _context.SaveChanges();

            return Ok(updatePatient.ToPatientDto());
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeletePatient([FromRoute] int id)
        {
            var deletePatient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (deletePatient == null) return NotFound();
            _context.Patients.Remove(deletePatient);
            return NoContent();
        }
    }
}
