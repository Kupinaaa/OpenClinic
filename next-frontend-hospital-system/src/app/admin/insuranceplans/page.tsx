import React from "react";
import { PhysicianDto } from "@/types/api/physician";
import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table";

import Link from "next/link";
import { buttonVariants } from "@/components/ui/button";
import { ExternalLink } from "lucide-react";
import { cn } from "@/lib/utils";
import { InsurancePlanDto } from "@/types/api/patient";

async function PhysiciansTable() {
    const data = await fetch("http://localhost:5222/api/insuranceplan");
    const insurancePlans = (await data.json()) as InsurancePlanDto[];
    // console.log(patients);

    console.log(insurancePlans);

    const physicianComponents = insurancePlans.map((insurancePlan) => (
        <TableRow key={insurancePlan.id}>
            <TableCell>{insurancePlan.id}</TableCell>
            <TableCell>{insurancePlan.name}</TableCell>
            <TableCell>${insurancePlan.deductable}</TableCell>
            <TableCell>${insurancePlan.copay}</TableCell>
            <TableCell>{insurancePlan.coinsurancePercent * 100}%</TableCell>
            <TableCell>${insurancePlan.oopm}</TableCell>
            <TableCell>
                <Link
                    href={`insuranceplans/${insurancePlan.id}`}
                    className={buttonVariants({
                        variant: "ghost",
                        size: "sm",
                    })}
                >
                    <ExternalLink />
                </Link>
            </TableCell>
        </TableRow>
    ));

    return (
        <>
            <h1 className="font-semibold text-3xl text-center mt-8">
                Insurance Plans
            </h1>
            <div className="max-w-3xl mx-auto p-10 border border-black rounded-lg mt-8">
                <Table>
                    {/* <TableCaption>Appointments</TableCaption> */}
                    <TableHeader>
                        <TableRow>
                            <TableHead className="w-[100px]">Id</TableHead>
                            <TableHead>Name</TableHead>
                            <TableHead>Deductable</TableHead>
                            <TableHead>Copay</TableHead>
                            <TableHead>Coinsurance</TableHead>
                            <TableHead>OOPM</TableHead>
                            <TableHead></TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>{physicianComponents}</TableBody>
                </Table>
                <Link
                    className={cn(
                        buttonVariants({ variant: "default" }),
                        "mt-8"
                    )}
                    href="insuranceplans/create"
                >
                    New Insurance Plan
                </Link>
            </div>
        </>
    );
}

export default PhysiciansTable;
