import React, { useEffect, useState } from "react";
import CompanyVehicleCard from "../../../components/CompanyVehicleTable/CompanyVehicleCard";
import CompanyVehicleDropDown from "../../../components/CompanyVehicleTable/CompanyVehicleDropdownMake";
import CompanyVehicleFilter from "../../../components/CompanyVehicleTable/CompanyVehicleFilter";
import NavBar from "../../../components/NavBar/NavBar";
import companyVehicleService from "../../../services/CompanyVehicleService";

const CompanyVehiclePage = () => {
	const [vehicles, setVehicles] = useState([]);
	const [list, setList] = useState({});
	const [filters, setFilters] = useState({});
	const [pageNumber, setPageNumber] = useState(1);
	const [pageSize, setPageSize] = useState(2);
	const [totalRecords, setTotalRecords] = useState(0);
	const [error, setError] = useState(null);

	useEffect(() => {
		const fetchData = async () => {
			try {
				const data = await companyVehicleService.getCompanyVehicles(
					pageNumber,
					pageSize
				);
				debugger;
				setVehicles(data.data);
				const totalPages = Math.ceil(data.totalRecords / pageSize);
				setTotalRecords(totalPages);
			} catch (error) {
				console.error(
					"Greška pri dohvaćanju podataka o održavanju vozila:",
					error
				);
			}
		};
		fetchData();
	}, [pageNumber, pageSize]);

	useEffect(() => {
		companyVehicleService.PageNumber = 1;
	}, []);

	const handleFilterChange = (newFilters) => {
		setFilters(newFilters);
	};
	const nextPage = () => {
		if (pageNumber * pageSize < totalRecords * pageSize) {
			setPageNumber(pageNumber + 1);
		}
	};

	const prevPage = () => {
		if (pageNumber > 1) {
			setPageNumber(pageNumber - 1);
		}
	};

	// const handlePageSizeChange = (event) => {
	// 	setPageSize(Number(event.target.value));
	// 	setPageNumber(1);
	// };

	// function handlePageClick(page) {
	// 	setCurrentPage(page);
	// 	companyVehicleService.PageNumber = page;
	// 	companyVehicleService.getCompanyVehicles().then(setList);
	// }

	if (error) return <div className="error-message">{error}</div>;

	return (
		<>
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
			<div className="pagination">
				<button onClick={prevPage} disabled={pageNumber <= 1}>
					Prethodna stranica
				</button>
				<span>{`Stranica ${pageNumber} od ${totalRecords || 1}`}</span>
				<button
					onClick={nextPage}
					disabled={pageNumber * pageSize >= totalRecords * pageSize}
				>
					Sljedeća stranica
				</button>
			</div>
		</>
	);
};

export default CompanyVehiclePage;
