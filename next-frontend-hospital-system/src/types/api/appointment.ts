export interface AppointmentDto {
    id: number;
    title: string;
    description: string;
    dateTimeStart: string;
    dateTimeEnd: string;
    physicianId: number;
    patientId: number;
}
