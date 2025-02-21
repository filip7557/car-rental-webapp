import React from "react";
import { useNavigate } from "react-router-dom";
//import "./CompanyVehicleCard.css";

const CompanyVehicleCard = ({ vehicle }) => {
	const navigate = useNavigate();

	if (!vehicle) return null;

	const handleDetailsClick = (id) => {
		navigate(`/addBooking/${id}`);
	};

	return (
		<div className="company-vehicle-card">
			<h3>
				{vehicle.vehicleMake} {vehicle.vehicleModel}
			</h3>
			{vehicle.imageUrl ? (
				<img src={vehicle.imageUrl} alt={vehicle.vehicleModel} width="150" />
			) : (
				<p>No Image Available</p>
			)}
			<p>
				<strong>Company:</strong> {vehicle.companyName || "N/A"}
			</p>
			<p>
				<strong>Price:</strong> ${vehicle.dailyPrice.toFixed(2)} per day
			</p>
			<p>
				<strong>Plate:</strong> {vehicle.plateNumber || "N/A"}
			</p>
			<p>
				<strong>Color:</strong> {vehicle.color || "N/A"}
			</p>
			<p>
				<strong>Engine Power:</strong> {vehicle.enginePower} HP
			</p>
			<button
				onClick={() => handleDetailsClick(vehicle.companyVehicleId)}
				className="details-button"
			>
				Book This Vehicle
			</button>
		</div>
	);
};

export default CompanyVehicleCard;
