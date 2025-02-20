import { useState } from "react";
import CompanyService from "../../services/CompanyService";
import UserService from "../../services/UserService";

function CompanyCreate() {
    const [company, setCompany] = useState({ name: "", email: "" });
    const [message, setMessage] = useState("");
    const [searchEmail, setSearchEmail] = useState("");
    const [user, setUser] = useState(null);

    const handleChange = (e) => {
        setCompany({ ...company, [e.target.name]: e.target.value });
    };

    const handleSearch = async () => {
        try {
            const response = await UserService.getUserByEmail(searchEmail);
            if (response) {
                setUser(response);
            } else {
                setUser(null);
                setMessage("User not found.");
            }
        } catch (error) {
            setUser(null);
            setMessage("Failed to fetch user.");
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await CompanyService.adminCreateCompany({company, user});
            setMessage(response || "Company created successfully.");
        } catch (error) {
            setMessage("Failed to create company.");
        }
    };

    return (
        <div>
            <h2>Create Company</h2>
            <div>
                <label>Search User by Email:</label>
                <input
                    type="email"
                    value={searchEmail}
                    onChange={(e) => setSearchEmail(e.target.value)}
                />
                <button onClick={handleSearch}>Search</button>
            </div>
            {user && <p>User found and selected: {user.fullName}</p>}
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
