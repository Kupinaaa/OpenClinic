import { Button, buttonVariants } from "@/components/ui/button";
import { PhysicianDto } from "@/types/api/physician";
import { notFound, redirect, useRouter } from "next/navigation";
import React from "react";
import { Toaster } from "sonner";
import Link from "next/link";
import { InsurancePlanDto } from "@/types/api/patient";
import { Treatment } from "@/types/api/appointment";

async function displayInsurance({
    params,
}: {
    params: Promise<{ id: number }>;
}) {
    const id = (await params).id;
    const data = await fetch(`http://localhost:5222/api/treatment/${id}`);
    console.log(data);
    // if (!data.ok) notFound();
    const treatmentDto = (await data.json()) as Treatment;

    return (
        <div className="h-screen w-full p-20">
            <h1 className="font-semibold text-5xl">
                {treatmentDto.name}
                <span className="text-gray-400 font-medium text-3xl">
                    {" "}
                    #{treatmentDto.id}
                </span>
            </h1>
            <div className="w-full grid gap-4 py-4">
                <div className="flex">
                    <p className="text-gray-400 w-36">Price:</p>
                    <p className="">${treatmentDto.price}</p>
                </div>
            </div>
            <div className="flex gap-2 mt-8">
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

export default displayInsurance;
