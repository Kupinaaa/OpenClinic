export interface AppointmentDto {
    id: number;
    title: string;
    description: string;
    dateTimeStart: string;
    dateTimeEnd: string;
    physicianId: number;
    patientId: number;
}

export interface AppointmentCreateDto {
    title: string;
    description: string;
    dateTimeStart: string;
    dateTimeEnd: string;
    physicianId: number;
    patientId: number;
}
