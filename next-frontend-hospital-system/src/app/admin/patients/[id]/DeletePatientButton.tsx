"use client";
import { Button } from "@/components/ui/button";
import { useRouter } from "next/navigation";
import React from "react";
import { toast } from "sonner";

interface DeleteProps {
    id: number;
}

const DeletePatientButton: React.FC<DeleteProps> = ({ id }) => {
    const router = useRouter();
    const handleDelete = async () => {
        try {
            const result = await fetch(
                `http://localhost:5222/api/patient/${id}`,
                { method: "DELETE" }
            );

            if (result.ok) router.push("/admin/patients");

            throw new Error(await result.json());
        } catch (error) {
            console.log(error);
        }
    };
    return (
        <Button variant={"destructive"} onClick={handleDelete}>
            Delete Patient
        </Button>
    );
};

export default DeletePatientButton;
