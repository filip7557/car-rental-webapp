import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import CompanyVehicleMaintenanceService from "../../services/CompanyVehicleMaintenanceService";

const AddMaintenanceReportPage = () => {
	const { vehicleId } = useParams();
	const [title, setTitle] = useState("");
	const [description, setDescription] = useState("");
	const navigate = useNavigate();

	const handleAddReport = async () => {
		const userId = localStorage.getItem("userId");

		const maintenanceData = {
			id: userId,
			companyVehicleId: vehicleId,
			title,
			description,
		};

		try {
			const response =
				await CompanyVehicleMaintenanceService.addMaintenanceReport(
					maintenanceData
				);
			if (response) {
				navigate(-1);
			}
		} catch (error) {
			console.error("Greška pri dodavanju izvještaja o održavanju:", error);
		}
	};

	return (
		<div>
			<h2>Add Maintenance Report</h2>
			<div>
				<label>Title</label>
				<input
					type="text"
					value={title}
					onChange={(e) => setTitle(e.target.value)}
				/>
			</div>

			<div>
				<label>Description</label>
				<textarea
					value={description}
					onChange={(e) => setDescription(e.target.value)}
				/>
			</div>

			<div>
				<button onClick={handleAddReport}>Add Report</button>
				<button onClick={() => navigate(-1)}>Cancel</button>
			</div>
		</div>
	);
};

export default AddMaintenanceReportPage;
