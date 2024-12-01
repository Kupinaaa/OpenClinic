export interface PatientDTO {
    id: number;
    name: string;
    addressLine: string;
    dob: string;
    gender: string;
    race: string[];
}

/*public enum Race {
    NotSpecified,
    White,
    Black,
    AmericanIndianorAlaskaNative,
    Asian,
    NativeHawaiianorOtherPacificIslander,
    Other
}

public enum Gender {
    NotSpecified,
    Male,
    Female,
    Other
}*/

export function convertRace(race: string) {
    switch (race) {
        case "notSpecified":
            return "Not Specified";
        case "white":
            return "White";
        case "black":
            return "Black";
        case "americanIndianorAlaskaNative":
            return "American-Indian or Alaska native";
        case "asian":
            return "Asian";
        case "nativeHawaiianorOtherPacificIslander":
            return "Native Hawaiian or Other Pacific Islander";
        case "other":
            return "Other";
    }
}

export function convertGender(gender: string) {
    switch (gender) {
        case "notSpecified":
            return "N/S";
        case "male":
            return "Male";
        case "female":
            return "Female";
        case "other":
            return "Other";
    }
}
