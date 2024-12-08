import { Button, buttonVariants } from "@/components/ui/button";
import { AppointmentDto, AppointmentWithNavDto } from "@/types/api/appointment";
import Link from "next/link";
import React from "react";
import DeleteAppointmentButton from "./DeleteAppointment";
import { notFound } from "next/navigation";

async function displayAppointment({
    params,
}: {
    params: Promise<{ id: number }>;
}) {
    const id = (await params).id;
    const data = await fetch(`http://localhost:5222/api/appointment/${id}`);
    if (!data.ok) notFound();
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
                    <p className="text-gray-400 w-36">Patient:</p>
                    <p className="">{appointmentDto.patientNav.name}</p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Physician:</p>
                    <p className="">{appointmentDto.physicianNav.name}</p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Insurance</p>
                    <p className="">
                        {appointmentDto.patientNav.insurancePlan.name}
                    </p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Total:</p>
                    <p className="">{appointmentDto.billNav.amount}</p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Total after insurance:</p>
                    <p className="">{appointmentDto.billNav.outOfPocket}</p>
                </div>
            </div>
            <div className="flex gap-2 mt-8">
                <DeleteAppointmentButton id={id} />
                <Link
                    href={`${id}/edit`}
                    className={buttonVariants({ variant: "outline" })}
                >
                    Edit
                </Link>
            </div>
        </div>
    );
}

export default displayAppointment;
