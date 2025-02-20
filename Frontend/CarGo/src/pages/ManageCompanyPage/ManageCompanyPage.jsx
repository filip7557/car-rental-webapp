import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import CompanyService from "../../services/CompanyService";

const ManageCompanyPage = () => {
	const [companyName, setCompanyName] = useState("");
	const [error, setError] = useState("");
	const { companyId } = useParams();
	const navigate = useNavigate();

	useEffect(() => {
		const fetchCompanyInfo = async () => {
			try {
				const response = await CompanyService.getCompanyInfoById(companyId);

				//console.log("RESPONSE", response);

				setCompanyName(response.name);
			} catch (error) {
				console.error("Greška pri dohvaćanju podataka o kompaniji:", error);
				setError("Došlo je do greške pri dohvaćanju podataka.");
			}
		};

		fetchCompanyInfo();
	}, [companyId]);

	const handleNavigate = (path) => {
		navigate(path);
	};

	return (
		<div>
			<h1>{companyName}</h1>

			<div>
				<button onClick={() => handleNavigate("/path-to-manage-vehicle")}>
					Manage Vehicle
				</button>
			</div>

			<div>
				<button onClick={() => handleNavigate("/path-to-manage-location")}>
					Manage Location
				</button>
			</div>

			<div>
				<button onClick={() => handleNavigate("/path-to-manage-managers")}>
					Manage Managers
				</button>
			</div>

			<div>
				<button onClick={() => handleNavigate("/path-to-manage-bookings")}>
					Manage Bookings
				</button>
			</div>
		</div>
	);
};

export default ManageCompanyPage;
