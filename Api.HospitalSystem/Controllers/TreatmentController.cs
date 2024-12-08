using Api.HospitalSystem.Dtos.TreatmentDtos;
using Api.HospitalSystem.Interfaces.Services;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.HospitalSystem.Controllers
{
    [Route("api/treatment")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        private readonly ITreatmentService _service;
        public TreatmentController(ITreatmentService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<TreatmentDto> treatmentDtos = await _service.GetTreatmentOptions();
            return Ok(treatmentDtos);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TreatmentCreateRequestDto treatmentCreateRequestDto)
        {
            TreatmentDto createdTreatment = await _service.CreateTreatementOption(treatmentCreateRequestDto);
            return Ok(createdTreatment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            TreatmentDto? treatmentDto = await _service.DeleteTreatmentOption(id);
            if (treatmentDto == null) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TreatmentCreateRequestDto treatmentUpdateRequestDto)
        {
            TreatmentDto? treatmentDto = await _service.UpdateTreatmentOption(id, treatmentUpdateRequestDto);
            if (treatmentDto == null) return NotFound();
            return Ok(treatmentDto);
        }
    }
}
