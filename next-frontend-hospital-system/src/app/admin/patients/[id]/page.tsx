import { PatientDTO } from "@/types/api/patient";
import React from "react";

async function displayPatient({ params }: { params: Promise<{ id: number }> }) {
    const id = (await params).id;
    const data = await fetch(`http://localhost:5222/api/patient/${id}`);
    const patientDto = (await data.json()) as PatientDTO;
    return (
        <div className="h-full w-full p-20">
            <h1 className="font-semibold text-5xl">
                {patientDto.name}
                <span className="text-gray-400 font-medium text-3xl">
                    {" "}
                    #{patientDto.id}
                </span>
            </h1>
            <div className="w-full lg:w-1/2 grid gap-4 pt-4 py-2">
                <div className="grid grid-cols-4 items-center gap-4">
                    <p className="text-gray-400">Address:</p>
                    <p className="col-span-3">{patientDto.addressLine}</p>
                </div>
                <div className="grid grid-cols-4 items-center gap-4">
                    <p className="text-gray-400">Date of birth:</p>
                    <p className="col-span-3">{patientDto.dob}</p>
                </div>
                <div className="grid grid-cols-4 items-center gap-4">
                    <p className="text-gray-400">Gender:</p>
                    <p className="col-span-3">{patientDto.gender}</p>
                </div>
                <div className="grid grid-cols-4 items-center gap-4">
                    <p className="text-gray-400">Race(s):</p>
                    <p className="col-span-3">{patientDto.race}</p>
                </div>
            </div>
        </div>
    );
}

export default displayPatient;
