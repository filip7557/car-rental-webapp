import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import companyVehicleService from "../../services/CompanyVehicleService";

function CompanyVehicle() {
	const [list, setList] = useState([]);
	const navigate = useNavigate();

	useEffect(() => {
		companyVehicleService.getCompanyVehicles().then(setList);
	}, []);

	return (
		<div>
			{list.length > 0 ? (
				<div className="table">
					<table>
						<thead>
							<tr className="thead">
								<th>Company Name</th>
								<th>Model</th>
								<th>Daily Price</th>
								<th>Number of plate</th>
							</tr>
						</thead>
						<tbody>
							{list.map((vehicle) => (
								<tr key={vehicle.companyVehicleId}>
									<td>{vehicle.companyName}</td>
									<td>{vehicle.vehicleModel}</td>
									<td>{vehicle.dailyPrice}</td>
									<td>{vehicle.plateNumber}</td>
								</tr>
							))}
						</tbody>
					</table>
				</div>
			) : (
				<p>No vehicle available</p>
			)}
		</div>
	);
}

export default CompanyVehicle;
