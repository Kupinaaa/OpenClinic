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
import { useRouter } from "next/navigation";
import { InsurancePlanCreateDto, InsurancePlanDto } from "@/types/api/patient";
import { Treatment, TreatmentCreate } from "@/types/api/appointment";

const formSchema = z.object({
    name: z.string().min(3),
    price: z.number().min(0).max(100000000),
});

export default function MyForm() {
    const [loading, setLoading] = useState(false);
    const router = useRouter();

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {},
    });

    async function onSubmit(values: z.infer<typeof formSchema>) {
        try {
            const treatment: TreatmentCreate = {
                name: values.name,
                price: values.price,
            };

            setLoading(true);
            const result = await fetch("http://localhost:5222/api/treatment", {
                headers: {
                    "Content-Type": "application/json",
                },
                method: "POST",
                body: JSON.stringify(treatment),
            });
            setLoading(false);

            if (!result.ok) throw new Error(await result.json());

            const resData = (await result.json()) as Treatment;

            router.push(`/admin/treatments/${resData.id}`);
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
                                <FormLabel>Name of Treatment</FormLabel>
                                <FormControl>
                                    <Input
                                        placeholder="Procedure name"
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
                        name="price"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Price</FormLabel>
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
