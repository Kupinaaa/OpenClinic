"use client";
import { usePathname, useRouter } from "next/navigation";
import { Button } from "./button";

function BackButton({
    className,
    children,
}: React.PropsWithChildren<{
    className?: string;
}>) {
    const router = useRouter();
    const pathname = usePathname();

    const goToParent = () => {
        const parentPath =
            pathname.substring(0, pathname.lastIndexOf("/")) || "/";
        router.push(parentPath);
    };

    return (
        <Button className={className} onClick={goToParent}>
            {children}
        </Button>
    );
}

export default BackButton;
