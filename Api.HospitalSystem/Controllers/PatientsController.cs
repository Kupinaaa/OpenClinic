using Api.HospitalSystem.Data;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Dtos;
using Api.HospitalSystem.Dtos.PatientDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.HospitalSystem.Enums;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll()
        {
            var patients = await _context.Patients.ToListAsync();
            var patientDtos = patients.Select(p => p.ToPatientDto());
            return Ok(patientDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient.ToPatientDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientRequestDto createPatientDto)
        {
            var createPatient = createPatientDto.ToPatient();
            await _context.Patients.AddAsync(createPatient);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = createPatient.Id }, createPatient.ToPatientDto());
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient([FromRoute] int id, [FromBody] UpdatePatientRequestDto updatePatientDto)
        {
            var updatePatient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (updatePatient == null) return NotFound();

            updatePatient.Name = updatePatientDto.Name;
            updatePatient.AddressLine = updatePatientDto.AddressLine;
            updatePatient.DOB = updatePatientDto.DOB;
            updatePatient.Gender = updatePatientDto.Gender;
            updatePatient.Race = updatePatientDto.Race;

            await _context.SaveChangesAsync();

            return Ok(updatePatient.ToPatientDto());
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id)
        {
            var deletePatient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (deletePatient == null) return NotFound();
            _context.Patients.Remove(deletePatient);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
