using Api.HospitalSystem.Dtos.InsurancePlanDtos;
using Api.HospitalSystem.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.HospitalSystem.Controllers
{
    [Route("api/insuranceplan")]
    [ApiController]
    public class InsurancePlanController : ControllerBase
    {
        private readonly IInsurancePlanService _service;
        public InsurancePlanController(IInsurancePlanService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<InsurancePlanDto> insurancePlanDtos = await _service.GetInsurancePlanOptions();
            return Ok(insurancePlanDtos);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsurancePlanCreateRequestDto insurancePlanCreateRequestDto)
        {
            InsurancePlanDto createdInsurancePlan = await _service.CreateTreatementOption(insurancePlanCreateRequestDto);
            return Ok(createdInsurancePlan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            InsurancePlanDto? insurancePlanDto = await _service.DeleteInsurancePlanOption(id);
            if (insurancePlanDto == null) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] InsurancePlanCreateRequestDto insurancePlanUpdateRequestDto)
        {
            InsurancePlanDto? insurancePlanDto = await _service.UpdateInsurancePlanOption(id, insurancePlanUpdateRequestDto);
            if (insurancePlanDto == null) return NotFound();
            return Ok(insurancePlanDto);
        }
    }
}
