import { convertGender, convertRace, PatientDTO } from "@/types/api/patient";
import React from "react";
import DeletePatientButton from "./DeletePatientButton";
import { notFound } from "next/navigation";

async function displayPatient({ params }: { params: Promise<{ id: number }> }) {
    const id = (await params).id;
    const data = await fetch(`http://localhost:5222/api/patient/${id}`);
    if (!data.ok) notFound();
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
            <div className="w-full grid gap-4 pt-4 py-2">
                <div className="flex">
                    <p className="text-gray-400 w-36">Address:</p>
                    <p className="">{patientDto.addressLine}</p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Date of birth:</p>
                    <p className="">
                        {new Date(patientDto.dob).toLocaleDateString()}
                    </p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Gender:</p>
                    <p className="">{convertGender(patientDto.gender)}</p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Race(s):</p>
                    <p className="">
                        {patientDto.race.map(convertRace).join(", ")}
                    </p>
                </div>
            </div>
            <div className="flex gap-2 mt-8">
                <DeletePatientButton id={id} />
            </div>
        </div>
    );
}

export default displayPatient;
