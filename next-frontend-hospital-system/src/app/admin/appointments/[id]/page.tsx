import { AppointmentDto } from "@/types/api/appointment";
import React from "react";

async function displayAppointment({
    params,
}: {
    params: Promise<{ id: number }>;
}) {
    const id = (await params).id;
    const data = await fetch(`http://localhost:5222/api/appointment/${id}`);
    const appointmentDto = (await data.json()) as AppointmentDto;
    return <div>{JSON.stringify(appointmentDto)}</div>;
}

export default displayAppointment;
