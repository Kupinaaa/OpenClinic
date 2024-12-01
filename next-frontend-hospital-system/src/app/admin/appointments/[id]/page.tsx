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
                <div className="flex">
                    <p className="text-gray-400 w-36">Description:</p>
                    <p className="">{appointmentDto.description}</p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Start:</p>
                    <p className="">
                        {new Date(
                            appointmentDto.dateTimeStart
                        ).toLocaleString()}
                    </p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">End:</p>
                    <p className="">
                        {new Date(appointmentDto.dateTimeEnd).toLocaleString()}
                    </p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Patient Id:</p>
                    <p className="">{appointmentDto.patientId}</p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Physician Id:</p>
                    <p className="">{appointmentDto.physicianId}</p>
                </div>
            </div>
        </div>
    );
}

export default displayAppointment;
