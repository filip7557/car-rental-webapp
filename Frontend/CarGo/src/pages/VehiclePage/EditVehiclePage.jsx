import React, { useState } from "react";
import { useParams } from "react-router-dom";
import EditVehicleForm from "../../components/VehicleForm/EditVehicleForm.jsx";
import VehicleService from "../../services/VehicleService.jsx";

const EditVehiclePage = () => {
	const { vehicleId } = useParams();
	const [message, setMessage] = useState("");

	const handleSubmit = async (formData) => {
		if (
			!formData.vehicleModelId ||
			!formData.colorId ||
			!formData.plateNumber ||
			!formData.currentLocationId
		) {
			setMessage("Sva polja moraju biti ispunjena.");
			return;
		}

		console.log("Data to send:", formData);

		const updatedData = {
			vehicleModelId: formData.vehicleModelId || "",
			dailyPrice: formData.dailyPrice || 0,
			colorId: formData.colorId || "",
			plateNumber: formData.plateNumber || "",
			imageUrl: formData.imageUrl || "",
			currentLocationId: formData.currentLocationId || "",
			enginePower: formData.enginePower || "",

			isActive: formData.isActive ?? true,
		};

		try {
			const result = await VehicleService.updateVehicle(
				vehicleId,
				updatedData.vehicleModelId,
				updatedData.dailyPrice,
				updatedData.colorId,
				updatedData.plateNumber,
				updatedData.imageUrl,
				updatedData.currentLocationId,
				updatedData.isActive
			);

			if (result.success) {
				setMessage("Vozilo je uspješno ažurirano!");
			} else {
				setMessage(result.message || "Greška pri ažuriranju vozila.");
			}
		} catch (error) {
			console.error("Greška pri ažuriranju vozila:", error);
			setMessage("Greška pri ažuriranju vozila.");
		}
	};

	return (
		<div>
			<h2>Uredi vozilo</h2>
			<EditVehicleForm onSubmit={handleSubmit} />
			{message && <p>{message}</p>}
		</div>
	);
};

export default EditVehiclePage;
