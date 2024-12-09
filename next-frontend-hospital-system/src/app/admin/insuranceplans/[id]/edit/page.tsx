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
import { InsurancePlanCreateDto, InsurancePlanDto } from "@/types/api/patient";

const formSchema = z.object({
    name: z.string().min(3),
    deductable: z.number().min(0).max(10000000),
    copay: z.number().min(0).max(10000000),
    coinsurance: z.number().min(0).max(100),
    oopm: z.number().min(0).max(10000000),
});

export default function MyForm() {
    const [loading, setLoading] = useState(false);
    const router = useRouter();

    const params = useParams<{ id: string }>();

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {},
    });

    async function onSubmit(values: z.infer<typeof formSchema>) {
        try {
            const InsurancePlan: InsurancePlanCreateDto = {
                name: values.name,
                deductable: values.deductable,
                coinsurancePercent: values.coinsurance / 100,
                copay: values.copay,
                oopm: values.oopm,
            };

            setLoading(true);
            const result = await fetch(
                `http://localhost:5222/api/insuranceplan/${params.id.toString()}`,
                {
                    headers: {
                        "Content-Type": "application/json",
                    },
                    method: "PUT",
                    body: JSON.stringify(InsurancePlan),
                }
            );
            setLoading(false);

            if (!result.ok) throw new Error(await result.json());

            const resData = (await result.json()) as PhysicianDto;

            router.push(`/admin/insuranceplans/${resData.id}`);
        } catch (error) {
            console.error("Form submission error", error);
            toast.error("Failed to submit the form. Please try again");
        }
    }

    useEffect(() => {
        console.log("Form State: ", form.formState.errors);
    }, [form.formState]);

    return (
        <>
            <Form {...form}>
                <form
                    onSubmit={form.handleSubmit(onSubmit)}
                    className="space-y-8 max-w-3xl p-10 mx-auto py-10"
                >
                    <FormField
                        control={form.control}
                        name="name"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Name of Insurance</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="Insurance name"
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
                        name="deductable"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Deductable</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder=""
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
                        name="copay"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Copay</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder=""
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
                        name="coinsurance"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Coinsurance</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder=""
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
                        name="oopm"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>OOPM</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder=""
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

                    <Button type="submit" disabled={loading}>
                        Submit
                    </Button>
                </form>
            </Form>
            <Toaster />
        </>
    );
}
