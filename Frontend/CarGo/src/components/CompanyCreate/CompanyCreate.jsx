import { useState } from "react";
import axios from "axios";
import CompanyService from "../../services/CompanyService";

function CompanyCreate() {
    const [company, setCompany] = useState({ name: "", email: "" });
    const [message, setMessage] = useState("");

    const handleChange = (e) => {
        setCompany({ ...company, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await CompanyService.adminCreateCompany(company);
            setMessage(response || "Company created successfully.");
        } catch (error) {
            setMessage("Failed to create company.");
        }
    };

    return (
        <div>
            <h2>Create Company</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Company Name:</label>
                    <input
                        type="text"
                        name="name"
                        value={company.name}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Company Email:</label>
                    <input
                        type="email"
                        name="email"
                        value={company.email}
                        onChange={handleChange}
                        required
                    />
                </div>
                <button type="submit">Create Company</button>
            </form>
            {message && <p>{message}</p>}
        </div>
    );
}

export default CompanyCreate;
