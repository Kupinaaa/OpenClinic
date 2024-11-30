import React from "react";
import { AppointmentDto } from "@/types/api/appointment";
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

async function AppointmentsTable() {
    const data = await fetch("http://localhost:5222/api/appointment");
    const appointments = (await data.json()) as AppointmentDto[];
    // console.log(patients);

    const appointmentComponents = appointments.map((appointment) => (
        <TableRow key={appointment.id}>
            <TableCell>{appointment.id}</TableCell>
            <TableCell>{appointment.title}</TableCell>
            <TableCell>{appointment.description}</TableCell>
            <TableCell>{appointment.dateTimeStart}</TableCell>
            <TableCell>{appointment.dateTimeEnd}</TableCell>
            <TableCell>{appointment.patientId}</TableCell>
            <TableCell>{appointment.physicianId}</TableCell>
            <TableCell>
                <Link
                    href={`appointments/${appointment.id}`}
                    className={buttonVariants({
                        variant: "outline",
                        size: "sm",
                    })}
                >
                    <ExternalLink />
                </Link>
            </TableCell>
        </TableRow>
    ));

    return (
        <Table>
            <TableCaption>Appointments</TableCaption>
            <TableHeader>
                <TableRow>
                    <TableHead className="w-[100px]">Id</TableHead>
                    <TableHead>Title</TableHead>
                    <TableHead>Description</TableHead>
                    <TableHead>Start</TableHead>
                    <TableHead>End</TableHead>
                    <TableHead>Patient</TableHead>
                    <TableHead>Physician</TableHead>
                    <TableHead></TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>{appointmentComponents}</TableBody>
        </Table>
    );
}

export default AppointmentsTable;
