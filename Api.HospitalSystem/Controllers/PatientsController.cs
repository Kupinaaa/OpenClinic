using Api.HospitalSystem.Data;
using Api.HospitalSystem.Dtos;
using Api.HospitalSystem.Dtos.PatientDtos;
using Microsoft.AspNetCore.Mvc;
using Api.HospitalSystem.Interfaces;

namespace Api.HospitalSystem.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPatientService _patientService;
        public PatientsController(IPatientService patientService, ApplicationDbContext context)
        {
            _patientService = patientService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patientDtos = await _patientService.GetAllPatients();

            return Ok(patientDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var patientDto = await _patientService.GetPatientById(id);

            if (patientDto == null) return NotFound();
            return Ok(patientDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] PatientCreateRequestDto createPatientDto)
        {
            PatientDto createdPatientDto = await _patientService.CreatePatient(createPatientDto);

            return CreatedAtAction(nameof(GetById), new { id = createdPatientDto.Id }, createdPatientDto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient([FromRoute] int id, [FromBody] PatientUpdateRequestDto updatePatientDto)
        {
            PatientDto? updatedPatientDto = await _patientService.UpdatePatient(id, updatePatientDto);

            if (updatedPatientDto == null) return NotFound();
            return Ok(updatedPatientDto);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id)
        {
            var deletePatient = await _patientService.DeletePatient(id);

            if (deletePatient == null) return NotFound();
            return NoContent();
        }
    }
}
