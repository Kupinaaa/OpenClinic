"use client";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { Toaster } from "@/components/ui/sonner";
import { toast } from "sonner";
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
import { PhysicianCreateDto, PhysicianDto } from "@/types/api/physician";
import { useParams, useRouter } from "next/navigation";

const formSchema = z.object({
    physicianName: z.string().min(3),
    licenseNumber: z.number().min(10000).max(100000000000),
    physicianSpecializatoins: z.string().optional(),
    physicianDateOfGrad: z.coerce.date(),
});

export default function MyForm() {
    const [loading, setLoading] = useState(false);
    const router = useRouter();

    const params = useParams();

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            physicianName: "",
            physicianSpecializatoins: "",
            physicianDateOfGrad: new Date(),
        },
    });

    async function onSubmit(values: z.infer<typeof formSchema>) {
        try {
            console.log(values);
            const PhysicianCreateDto: PhysicianCreateDto = {
                name: values.physicianName,
                lisenceNumber: values.licenseNumber,
                specializations: values.physicianSpecializatoins ?? "",
                graduationDate: values.physicianDateOfGrad.toISOString(),
            };

            setLoading(true);
            const result = await fetch(
                `http://localhost:5222/api/physician/${params.id}`,
                {
                    headers: {
                        "Content-Type": "application/json",
                    },
                    method: "PUT",
                    body: JSON.stringify(PhysicianCreateDto),
                }
            );
            setLoading(false);

            if (!result.ok) throw new Error(await result.json());

            const resData = (await result.json()) as PhysicianDto;

            router.push(`/admin/physicians/${params.id}`);
        } catch (error) {
            console.error("Form submission error", error);
            toast.error("Failed to submit the form. Please try again");
        }
    }

    const fetchValues = async () => {
        const resultPhysician = await fetch(
            `http://localhost:5222/api/physician/${params.id}`
        );
        if (!resultPhysician.ok) throw Error(await resultPhysician.json());
        const dataPhysician = (await resultPhysician.json()) as PhysicianDto;

        form.setValue("physicianName", dataPhysician.name);
        form.setValue(
            "physicianSpecializatoins",
            dataPhysician.specializations
        );
        form.setValue("licenseNumber", dataPhysician.lisenceNumber);
        form.setValue(
            "physicianDateOfGrad",
            new Date(dataPhysician.graduationDate)
        );
    };

    useEffect(() => {
        fetchValues();
    }, []);

    return (
        <>
            <Form {...form}>
                <form
                    onSubmit={form.handleSubmit(onSubmit)}
                    className="space-y-8 max-w-3xl p-10 mx-auto py-10"
                >
                    <FormField
                        control={form.control}
                        name="physicianName"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Name</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="First Last"
                                        type="text"
                                        {...field}
                                    />
                                </FormControl>

                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="licenseNumber"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>License #</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="12341234"
                                        type="number"
                                        {...field}
                                        value={field.value ?? ""}
                                        onChange={(e) => {
                                            field.onChange(
                                                e.target.valueAsNumber ||
                                                    undefined
                                            );
                                        }}
                                    />
                                </FormControl>

                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="physicianSpecializatoins"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Specializations</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="Physician specializations"
                                        type="text"
                                        {...field}
                                    />
                                </FormControl>

                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="physicianDateOfGrad"
                        render={({ field }) => (
                            <FormItem className="flex flex-col">
                                <FormLabel>Date of graduation</FormLabel>
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
                    <Button type="submit" disabled={loading}>
                        Submit
                    </Button>
                </form>
            </Form>
            <Toaster />
        </>
    );
}
