import { PhysicianDto } from "@/types/api/physician";
import React from "react";

async function displayPhysician({
    params,
}: {
    params: Promise<{ id: number }>;
}) {
    const id = (await params).id;
    const data = await fetch(`http://localhost:5222/api/physician/${id}`);
    const physicianDto = (await data.json()) as PhysicianDto;
    return (
        <div className="h-full w-full p-20">
            <h1 className="font-semibold text-5xl">
                {physicianDto.name}
                <span className="text-gray-400 font-medium text-3xl">
                    {" "}
                    #{physicianDto.id}
                </span>
            </h1>
            <div className="w-full lg:w-1/2 grid gap-4 pt-4 py-2">
                <div className="grid grid-cols-4 items-center gap-4">
                    <p className="text-gray-400">Lisence:</p>
                    <p className="col-span-3">{physicianDto.lisenceNumber}</p>
                </div>
                <div className="grid grid-cols-4 items-center gap-4">
                    <p className="text-gray-400">Specializations:</p>
                    <p className="col-span-3">{physicianDto.specializations}</p>
                </div>
                <div className="grid grid-cols-4 items-center gap-4">
                    <p className="text-gray-400">Graduation:</p>
                    <p className="col-span-3">{physicianDto.graduationDate}</p>
                </div>
            </div>
        </div>
    );
}

export default displayPhysician;
