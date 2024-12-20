import React from "react";
import { convertGender, convertRace, PatientDTO } from "@/types/api/patient";
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

async function PatientsTable() {
    const data = await fetch("http://localhost:5222/api/patient");
    const patients = (await data.json()) as PatientDTO[];
    // console.log(patients);

    const patientComponents = patients.map((patient) => (
        <TableRow key={patient.id}>
            <TableCell>{patient.id}</TableCell>
            <TableCell>{patient.name}</TableCell>
            <TableCell>{patient.addressLine}</TableCell>
            <TableCell>{new Date(patient.dob).toLocaleDateString()}</TableCell>
            <TableCell>{convertGender(patient.gender)}</TableCell>
            <TableCell>{patient.race.map(convertRace).join(", ")}</TableCell>
            <TableCell>
                <Link
                    href={`patients/${patient.id}`}
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
                Patients
            </h1>
            <div className="max-w-3xl mx-auto p-10 border border-black rounded-lg mt-8">
                <Table>
                    <TableHeader>
                        <TableRow>
                            <TableHead className="w-[100px]">Id</TableHead>
                            <TableHead>Name</TableHead>
                            <TableHead>Address</TableHead>
                            <TableHead className="">DOB</TableHead>
                            <TableHead className="">Gender</TableHead>
                            <TableHead className="w-36">Race</TableHead>
                            <TableHead className="w-[100px]"></TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>{patientComponents}</TableBody>
                </Table>
                <Link
                    href="patients/create"
                    className={cn(buttonVariants(), "mt-8")}
                >
                    Create patient
                </Link>
            </div>
        </>
    );
}

export default PatientsTable;
