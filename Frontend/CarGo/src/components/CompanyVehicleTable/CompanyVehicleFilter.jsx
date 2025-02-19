import { useEffect, useState } from "react";

const CompanyVehicleFilter = ({ onFilterChange }) => {
	const getInitialMake = () => localStorage.getItem("vehicleMake") || "";
	const getInitialModel = () => localStorage.getItem("vehicleModel") || "";
	const getInitialColor = () => localStorage.getItem("vehicleColor") || "";

	const [vehicleMake, setVehicleMake] = useState(getInitialMake());
	const [vehicleModel, setVehicleModel] = useState(getInitialModel());
	const [vehicleColor, setVehicleColor] = useState(getInitialColor());

	const saveFiltersToLocalStorage = () => {
		localStorage.setItem("vehicleMake", vehicleMake);
		localStorage.setItem("vehicleModel", vehicleModel);
		localStorage.setItem("vehicleColor", vehicleColor);
	};

	const handleFilterChange = () => {
		saveFiltersToLocalStorage();
		onFilterChange({
			vehicleMake: vehicleMake || null,
			vehicleModel: vehicleModel || null,
			vehicleColor: vehicleColor || null,
		});
	};

	useEffect(() => {
		saveFiltersToLocalStorage();
	}, [vehicleMake, vehicleModel, vehicleColor]);

	return (
		<div className="company-vehicle-filter">
			<div>
				<label>Vehicle Make:</label>
				<input
					type="text"
					value={vehicleMake}
					onChange={(e) => setVehicleMake(e.target.value)}
				/>
			</div>
			<div>
				<label>Vehicle Color:</label>
				<input
					type="text"
					value={vehicleColor}
					onChange={(e) => setVehicleColor(e.target.value)}
				/>
			</div>
			<button onClick={handleFilterChange}>Apply Filters</button>
		</div>
	);
};

export default CompanyVehicleFilter;
