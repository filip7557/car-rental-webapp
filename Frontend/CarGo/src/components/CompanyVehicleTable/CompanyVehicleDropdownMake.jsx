import { useEffect, useState } from "react";
import companyVehicleService from "../../services/CompanyVehicleService";

//import "./CompanyVehicleDropDown.css";

function CompanyVehicleDropDown({ setList }) {
	const [makes, setMakes] = useState([]);
	const [models, setModels] = useState([]);
	const [types, setTypes] = useState([]);
	const [selectedMake, setSelectedMake] = useState("");
	const [selectedModel, setSelectedModel] = useState("");
	const [selectedType, setSelectedType] = useState("");

	useEffect(() => {
		companyVehicleService.getVehicleMakes().then(setMakes);
		companyVehicleService.getVehicleModels().then(setModels);
		companyVehicleService.getVehicleTypes().then(setTypes);
	}, []);

	function handleMakeChange(e) {
		const make = e.target.value;
		setSelectedMake(make);
		companyVehicleService.vehicleMakeId = e.target.value;
		companyVehicleService.getCompanyVehicles().then(setList);
	}

	// Kada se promeni model
	function handleModelChange(e) {
		const model = e.target.value;
		setSelectedModel(model);
		companyVehicleService.vehicleModelId = e.target.value;
		companyVehicleService.getCompanyVehicles().then(setList);
	}
	function handleTypeChange(e) {
		const type = e.target.value;
		setSelectedType(type);
		companyVehicleService.vehicleTypeId = e.target.value;
		companyVehicleService.getCompanyVehicles().then(setList);
	}

	return (
		<>
			<div>
				<label className="filterby">Filter by Make: </label>
				<select value={selectedMake} onChange={handleMakeChange}>
					<option value="">All</option>
					{makes.map((make) => (
						<option key={make.id} value={make.id}>
							{make.name}
						</option>
					))}
				</select>
			</div>
			<div>
				<label className="filterby">Filter by Model: </label>
				<select value={selectedModel} onChange={handleModelChange}>
					<option value="">All</option>
					{models.map((model) => (
						<option key={model.id} value={model.id}>
							{model.name}
						</option>
					))}
				</select>
			</div>
			<div>
				<label className="filterby">Filter by Type: </label>
				<select value={selectedType} onChange={handleTypeChange}>
					<option value="">All</option>
					{types.map((type) => (
						<option key={type.id} value={type.id}>
							{type.name}
						</option>
					))}
				</select>
			</div>
		</>
	);
}

export default CompanyVehicleDropDown;
