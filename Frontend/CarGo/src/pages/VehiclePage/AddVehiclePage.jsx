import React, { useState } from "react";
import AddVehicleForm from "../../components/VehicleForm/AddVehicleForm.jsx";
import VehicleService from "../../services/VehicleService.jsx";

const AddVehiclePage = () => {
	const [message, setMessage] = useState("");

	const handleSubmit = async (formData) => {
		try {
			await VehicleService.addVehicle(
				formData.vehicleModelId,
				formData.dailyPrice,
				formData.colorId,
				formData.plateNumber,
				formData.imageUrl,
				formData.currentLocationId,
				formData.isOperational,
				formData.isActive,
				formData.companyId
			);

			setMessage("Vozilo je uspješno dodano!");
		} catch (error) {
			console.error("Greška pri dodavanju vozila:", error);
			setMessage("Greška pri dodavanju vozila.");
		}
	};

	return (
		<div>
			<h2>Dodaj novo vozilo</h2>
			<AddVehicleForm onSubmit={handleSubmit} />
			{message && <p>{message}</p>}
		</div>
	);
};

export default AddVehiclePage;
