"use client";
import { useEffect, useState } from "react";
import { toast } from "sonner";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form";
import {
    Command,
    CommandEmpty,
    CommandGroup,
    CommandInput,
    CommandItem,
    CommandList,
} from "@/components/ui/command";
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import { Textarea } from "@/components/ui/textarea";
import { Check, ChevronsUpDown } from "lucide-react";
import { format } from "date-fns";
import { Calendar } from "@/components/ui/calendar";
import { Calendar as CalendarIcon } from "lucide-react";
import { PatientDTO } from "@/types/api/patient";
import { PhysicianDto } from "@/types/api/physician";
import { Input } from "@/components/ui/input";
import {
    AppointmentCreateDto,
    AppointmentDto,
    Treatment,
} from "@/types/api/appointment";
import { useParams, useRouter } from "next/navigation";
import { MultiSelect } from "@/components/ui/multi-select";

interface TreatmentKeyValue {
    value: string;
    label: string;
}

const formSchema = z.object({
    title: z.string().max(100),
    description: z.string(),
    patient: z.string(),
    physician: z.string(),
    appointmentDate: z.coerce.date(),
    dateTimeOfAppointment: z.string(),
    timeLengthOfAppointment: z.string(),
    treatments: z.array(z.string()).nonempty("Please at least one item"),
});

