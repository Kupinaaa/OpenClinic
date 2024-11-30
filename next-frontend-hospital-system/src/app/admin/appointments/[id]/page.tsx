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
    return (
        <div className="h-full w-full p-20">
            <h1 className="font-semibold text-5xl">
                {appointmentDto.title}
                <span className="text-gray-400 font-medium text-3xl">
                    {" "}
                    #{appointmentDto.id}
                </span>
            </h1>
            <div className="w-full lg:w-1/2 grid gap-4 pt-4 py-2">
                <div className="grid grid-cols-4 items-center gap-4">
                    <p className="text-gray-400">Description:</p>
                    <p className="col-span-3">{appointmentDto.description}</p>
                </div>
                <div className="grid grid-cols-4 items-center gap-4">
                    <p className="text-gray-400">Start:</p>
                    <p className="col-span-3">{appointmentDto.dateTimeStart}</p>
                </div>
                <div className="grid grid-cols-4 items-center gap-4">
                    <p className="text-gray-400">End:</p>
                    <p className="col-span-3">{appointmentDto.dateTimeEnd}</p>
                </div>
                <div className="grid grid-cols-4 items-center gap-4">
                    <p className="text-gray-400">Patient Id:</p>
                    <p className="col-span-3">{appointmentDto.patientId}</p>
                </div>
                <div className="grid grid-cols-4 items-center gap-4">
                    <p className="text-gray-400">Physician Id:</p>
                    <p className="col-span-3">{appointmentDto.physicianId}</p>
                </div>
            </div>
        </div>
    );
}

export default displayAppointment;
