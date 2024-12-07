import React from "react";
import { AppointmentDto, AppointmentWithNavDto } from "@/types/api/appointment";
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
import BackButton from "@/components/ui/backbutton";

async function AppointmentsTable() {
    const data = await fetch("http://localhost:5222/api/appointment");
    const appointments = (await data.json()) as AppointmentWithNavDto[];
    console.log(appointments);
    // console.log(patients);

    const appointmentComponents = appointments.map((appointment) => (
        <TableRow key={appointment.id}>
            <TableCell>{appointment.id}</TableCell>
            <TableCell>{appointment.title}</TableCell>
            <TableCell>{appointment.description}</TableCell>
            <TableCell>
                {new Date(appointment.dateTimeStart).toLocaleString()}
            </TableCell>
            <TableCell>
                {new Date(appointment.dateTimeEnd).toLocaleString()}
            </TableCell>
            <TableCell>{appointment.patientNav.name}</TableCell>
            <TableCell>{appointment.physicianNav.name}</TableCell>
            <TableCell>
                <Link
                    href={`appointments/${appointment.id}`}
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
                Appointemnts
            </h1>
            <div className="max-w-4xl mx-auto p-10 border border-black rounded-lg mt-8">
                <Table>
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
                <Link
                    href="appointments/create"
                    className={cn(buttonVariants(), "mt-8")}
                >
                    Create appointment
                </Link>
            </div>
        </>
    );
}

export default AppointmentsTable;
