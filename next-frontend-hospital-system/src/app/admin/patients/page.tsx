import React from "react";

async function AdminPatients() {
    const data = await fetch("http://localhost:5222/api/patient");
    const patients = await data.json();
    return <div className=""></div>;
}

export default AdminPatients;
