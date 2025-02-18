import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import CompanyVehicleMaintenanceTable from "../../components/CompanyVehicleMaintenanceForm/CompanyVehicleMaintenanceTable.jsx";
import CompanyVehicleMaintenanceService from "../../services/CompanyVehicleMaintenanceService";

const CompanyVehicleMaintenancePage = () => {
	const { vehicleId } = useParams();
	const [maintenanceRecords, setMaintenanceRecords] = useState([]);
	const [pageNumber, setPageNumber] = useState(1);
	const [pageSize, setPageSize] = useState(10);
	const [totalRecords, setTotalRecords] = useState(0);

	useEffect(() => {
		const fetchData = async () => {
			try {
				const data =
					await CompanyVehicleMaintenanceService.getVehicleMaintenance(
						vehicleId,
						pageSize,
						pageNumber
					);
				setMaintenanceRecords(data);
				const totalPages = Math.ceil(data.length / pageSize);
				setTotalRecords(totalPages);
			} catch (error) {
				console.error(
					"Greška pri dohvaćanju podataka o održavanju vozila:",
					error
				);
			}
		};

		fetchData();
	}, [vehicleId, pageNumber, pageSize]);

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

	const handlePageSizeChange = (event) => {
		setPageSize(Number(event.target.value));
		setPageNumber(1);
	};

	return (
		<div>
			<h2>Održavanje vozila</h2>

			<div>
				<label>Broj redaka po stranici:</label>
				<select value={pageSize} onChange={handlePageSizeChange}>
					<option value={10}>10</option>
					<option value={20}>20</option>
					<option value={50}>50</option>
					<option value={100}>100</option>
				</select>
			</div>

			<CompanyVehicleMaintenanceTable maintenanceRecords={maintenanceRecords} />

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
		</div>
	);
};

export default CompanyVehicleMaintenancePage;
