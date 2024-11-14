import React from "react";
import { PatientDTO } from "@/types/api/patient";
import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table";

async function AdminPatients() {
    const data = await fetch("http://localhost:5222/api/patient");
    const patients = (await data.json()) as PatientDTO[];
    // console.log(patients);
    const patientComponents = patients.map((patient) => (
        <TableRow key={patient.id}>
            <TableCell>{patient.id}</TableCell>
            <TableCell>{patient.name}</TableCell>
            <TableCell>{patient.addressLine}</TableCell>
            <TableCell>{patient.dob}</TableCell>
            <TableCell>{patient.gender}</TableCell>
            <TableCell>{patient.race}</TableCell>
        </TableRow>
    ));
    return (
        <Table>
            <TableCaption>Patients</TableCaption>
            <TableHeader>
                <TableRow>
                    <TableHead className="w-[100px]">Id</TableHead>
                    <TableHead>Name</TableHead>
                    <TableHead>Adress</TableHead>
                    <TableHead>DOB</TableHead>
                    <TableHead>Gender</TableHead>
                    <TableHead>Race</TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>{patientComponents}</TableBody>
        </Table>
    );
}

export default AdminPatients;
