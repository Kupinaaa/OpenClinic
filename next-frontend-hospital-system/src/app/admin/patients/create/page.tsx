"use client";
import { useEffect, useState } from "react";
import { toast, Toaster } from "sonner";
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
import { Input } from "@/components/ui/input";
import { format } from "date-fns";
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover";
import { Calendar } from "@/components/ui/calendar";
import { Calendar as CalendarIcon } from "lucide-react";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";

import { MultiSelect } from "@/components/ui/multi-select";
import { InsurancePlanDto, PatientDTO } from "@/types/api/patient";
import { useRouter } from "next/navigation";

const formSchema = z.object({
    name: z.string().min(3),
    addressLine: z.string().min(3),
    dob: z.coerce.date(),
    gender: z.string().nonempty("Please select your gender"),
    race: z.array(z.string()).nonempty("Please at least one item"),
    insurancePlanId: z.string().nonempty("Please select your insurance"),
});

export default function MyForm() {
    /*
    public enum Race {
        NotSpecified,
        White,
        Black,
        AmericanIndianorAlaskaNative,
        Asian,
        NativeHawaiianorOtherPacificIslander,
        Other
    }
    */
    const raceValuesList = [
        { value: "white", label: "White" },
        { value: "black", label: "Black" },
        {
            value: "americanIndianorAlaskaNative",
            label: "American-Indian or Alaska native",
        },
        { value: "asian", label: "Asian" },
        {
            value: "NativeHawaiianorOtherPacificIslander",
            label: "Native Hawaiian or Other pacific islander",
        },
        { value: "other", label: "Other" },
    ];

    const router = useRouter();
    const [loading, setLoading] = useState(false);
    const [insurancePlans, setInsurancePlans] = useState<InsurancePlanDto[]>(
        []
    );

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            dob: new Date(),
        },
    });

    async function onSubmit(values: z.infer<typeof formSchema>) {
        try {
            setLoading(true);
            const result = await fetch("http://localhost:5222/api/patient", {
                headers: {
                    "Content-Type": "application/json",
                },
                method: "POST",
                body: JSON.stringify(values),
            });

            if (!result.ok) throw new Error(await result.json());

            const resData = (await result.json()) as PatientDTO;

            router.push(`/admin/patients/${resData.id}`);
        } catch (error) {
            setLoading(false);
            console.error("Form submission error", error);
            toast.error("Failed to submit the form. Please try again.");
        }
    }

    const fetchInsurances = async () => {
        try {
            const result = await fetch(
                "http://localhost:5222/api/insuranceplan"
            );
            if (!result.ok) throw new Error("Fetch failed");
            const fetchedInsurances =
                (await result.json()) as InsurancePlanDto[];
            setInsurancePlans(fetchedInsurances);
        } catch {
            console.error("There was an error fetching patients");
        }
    };

    useEffect(() => {
        fetchInsurances();
    }, []);

    return (
        <>
            <Form {...form}>
                <form
                    onSubmit={form.handleSubmit(onSubmit)}
                    className="space-y-8 max-w-3xl mx-auto py-10"
                >
                    <FormField
                        control={form.control}
                        name="name"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Name</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="First Last"
                                        type="text"
                                        {...field}
                                        value={field.value ?? ""}
                                    />
                                </FormControl>

                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="addressLine"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Address</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="123 Street St., Tallahassee, FL"
                                        type="text"
                                        {...field}
                                        value={field.value ?? ""}
                                    />
                                </FormControl>

                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="dob"
                        render={({ field }) => (
                            <FormItem className="flex flex-col">
                                <FormLabel>Date of birth</FormLabel>
                                <Popover>
                                    <PopoverTrigger asChild>
                                        <FormControl>
                                            <Button
                                                variant={"outline"}
                                                className={cn(
                                                    "w-[240px] pl-3 text-left font-normal",
                                                    !field.value &&
                                                        "text-muted-foreground"
                                                )}
                                            >
                                                {field.value ? (
                                                    format(field.value, "PPP")
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

                    <FormField
                        control={form.control}
                        name="gender"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Gender</FormLabel>
                                <Select
                                    onValueChange={field.onChange}
                                    defaultValue={field.value}
                                >
                                    <FormControl>
                                        <SelectTrigger>
                                            <SelectValue placeholder="" />
                                        </SelectTrigger>
                                    </FormControl>
                                    <SelectContent>
                                        <SelectItem value="Male">
                                            Male
                                        </SelectItem>
                                        <SelectItem value="Female">
                                            Female
                                        </SelectItem>
                                        <SelectItem value="Other">
                                            Other
                                        </SelectItem>
                                    </SelectContent>
                                </Select>

                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="race"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Race(s)</FormLabel>
                                <FormControl>
                                    <MultiSelect
                                        options={raceValuesList}
                                        onValueChange={field.onChange}
                                        defaultValue={[]}
                                        variant={"default"}
                                    />
                                </FormControl>

                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="insurancePlanId"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Insurance Plan</FormLabel>
                                <Select
                                    onValueChange={field.onChange}
                                    defaultValue={field.value}
                                >
                                    <FormControl>
                                        <SelectTrigger>
                                            <SelectValue placeholder="" />
                                        </SelectTrigger>
                                    </FormControl>
                                    <SelectContent>
                                        {insurancePlans.map((insurance) => (
                                            <SelectItem
                                                value={insurance.id.toString()}
                                                key={insurance.id}
                                            >
                                                {insurance.name}
                                            </SelectItem>
                                        ))}
                                    </SelectContent>
                                </Select>

                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <Button type="submit" disabled={loading}>
                        Submit
                    </Button>
                </form>
            </Form>
            <Toaster />
        </>
    );
}
