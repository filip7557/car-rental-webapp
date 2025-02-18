import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import NavBar from "../../components/NavBar/NavBar";
import companyVehicleService from "../../services/CompanyVehicleService";
import "./HomePage.css";

function HomePage() {
	const [list, setList] = useState([]);
	const navigate = useNavigate();

	const [userId, setUserId] = useState("");

	useEffect(() => {
		const id = localStorage.getItem("userId");
		setUserId(id);
	}, []);
	useEffect(() => {
		companyVehicleService.getCompanyVehicles().then(setList);
	}, []);

	function handleLoginClick() {
		navigate("/login");
	}

	function handleRegisterClick() {
		navigate("/register");
	}

	return (
		<div>
			<NavBar />
			<div className="homePage">
				{list.length > 0 ? (
					<div className="table">
						<table>
							<thead>
								<tr className="thead">
									<th>Images</th>
									<th>Model</th>
									<th>Company Name</th>
								</tr>
							</thead>
							<tbody>
								{list.map((vehicle) => (
									<tr key={vehicle.id}>
										{/* <td>
											<img
												src={vehicle.imageUrl}
												alt={vehicle.model}
												width="100"
												height="60"
											/>
										</td> */}
										<td> {vehicle.ModelName}</td>
										{/* <td>{vehicle.companyName}</td> */}
									</tr>
								))}
							</tbody>
						</table>
					</div>
				) : (
					<p>No vehicle available</p>
				)}

				<h1>
					Welcome to <strong>CarGo</strong>!
				</h1>
				{userId == null || userId == "" ? (
					<>
						<p>You must be logged in to use our services.</p>
						<button onClick={handleLoginClick}>Login</button>
						<button onClick={handleRegisterClick}>Register</button>
					</>
				) : (
					<></>
				)}
			</div>
		</div>
	);
}

export default HomePage;
