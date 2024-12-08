using Api.HospitalSystem.Data;
using Api.HospitalSystem.Dtos;
using Api.HospitalSystem.Dtos.AppointmentDtos;
using Api.HospitalSystem.Dtos.PatientDtos;
using Api.HospitalSystem.Dtos.PhysicianDtos;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Mappers;
using Api.HospitalSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.HospitalSystem.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentsController: ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentsController(IAppointmentService appointmentService) 
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<AppointmentDto> appointmentDtos = await _appointmentService.GetAllAppointments();
            return Ok(appointmentDtos);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentCreateRequestDto appointmentDto)
        {
            AppointmentDto? createdAppointment = await _appointmentService.CreateAppointment(appointmentDto);
            if (createdAppointment == null) return BadRequest();

            return CreatedAtAction(nameof(GetById), new {id = createdAppointment.Id}, createdAppointment);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            AppointmentDto? appointmentDto = await _appointmentService.GetAppointmentById(id);
            if (appointmentDto == null) return NotFound();
            return Ok(appointmentDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            AppointmentDto? deletedAppointemntDto = await _appointmentService.DeleteAppointment(id);
            if(deletedAppointemntDto == null) return NotFound();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AppointmentUpdateRequestDto updateRequestDto)
        {
            AppointmentDto? updatedAppointment = await _appointmentService.UpdateAppointment(id, updateRequestDto);
            if (updatedAppointment == null) return NotFound();
            return Ok(updatedAppointment);
        }
        [HttpGet("physician/{physicianId}")]
        public async Task<IActionResult> GetPhysicianAppointments([FromRoute] int physicianId)
        {
            List<AppointmentDto> physicianAppointmentDtos = await _appointmentService.GetPhysicianAppointments(physicianId);
            return Ok(physicianAppointmentDtos);
        }
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetPatientAppointments([FromRoute] int patientId)
        {
            List<AppointmentDto> patientAppointmentDtos = await _appointmentService.GetPatientAppointments(patientId);
            return Ok(patientAppointmentDtos);
        }
        [HttpGet("upcoming/physician/{physicianId}")]
        public async Task<IActionResult> GetUpcomingPhysicianAppointments([FromRoute] int physicianId, [FromQuery] DateTime now)
        {
            List<AppointmentDto> physicianAppointemntDtos = await _appointmentService.GetUpcomingPhysicianAppointments(physicianId, now);
            return Ok(physicianAppointemntDtos);
        }
        [HttpGet("upcoming/patient/{patientId}")]
        public async Task<IActionResult> GetUpcomingPatientAppointments([FromRoute] int patientId, [FromQuery] DateTime now)
        {
            List<AppointmentDto> patientAppointmentDtos = await _appointmentService.GetUpcomingPatientAppointments(patientId, now);
            return Ok(patientAppointmentDtos);
        }

        [HttpGet("availability/{physicianId}")]
        public async Task<IActionResult> GetPhysicianAvailability([FromRoute] int physicianId, [FromQuery] DateTime day, [FromQuery] int? updateId)
        {
            List<DateTime> physicianAvailability = await _appointmentService.GetPhysicianAvailability(physicianId, day.Date, updateId);
            return Ok(physicianAvailability);
        }
    }
}