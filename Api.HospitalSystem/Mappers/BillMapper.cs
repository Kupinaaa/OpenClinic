using System;
using Api.HospitalSystem.Dtos.BillDtos;
using Api.HospitalSystem.Models;

namespace Api.HospitalSystem.Mappers;

public static class BillMapper
{
    public static BillDto ToBillDto(this Bill bill)
    {
        BillDto billDto = new BillDto
        {
            Amount = bill.Amount,
            AppointmentId = bill.AppointmentId,
            Id = bill.Id,
            OutOfPocket = bill.OutOfPocket,
        };

        return billDto;
    }
}
