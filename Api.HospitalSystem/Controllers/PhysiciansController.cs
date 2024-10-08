using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.HospitalSystem.Controllers
{
    [Route("api/physician")]
    [ApiController]
    public class PhysiciansController: ControllerBase
    {
        private readonly IPhysicianService _physicianService;
        public PhysiciansController(IPhysicianService physicianService)
        {
            _physicianService = physicianService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var physicianDtos = await _physicianService.GetAllAsync();

            return Ok(physicianDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var physicianDto = await _physicianService.GetByIdAsync(id);
            if (physicianDto == null) return NotFound();

            return Ok(physicianDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhysicianCreateRequestDto createPhysicianDto)
        {
            PhysicianDto createdPhysicianDto = await _physicianService.CreateAsync(createPhysicianDto);

            return CreatedAtAction(nameof(GetById), new { id = createdPhysicianDto.Id }, createdPhysicianDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PhysicianUpdateRequestDto updatePhysicianDto)
        {
            PhysicianDto? updatedPhysicianDto = await _physicianService.UpdateAsync(id, updatePhysicianDto);
            if (updatedPhysicianDto == null) return NotFound();

            return Ok(updatedPhysicianDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            PhysicianDto? deletePatient = await _physicianService.DeleteAsync(id);
            if (deletePatient == null) return NotFound();

            return NoContent();
        }
    }
}
