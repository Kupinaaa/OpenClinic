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
import { Treatment } from "@/types/api/appointment";

async function PhysiciansTable() {
    const data = await fetch("http://localhost:5222/api/treatment");
    const treatments = (await data.json()) as Treatment[];
    // console.log(patients);

    console.log(treatments);

    const treatmentRows = treatments.map((treatment) => (
        <TableRow key={treatment.id}>
            <TableCell>{treatment.id}</TableCell>
            <TableCell>{treatment.name}</TableCell>
            <TableCell>${treatment.price}</TableCell>
            <TableCell>
                <Link
                    href={`treatments/${treatment.id}`}
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
                Treatments
            </h1>
            <div className="max-w-3xl mx-auto p-10 border border-black rounded-lg mt-8">
                <Table>
                    {/* <TableCaption>Appointments</TableCaption> */}
                    <TableHeader>
                        <TableRow>
                            <TableHead className="w-[100px]">Id</TableHead>
                            <TableHead>Name</TableHead>
                            <TableHead>Price</TableHead>
                            <TableHead></TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>{treatmentRows}</TableBody>
                </Table>
                <Link
                    className={cn(
                        buttonVariants({ variant: "default" }),
                        "mt-8"
                    )}
                    href="treatments/create"
                >
                    New Treatment
                </Link>
            </div>
        </>
    );
}

export default PhysiciansTable;
