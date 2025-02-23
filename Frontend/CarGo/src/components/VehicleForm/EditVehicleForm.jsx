import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import VehicleService from "../../services/VehicleService.jsx";

const EditVehicleForm = ({ onSubmit }) => {
	const { vehicleId } = useParams();
	const [formData, setFormData] = useState({
		companyId: "",
		vehicleModelId: "",
		dailyPrice: "",
		colorId: "",
		plateNumber: "",
		imageUrl: "",
		currentLocationId: "",
		isOperational: true,
		isActive: true,
	});

	const [vehicleModels, setVehicleModels] = useState([]);
	const [carColors, setCarColors] = useState([]);
	const [locations, setLocations] = useState([]);
	const [dropdownsLoaded, setDropdownsLoaded] = useState(false);

	useEffect(() => {
		const fetchDropdownData = async () => {
			try {
				const models = await VehicleService.getVehicleModels();
				const colors = await VehicleService.getCarColors();
				const locs = await VehicleService.getLocations();

				setVehicleModels(models || []);
				setCarColors(colors || []);
				setLocations(locs || []);
				setDropdownsLoaded(true);
			} catch (error) {
				console.error("Greška pri dohvaćanju podataka za dropdown:", error);
			}
		};
		fetchDropdownData();
	}, []);

	useEffect(() => {
		if (!dropdownsLoaded) return;
		const fetchVehicleData = async () => {
			try {
				const vehicleResponse = await VehicleService.getVehicleById(vehicleId);

				const currentVehicle = {
					companyId: vehicleResponse.companyId,
					vehicleModelId: vehicleResponse.vehicleModelId || "",
					dailyPrice: vehicleResponse.dailyPrice || "",
					colorId: vehicleResponse.colorId || "",
					plateNumber: vehicleResponse.plateNumber || "",
					imageUrl: vehicleResponse.imageUrl || "",
					currentLocationId: vehicleResponse.currentLocationId || "",
					isActive: vehicleResponse.isActive ?? true,
				};

				setFormData((prevData) => ({ ...prevData, ...currentVehicle }));
			} catch (error) {
				console.error("Greška pri dohvaćanju podataka o vozilu:", error);
			}
		};
		fetchVehicleData();
	}, [vehicleId, dropdownsLoaded]);

	const handleChange = (e) => {
		const { name, value, type, checked } = e.target;
		setFormData((prevData) => ({
			...prevData,
			[name]: type === "checkbox" ? checked : value.trim() || undefined,
		}));
	};

	const handleSubmit = (e) => {
		e.preventDefault();

		console.log("Data to send:", formData);

		onSubmit(formData);
	};

	return (
		<form onSubmit={handleSubmit}>
			<label>Model vozila:</label>
			<select
				name="vehicleModelId"
				value={formData.vehicleModelId}
				onChange={handleChange}
				required
			>
				<option value="">Odaberi model vozila</option>
				{vehicleModels.map((model) => (
					<option key={model.id} value={model.id}>
						{model.name}
					</option>
				))}
			</select>

			<label>Daily Price:</label>
			<input
				type="number"
				step="0.01"
				name="dailyPrice"
				value={formData.dailyPrice}
				onChange={handleChange}
				required
			/>

			<label>Boja vozila:</label>
			<select
				name="colorId"
				value={formData.colorId}
				onChange={handleChange}
				required
			>
				<option value="">Odaberi boju</option>
				{carColors.map((color) => (
					<option key={color.id} value={color.id}>
						{color.name}
					</option>
				))}
			</select>

			<label>Plate Number:</label>
			<input
				type="text"
				name="plateNumber"
				value={formData.plateNumber}
				onChange={handleChange}
				required
			/>

			<label>Lokacija:</label>
			<select
				name="currentLocationId"
				value={formData.currentLocationId}
				onChange={handleChange}
				required
			>
				<option value="">Odaberi lokaciju</option>
				{locations.map((location) => (
					<option key={location.id} value={location.id}>
						{location.city}
					</option>
				))}
			</select>

			<label>
				Is Active:
				<input
					type="checkbox"
					name="isActive"
					checked={formData.isActive}
					onChange={handleChange}
				/>
			</label>

			<button type="submit">Uredi vozilo</button>
		</form>
	);
};

export default EditVehicleForm;
