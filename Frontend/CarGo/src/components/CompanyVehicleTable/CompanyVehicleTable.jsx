import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import companyVehicleService from "../../services/CompanyVehicleService";
import "./CompanyVehicleTable.css";

function CompanyVehicle() {
	const [list, setList] = useState([]);
	const navigate = useNavigate();

	useEffect(() => {
		companyVehicleService.getCompanyVehicles().then(setList);
	}, []);

	return (
		<div>
			<ul>
				{list.length > 0 ? (
					list.map((vehicle) => (
						<li key={vehicle.companyVehicleId}>
							<h3>{vehicle.vehicleModel}</h3>
							<img
								src={vehicle.imageUrl}
								alt={vehicle.vehicleModel}
								width="150"
							/>
							<p>Company: {vehicle.companyName}</p>
							<p>Price: ${vehicle.dailyPrice} per day</p>
							<p>Plate: {vehicle.plateNumber}</p>
						</li>
					))
				) : (
					<p>No vehicles available</p>
				)}
			</ul>
		</div>
	);
}

export default CompanyVehicle;
