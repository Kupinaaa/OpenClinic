import Image from "next/image";
import Link from "next/link";
import { buttonVariants } from "@/components/ui/button";

export default function Home() {
    return (
        <div className="HomePage w-full h-full p-20">
            <div className="links w-full flex justify-center">
                <Link
                    className={buttonVariants({ variant: "default" })}
                    href={"/admin"}
                >
                    Admin Panel
                </Link>
            </div>
        </div>
    );
}
