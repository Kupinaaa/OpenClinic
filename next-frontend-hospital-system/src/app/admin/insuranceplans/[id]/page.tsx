import { Button, buttonVariants } from "@/components/ui/button";
import { PhysicianDto } from "@/types/api/physician";
import { notFound, redirect, useRouter } from "next/navigation";
import React from "react";
import { Toaster } from "sonner";
import Link from "next/link";
import { InsurancePlanDto } from "@/types/api/patient";

async function displayInsurance({
    params,
}: {
    params: Promise<{ id: number }>;
}) {
    const id = (await params).id;
    const data = await fetch(`http://localhost:5222/api/insuranceplan/${id}`);
    console.log(data);
    // if (!data.ok) notFound();
    const insurancePlanDto = (await data.json()) as InsurancePlanDto;

    return (
        <div className="h-screen w-full p-20">
            <h1 className="font-semibold text-5xl">
                {insurancePlanDto.name}
                <span className="text-gray-400 font-medium text-3xl">
                    {" "}
                    #{insurancePlanDto.id}
                </span>
            </h1>
            <div className="w-full grid gap-4 py-4">
                <div className="flex">
                    <p className="text-gray-400 w-36">Deductable:</p>
                    <p className="">${insurancePlanDto.deductable}</p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Copay:</p>
                    <p className="">${insurancePlanDto.copay}</p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">Coinsurance:</p>
                    <p className="">
                        {insurancePlanDto.coinsurancePercent * 100}%
                    </p>
                </div>
                <div className="flex">
                    <p className="text-gray-400 w-36">OOPM:</p>
                    <p className="">${insurancePlanDto.oopm}</p>
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
