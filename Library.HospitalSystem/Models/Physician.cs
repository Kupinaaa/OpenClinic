using System;

namespace Library.HospitalSystem.Models;

public class Physician
{
    public uint Id;
    public string Name;
    public uint LisenceNumber;
    public DateOnly GraduationDate;
    public List<string> Specializations;

    Physician() {
        Id = 0;
        Name = string.Empty;
        LisenceNumber = 0;
        GraduationDate = DateOnly.MinValue;

        Specializations = new List<string>();
    }
}
