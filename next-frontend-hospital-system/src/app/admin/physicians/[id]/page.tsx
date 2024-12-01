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
            <div className="w-full grid gap-4 py-4">
                <div className="flex">
                    <p className="text-gray-400 w-36">License:</p>
                    <p className="">{physicianDto.lisenceNumber}</p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Specializations:</p>
                    <p className="">{physicianDto.specializations}</p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Graduation:</p>
                    <p className="">
                        {new Date(
                            physicianDto.graduationDate
                        ).toLocaleDateString()}
                    </p>
                </div>
            </div>
        </div>
    );
}

export default displayPhysician;
