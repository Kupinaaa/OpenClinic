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
    billId: number;
    patientNav: PatientDTO;
    physicianNav: PhysicianDto;
    billNav: BillDto;
    appointmentTreatmentsNav: AppointmentTreatment[];
}

export interface BillDto {
    id: number;
    amount: number;
    outOfPocket: number;
}

export interface Treatment {
    id: number;
    name: string;
    price: number;
}

export interface AppointmentTreatment {
    id: number;
    appointmentId: number;
    treatmentId: number;
    treatment: Treatment;
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
    treatmentOptionIds: string[];
}
