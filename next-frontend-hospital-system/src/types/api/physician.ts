export interface PhysicianDto {
    id: number;
    name: string;
    lisenceNumber: number;
    graduationDate: string;
    specializations: string;
}

export interface PhysicianCreateDto {
    name: string;
    lisenceNumber: number;
    graduationDate: string;
    specializations: string;
}