export default function MyForm() {
    const [patients, setPatients] = useState<PatientDTO[]>([]);
    const [physicians, setPhysicians] = useState<PhysicianDto[]>([]);
    const [availability, setAvailability] = useState<string[]>([]);
    const [loading, setLoading] = useState<boolean>(false);

    const [treatmentOptions, setTreatmentOptions] = useState<
        TreatmentKeyValue[]
    >([]);

    const params = useParams<{ id: string }>();
    const router = useRouter();

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            appointmentDate: new Date(),
        },
    });

    const patientQuery = form.watch("patient");
    const physicianQuery = form.watch("physician");
    const dateOfAppointment = form.watch("appointmentDate");

    const [debouncedPatientQuery, setDebouncedPatientQuery] = useState("");
    const [debouncedPhysicianQuery, setDebouncedPhysicianQuery] = useState("");

    const fetchTreatments = async () => {
        try {
            const result = await fetch("http://localhost:5222/api/treatment");
            if (!result.ok) throw new Error("Fetch failed");
            const fetchedTreatments = (await result.json()) as Treatment[];
            setTreatmentOptions(
                fetchedTreatments.map((t) => ({
                    value: t.id.toString(),
                    label: t.name,
                }))
            );
            console.log(treatmentOptions, fetchedTreatments);
        } catch {
            console.error("There was an error fetching patients");
        }
    };

    const fetchPatients = async (query: string) => {
        try {
            const result = await fetch("http://localhost:5222/api/patient");
            if (!result.ok) throw new Error("Fetch failed");
            const fetchedPatients = (await result.json()) as PatientDTO[];
            setPatients(fetchedPatients);
        } catch {
            console.error("There was an error fetching patients");
        }
    };

    const fetchPhysicians = async (query: string) => {
        try {
            const result = await fetch("http://localhost:5222/api/physician");
            if (!result.ok) throw new Error("Fetch failed");
            const fetchedPhysicians = (await result.json()) as PhysicianDto[];
            setPhysicians(fetchedPhysicians);
        } catch {
            console.error("There was an error fetching physicians");
        }
    };

    const updateAvailability = async (
        queryPhysician: PhysicianDto,
        day: Date
    ) => {
        try {
            const result = await fetch(
                `http://localhost:5222/api/appointment/availability/${queryPhysician.id}?` +
                    new URLSearchParams({
                        day: day.toISOString(),
                        updateId: parseInt(params.id).toString(),
                    }).toString()
            );
            if (!result.ok) throw new Error("Fetch failed");
            const fetchedAvailability = (await result.json()) as string[];
            setAvailability(fetchedAvailability);
        } catch {
            console.error("There was an error fetching availability");
        }
    };

    const setAppointmentData = async (updateId: number) => {
        try {
            await fetchTreatments();
            const result = await fetch(
                `http://localhost:5222/api/appointment/${updateId}`
            );
            if (!result.ok) throw Error(await result.json());
            const data = (await result.json()) as AppointmentDto;

            form.setValue("title", data.title);
            form.setValue("description", data.description);
            form.setValue("physician", data.physicianNav.name);
            form.setValue("patient", data.patientNav.name);
            form.setValue("dateTimeOfAppointment", data.dateTimeStart);
            form.setValue("appointmentDate", new Date(data.dateTimeStart));
        } catch (e) {
            console.error(e);
        }
    };

    useEffect(() => {
        setAppointmentData(parseInt(params.id));
    }, []);

    useEffect(() => {
        const bouncer = setTimeout(() => {
            setDebouncedPatientQuery(patientQuery);
        }, 500);

        return () => {
            clearTimeout(bouncer);
        };
    }, [patientQuery]);

    useEffect(() => {
        const bouncer = setTimeout(() => {
            setDebouncedPhysicianQuery(physicianQuery);
        }, 500);

        return () => {
            clearTimeout(bouncer);
        };
    }, [physicianQuery]);

    useEffect(() => {
        fetchPatients(debouncedPatientQuery);
    }, [debouncedPatientQuery]);

    useEffect(() => {
        fetchPhysicians(debouncedPhysicianQuery);
    }, [debouncedPhysicianQuery]);

    useEffect(() => {
        const queryPhysician = physicians.find(
            (ph) => ph.name == debouncedPhysicianQuery
        );
        if (queryPhysician)
            updateAvailability(queryPhysician, dateOfAppointment);
    }, [debouncedPhysicianQuery, dateOfAppointment]);

    async function onSubmit(values: z.infer<typeof formSchema>) {
        try {
            const patient = patients.find((p) => p.name == values.patient);
            const physician = physicians.find(
                (p) => p.name == values.physician
            );
            if (!patient || !physician)
                throw new Error("No physican or patient found");

            const createAppointment: AppointmentCreateDto = {
                title: values.title,
                description: values.description,
                patientId: patient.id,
                physicianId: physician.id,
                dateTimeStart: new Date(values.dateTimeOfAppointment).toJSON(),
                dateTimeEnd: new Date(
                    new Date(values.dateTimeOfAppointment).getTime() +
                        parseInt(values.timeLengthOfAppointment) * 60000
                ).toJSON(),
                treatmentOptionIds: values.treatments,
            };

            setLoading(true);

            console.log(params.id);

            const result = await fetch(
                `http://localhost:5222/api/appointment/${params.id}`,
                {
                    headers: {
                        "Content-Type": "application/json",
                    },
                    method: "PUT",
                    body: JSON.stringify(createAppointment),
                }
            );

            setLoading(false);

            if (!result.ok) throw new Error(JSON.stringify(result));

            router.push(`/admin/appointments`);
        } catch (error) {
            console.error("Form submission error", error);
            toast.error("Failed to submit the form. Please try again.");
        }
    }

    return (
        <Form {...form}>
            <form
                onSubmit={form.handleSubmit(onSubmit)}
                className="space-y-8 max-w-3xl mx-auto py-10"
            >
                <div className="grid grid-cols-12 gap-4">
                    <div className="col-span-12">
                        <FormField
                            control={form.control}
                            name="title"
                            render={({ field }) => (
                                <FormItem className="flex flex-col">
                                    <FormLabel>Appointemnt Title</FormLabel>
                                    <FormControl>
                                        <Input
                                            placeholder="Title"
                                            type="text"
                                            {...field}
                                            value={field.value ?? ""}
                                        />
                                    </FormControl>

                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                    </div>
                    <div className="col-span-12">
                        <FormField
                            control={form.control}
                            name="description"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>
                                        Appointment Description
                                    </FormLabel>
                                    <FormControl>
                                        <Textarea
                                            placeholder="Description"
                                            className="resize-none"
                                            {...field}
                                        />
                                    </FormControl>

                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                    </div>
                    <div className="col-span-6">
                        <FormField
                            control={form.control}
                            name="patient"
                            render={({ field }) => (
                                <FormItem className="flex flex-col">
                                    <FormLabel>Patient</FormLabel>
                                    <Popover>
                                        <PopoverTrigger asChild>
                                            <FormControl>
                                                <Button
                                                    variant="outline"
                                                    role="combobox"
                                                    className={cn(
                                                        "justify-between",
                                                        !field.value &&
                                                            "text-muted-foreground"
                                                    )}
                                                >
                                                    {field.value
                                                        ? patients.find(
                                                              (patient) =>
                                                                  patient.name ===
                                                                  field.value
                                                          )?.name
                                                        : "Select patient"}
                                                    <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                                                </Button>
                                            </FormControl>
                                        </PopoverTrigger>
                                        <PopoverContent className="w-full p-0">
                                            <Command>
                                                <CommandInput placeholder="Search language..." />
                                                <CommandList>
                                                    <CommandEmpty>
                                                        No language found.
                                                    </CommandEmpty>
                                                    <CommandGroup>
                                                        {patients.map(
                                                            (patient) => (
                                                                <CommandItem
                                                                    value={
                                                                        patient.name
                                                                    }
                                                                    key={
                                                                        patient.id
                                                                    }
                                                                    onSelect={() => {
                                                                        form.setValue(
                                                                            "patient",
                                                                            patient.name
                                                                        );
                                                                    }}
                                                                >
                                                                    <Check
                                                                        className={cn(
                                                                            "mr-2 h-4 w-4",
                                                                            patient.name ===
                                                                                field.value
                                                                                ? "opacity-100"
                                                                                : "opacity-0"
                                                                        )}
                                                                    />
                                                                    {
                                                                        patient.name
                                                                    }
                                                                </CommandItem>
                                                            )
                                                        )}
                                                    </CommandGroup>
                                                </CommandList>
                                            </Command>
                                        </PopoverContent>
                                    </Popover>

                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                    </div>

                    <div className="col-span-6">
                        <FormField
                            control={form.control}
                            name="physician"
                            render={({ field }) => (
                                <FormItem className="flex flex-col">
                                    <FormLabel>Physician</FormLabel>
                                    <Popover>
                                        <PopoverTrigger asChild>
                                            <FormControl>
                                                <Button
                                                    variant="outline"
                                                    role="combobox"
                                                    className={cn(
                                                        "justify-between",
                                                        !field.value &&
                                                            "text-muted-foreground"
                                                    )}
                                                >
                                                    {field.value
                                                        ? physicians.find(
                                                              (physician) =>
                                                                  physician.name ===
                                                                  field.value
                                                          )?.name
                                                        : "Select physician"}
                                                    <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                                                </Button>
                                            </FormControl>
                                        </PopoverTrigger>
                                        <PopoverContent className="w-full p-0">
                                            <Command>
                                                <CommandInput placeholder="Search language..." />
                                                <CommandList>
                                                    <CommandEmpty>
                                                        No language found.
                                                    </CommandEmpty>
                                                    <CommandGroup>
                                                        {physicians.map(
                                                            (physician) => (
                                                                <CommandItem
                                                                    value={
                                                                        physician.name
                                                                    }
                                                                    key={
                                                                        physician.id
                                                                    }
                                                                    onSelect={() => {
                                                                        form.setValue(
                                                                            "physician",
                                                                            physician.name
                                                                        );
                                                                    }}
                                                                >
                                                                    <Check
                                                                        className={cn(
                                                                            "mr-2 h-4 w-4",
                                                                            physician.name ===
                                                                                field.value
                                                                                ? "opacity-100"
                                                                                : "opacity-0"
                                                                        )}
                                                                    />
                                                                    {
                                                                        physician.name
                                                                    }
                                                                </CommandItem>
                                                            )
                                                        )}
                                                    </CommandGroup>
                                                </CommandList>
                                            </Command>
                                        </PopoverContent>
                                    </Popover>

                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                    </div>
                </div>

                <div className="grid grid-cols-12 gap-4">
                    <div className="col-span-4">
                        <FormField
                            control={form.control}
                            name="appointmentDate"
                            render={({ field }) => (
                                <FormItem className="flex flex-col">
                                    <FormLabel>Date of Appointment</FormLabel>
                                    <Popover>
                                        <PopoverTrigger asChild>
                                            <FormControl>
                                                <Button
                                                    variant={"outline"}
                                                    className={cn(
                                                        "pl-3 text-left font-normal",
                                                        !field.value &&
                                                            "text-muted-foreground"
                                                    )}
                                                >
                                                    {field.value ? (
                                                        format(
                                                            field.value,
                                                            "PPP"
                                                        )
                                                    ) : (
                                                        <span>Pick a date</span>
                                                    )}
                                                    <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                                                </Button>
                                            </FormControl>
                                        </PopoverTrigger>
                                        <PopoverContent
                                            className="w-auto p-0"
                                            align="start"
                                        >
                                            <Calendar
                                                mode="single"
                                                selected={field.value}
                                                onSelect={field.onChange}
                                                initialFocus
                                            />
                                        </PopoverContent>
                                    </Popover>

                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                    </div>
                    <div className="col-span-4">
                        <FormField
                            control={form.control}
                            name="dateTimeOfAppointment"
                            render={({ field }) => (
                                <FormItem className="flex flex-col">
                                    <FormLabel>Time of Appointment</FormLabel>
                                    <Select
                                        onValueChange={field.onChange}
                                        defaultValue={field.value}
                                    >
                                        <FormControl>
                                            <SelectTrigger>
                                                <SelectValue placeholder="Select the time" />
                                            </SelectTrigger>
                                        </FormControl>
                                        <SelectContent>
                                            {availability.map(
                                                (dateTime: string) => {
                                                    return (
                                                        <SelectItem
                                                            key={dateTime}
                                                            value={dateTime}
                                                        >
                                                            {new Date(dateTime)
                                                                .toUTCString()
                                                                .slice(17, 30)}
                                                        </SelectItem>
                                                    );
                                                }
                                            )}
                                        </SelectContent>
                                    </Select>

                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                    </div>
                    <div className="col-span-4">
                        <FormField
                            control={form.control}
                            name="timeLengthOfAppointment"
                            render={({ field }) => (
                                <FormItem className="flex flex-col">
                                    <FormLabel>Length of Appointment</FormLabel>
                                    <Select
                                        onValueChange={field.onChange}
                                        defaultValue={field.value}
                                    >
                                        <FormControl>
                                            <SelectTrigger>
                                                <SelectValue placeholder="Select the length" />
                                            </SelectTrigger>
                                        </FormControl>
                                        <SelectContent>
                                            <SelectItem value="30">
                                                30m
                                            </SelectItem>
                                            <SelectItem value="60">
                                                1h
                                            </SelectItem>
                                            <SelectItem value="90">
                                                1.5h
                                            </SelectItem>
                                            <SelectItem value="120">
                                                2h
                                            </SelectItem>
                                        </SelectContent>
                                    </Select>

                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                    </div>
                </div>
                <FormField
                    control={form.control}
                    name="treatments"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Treatment(s)</FormLabel>
                            <FormControl>
                                <MultiSelect
                                    options={treatmentOptions}
                                    onValueChange={field.onChange}
                                    defaultValue={[]}
                                    variant={"default"}
                                />
                            </FormControl>

                            <FormMessage />
                        </FormItem>
                    )}
                />
                <Button type="submit" disabled={loading}>
                    Submit
                </Button>
            </form>
        </Form>
    );
}
