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

async function PhysiciansTable() {
    const data = await fetch("http://localhost:5222/api/physician");
    const physicians = (await data.json()) as PhysicianDto[];
    // console.log(patients);

    const physicianComponents = physicians.map((physician) => (
        <TableRow key={physician.id}>
            <TableCell>{physician.id}</TableCell>
            <TableCell>{physician.name}</TableCell>
            <TableCell>{physician.lisenceNumber}</TableCell>
            <TableCell>
                {new Date(physician.graduationDate).toLocaleDateString()}
            </TableCell>
            <TableCell>{physician.specializations}</TableCell>
            <TableCell>
                <Link
                    href={`physicians/${physician.id}`}
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
        <div>
            <Table>
                {/* <TableCaption>Appointments</TableCaption> */}
                <TableHeader>
                    <TableRow>
                        <TableHead className="w-[100px]">Id</TableHead>
                        <TableHead>Name</TableHead>
                        <TableHead>Lisence #</TableHead>
                        <TableHead>Graduation</TableHead>
                        <TableHead>Specializations</TableHead>
                        <TableHead></TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>{physicianComponents}</TableBody>
            </Table>
            <Link
                className={cn(
                    buttonVariants({ variant: "default" }),
                    "float-right m-4"
                )}
                href="physicians/create"
            >
                New Physician
            </Link>
        </div>
    );
}

export default PhysiciansTable;
