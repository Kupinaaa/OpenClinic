import { PatientDTO } from "./patient";
import { PhysicianDto } from "./physician";

export interface AppointmentDto {
    id: number;
    title: string;
    description: string;
    dateTimeStart: string;
    dateTimeEnd: string;
    physicianId: number;
    patientId: number;
}

export interface AppointmentWithNavDto {
    id: number;
    title: string;
    description: string;
    dateTimeStart: string;
    dateTimeEnd: string;
    physicianId: number;
    patientId: number;
    patientNav: PatientDTO;
    physicianNav: PhysicianDto;
}

export interface AppointmentCreateDto {
    title: string;
    description: string;
    dateTimeStart: string;
    dateTimeEnd: string;
    physicianId: number;
    patientId: number;
}
