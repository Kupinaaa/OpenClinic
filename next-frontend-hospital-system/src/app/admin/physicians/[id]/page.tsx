import { Button, buttonVariants } from "@/components/ui/button";
import { PhysicianDto } from "@/types/api/physician";
import { notFound, redirect, useRouter } from "next/navigation";
import React from "react";
import DeletePhysicianButton from "./DeletePhysicianButton";
import { Toaster } from "sonner";
import Link from "next/link";

async function displayPhysician({
    params,
}: {
    params: Promise<{ id: number }>;
}) {
    const id = (await params).id;
    const data = await fetch(`http://localhost:5222/api/physician/${id}`);
    if (!data.ok) notFound();
    const physicianDto = (await data.json()) as PhysicianDto;

    return (
        <div className="h-screen w-full p-20">
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
            <div className="flex gap-2 mt-8">
                <DeletePhysicianButton id={id} />
                <Link
                    href={`${id}/edit`}
                    className={buttonVariants({ variant: "outline" })}
                >
                    Edit
                </Link>
            </div>
            <Toaster />
        </div>
    );
}

export default displayPhysician;
