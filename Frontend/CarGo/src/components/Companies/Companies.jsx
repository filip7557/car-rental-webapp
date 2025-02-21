import React, { useEffect, useState } from "react";
import CompanyService from "../../services/CompanyService";
import UserService from "../../services/UserService";

function Companies() {
    const [companies, setCompanies] = useState([]);
    const [creatorNames, setCreatorNames] = useState([]);
    const [error, setError] = useState(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        fetchCompanies();
    }, []);

    const fetchCompanies = async () => {
        setIsLoading(true);
        setError(null);
        try {
            const data = await CompanyService.getAllCompanies();
            setCompanies(data);
            const creatorNameMap = {};
            await Promise.all(
                data.map(async (company) => {
                    if (company.createdByUserId) {
                        const user = await UserService.getUser(company.createdByUserId);
                        creatorNameMap[company.id] = user?.fullName || "Unknown";
                    }
                })
            );
            setCreatorNames(creatorNameMap);
        } catch (err) {
            setError("Failed to fetch companies.");
        } finally {
            setIsLoading(false);
        }
    };

    const handleDelete = async (id) => {
        const confirmDelete = window.confirm("Are you sure you want to disable this company?");
        if (!confirmDelete) return;

        try {
            await CompanyService.changeCompanyStatus(id, false);
            fetchCompanies();
        } catch (err) {
            setError("Failed to disable company.");
        }
    };

    if (isLoading) return <div>Loading companies...</div>;
    if (error) return <div className="error-message">{error}</div>;

    return (
        <div>
            <h1>Companies</h1>
            {companies.length > 0 ? (
                <table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Email</th>
                            <th>Created By</th>
                            <th>Date Created</th>
                            <th>Is Active</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {companies.map(company => (
                            <tr key={company.id}>
                                <td>{company.name}</td>
                                <td>{company.email}</td>
                                <td>{creatorNames[company.id] || "Loading..."}</td>
                                <td>{company.dateCreated}</td>
                                <td>{company.isActive ? "Yes" : "No"}</td>
                                <td>
                                    {company.isActive && (
                                        <button onClick={() => handleDelete(company.id)}>
                                            Disable
                                        </button>
                                    )}
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            ) : (
                <p>No companies found.</p>
            )}
        </div>
    );
}

export default Companies;
