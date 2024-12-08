using System;

namespace Api.HospitalSystem.Dtos.TreatmentDtos;

public class AppointmentTreatmentCreateRequestDto
{
    public int AppointmentId { get; set; } = 0;
    public int TreatmentId { get; set; } = 0;
}
