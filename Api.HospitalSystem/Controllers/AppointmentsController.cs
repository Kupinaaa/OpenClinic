using Api.HospitalSystem.Data;
using Api.HospitalSystem.Dtos;
using Api.HospitalSystem.Dtos.AppointmentDtos;
using Api.HospitalSystem.Dtos.PatientDtos;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.HospitalSystem.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentsController(IAppointmentRepository repo) 
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _repo.GetAllAsync();
            var appointmentResponseDtos = appointments.Select(a => a.ToAppointmentResponseDto());

            return Ok(appointmentResponseDtos);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentCreateRequestDto appointmentDto)
        {
            var createAppointment = appointmentDto.ToAppointment();

            await _repo.CreateAsync(createAppointment);

            return Ok(createAppointment.ToAppointmentResponseDto());
        }

        // [HttpGet("{id}")]
        // public IActionResult GetById([FromRoute] int id)
        // {
        //     return NotFound();
        // }
    }
}