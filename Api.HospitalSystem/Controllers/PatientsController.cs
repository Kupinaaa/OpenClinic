using Api.HospitalSystem.Data;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Dtos;
using Api.HospitalSystem.Dtos.PatientDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.HospitalSystem.Enums;
using Microsoft.EntityFrameworkCore;
using Api.HospitalSystem.Repositories;
using Api.HospitalSystem.Interfaces;

namespace Api.HospitalSystem.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPatientRepository _repo;
        public PatientsController(IPatientRepository repository, ApplicationDbContext context)
        {
            _repo = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _repo.GetAllAsync();
            var patientDtos = patients.Select(p => p.ToPatientDto());

            return Ok(patientDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var patient = await _repo.GetByIdAsync(id);
            if (patient == null) return NotFound();

            return Ok(patient.ToPatientDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientRequestDto createPatientDto)
        {
            var createPatient = createPatientDto.ToPatient();
            await _repo.CreateAsync(createPatient);

            return CreatedAtAction(nameof(GetById), new { id = createPatient.Id }, createPatient.ToPatientDto());
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient([FromRoute] int id, [FromBody] UpdatePatientRequestDto updatePatientDto)
        {
            var updatePatient = await _repo.UpdateAsync(id, updatePatientDto);
            if (updatePatient == null) return NotFound();

            return Ok(updatePatient.ToPatientDto());
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id)
        {
            var deletePatient = await _repo.DeleteAsync(id);

            if (deletePatient == null) return NotFound();
            return NoContent();
        }
    }
}
