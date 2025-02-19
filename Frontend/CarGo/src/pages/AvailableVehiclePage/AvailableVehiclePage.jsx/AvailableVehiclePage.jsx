import React, { useEffect, useState } from "react";
import CompanyVehicleCard from "../../../components/CompanyVehicleTable/CompanyVehicleCard";
import CompanyVehicleDropDown from "../../../components/CompanyVehicleTable/CompanyVehicleDropdownMake";
import CompanyVehicleFilter from "../../../components/CompanyVehicleTable/CompanyVehicleFilter";
import NavBar from "../../../components/NavBar/NavBar";
import companyVehicleService from "../../../services/CompanyVehicleService";

const CompanyVehiclePage = () => {
	const [vehicles, setVehicles] = useState([]);
	const [filters, setFilters] = useState({});
	const [error, setError] = useState(null);

	useEffect(() => {
		companyVehicleService.getCompanyVehicles().then(setVehicles);
	}, []);

	const handleFilterChange = (newFilters) => {
		setFilters(newFilters);
	};

	if (error) return <div className="error-message">{error}</div>;

	return (
		<div className="company-vehicle-page">
			<NavBar />
			<h1>Company Vehicles</h1>
			<CompanyVehicleFilter onFilterChange={handleFilterChange} />
			<CompanyVehicleDropDown setFilters={setFilters} setList={setVehicles} />
			<div className="company-vehicle-list">
				{vehicles.length > 0 ? (
					vehicles.map((vehicle) => (
						<CompanyVehicleCard
							key={vehicle.companyVehicleId}
							vehicle={vehicle}
						/>
					))
				) : (
					<p>No vehicles available.</p>
				)}
			</div>
		</div>
	);
};

export default CompanyVehiclePage;
