import React, { useEffect, useState } from "react";
import VehicleService from "../../services/VehicleService.jsx";

const AddVehicleForm = ({ onSubmit }) => {
	const userRole = localStorage.getItem("role") || "";
	const userCompanyId = localStorage.getItem("companyId") || "";

	const [formData, setFormData] = useState({
		companyId: userRole === "Administrator" ? "" : userCompanyId,
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
	const [companies, setCompanies] = useState([]);

	useEffect(() => {
		const fetchData = async () => {
			try {
				const models = await VehicleService.getVehicleModels();
				const colors = await VehicleService.getCarColors();
				const locs = await VehicleService.getLocations();

				setVehicleModels(models || []);
				setCarColors(colors || []);
				setLocations(locs || []);

				if (userRole === "Administrator") {
					const companyList = await VehicleService.getCompanies();
					setCompanies(companyList || []);
				}
			} catch (error) {
				console.error("Greška pri dohvaćanju podataka:", error);
			}
		};

		fetchData();
	}, [userRole]);

	const handleChange = (e) => {
		const { name, value, type, checked } = e.target;

		setFormData((prevData) => ({
			...prevData,
			[name]: type === "checkbox" ? checked : value.trim() || undefined,
		}));
	};

	const handleReset = () => {
		setFormData({
			companyId: userRole === "Administrator" ? "" : userCompanyId,
			vehicleModelId: "",
			dailyPrice: "",
			colorId: "",
			plateNumber: "",
			imageUrl: "",
			currentLocationId: "",
			isOperational: true,
			isActive: true,
		});
	};

	const handleSubmit = (e) => {
		e.preventDefault();
		onSubmit(formData);
	};

	return (
		<form onSubmit={handleSubmit}>
			{userRole === "Administrator" && companies.length > 0 && (
				<>
					<label>Kompanija:</label>
					<select
						name="companyId"
						value={formData.companyId}
						onChange={handleChange}
						required
					>
						<option value="">Odaberi kompaniju</option>
						{companies.map((company) => (
							<option key={company.id} value={company.id}>
								{company.name}
							</option>
						))}
					</select>
				</>
			)}

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
				placeholder="Daily Price"
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
				placeholder="Plate Number"
				value={formData.plateNumber}
				onChange={handleChange}
				required
			/>

			<label>Lokacija:</label>
			<select
				name="currentLocationId"
				value={formData.currentLocationId}
				onChange={handleChange}
			>
				<option value="">Odaberi lokaciju</option>
				{locations.map((location) => (
					<option key={location.id} value={location.id}>
						{location.name}
					</option>
				))}
			</select>

			<label>
				Is Operational:
				<input
					type="checkbox"
					name="isOperational"
					checked={formData.isOperational}
					onChange={handleChange}
				/>
			</label>

			<label>
				Is Active:
				<input
					type="checkbox"
					name="isActive"
					checked={formData.isActive}
					onChange={handleChange}
				/>
			</label>

			<button type="submit">Dodaj vozilo</button>
			<button type="button" onClick={handleReset}>
				Očisti formu
			</button>
		</form>
	);
};

export default AddVehicleForm;
