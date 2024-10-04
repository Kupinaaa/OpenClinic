using Api.HospitalSystem.Data;
using Api.HospitalSystem.Dtos.PatientDtos;
using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.HospitalSystem.Controllers
{
    [Route("api/physician")]
    [ApiController]
    public class PhysiciansController : ControllerBase
    {
        private readonly IPhysicianRepository _repo;
        public PhysiciansController(IPhysicianRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var physicians = await _repo.GetAllAsync();
            var physicianDtos = physicians.Select(p => p.ToPhysicianDto());

            return Ok(physicianDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var physician = await _repo.GetByIdAsync(id);
            if (physician == null) return NotFound();

            return Ok(physician.ToPhysicianDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePhysicianRequestDto createPhysicianDto)
        {
            var createPhysicanModel = createPhysicianDto.ToPhysician();
            await _repo.CreateAsync(createPhysicanModel);

            return CreatedAtAction(nameof(GetById), new { id = createPhysicanModel.Id }, createPhysicanModel.ToPhysicianDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePhysicianRequestDto updatePhysicianDto)
        {
            var updatePhysician = await _repo.UpdateAsync(id, updatePhysicianDto);
            if (updatePhysician == null) return NotFound();

            return Ok(updatePhysician.ToPhysicianDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deletePatient = await _repo.DeleteAsync(id);
            if (deletePatient == null) return NotFound();

            return NoContent();
        }
    }
}
