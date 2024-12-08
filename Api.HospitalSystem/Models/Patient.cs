using System.Security.Cryptography;
using Api.HospitalSystem.Enums;

namespace Api.HospitalSystem.Models;

public class Patient
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string AddressLine { get; set; } = string.Empty;
    public DateTime DOB { get; set; } = DateTime.MinValue;
    public Gender Gender { get; set; } = Gender.NotSpecified;
    public List<Race> Race { get; set; } = new List<Race>{Enums.Race.NotSpecified};

    public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public int InsurancePlanId { get; set; } = 0;
    public double Balance { get; set; } = 0;
    public double TotalPayThisYear { get; set; } = 0;
    public InsurancePlan? InsurancePlan { get; set; } = null;
    // TODO: Add medical history, tests, etc.
}