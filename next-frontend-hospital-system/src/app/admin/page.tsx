import { buttonVariants } from "@/components/ui/button";
import Link from "next/link";
import React from "react";

function AdminMainPage() {
    return (
        <div className="main w-full h-full p-20">
            <div className="links-box w-full flex justify-center">
                <div className="links flex flex-col gap-2">
                    <Link
                        href={"admin/appointments"}
                        className={buttonVariants({ variant: "default" })}
                    >
                        Appointment Management
                    </Link>
                    <Link
                        href={"admin/patients"}
                        className={buttonVariants({ variant: "default" })}
                    >
                        Patient Management
                    </Link>
                    <Link
                        href={"admin/physicians"}
                        className={buttonVariants({ variant: "default" })}
                    >
                        Physician Management
                    </Link>
                </div>
            </div>
        </div>
    );
}

export default AdminMainPage;
