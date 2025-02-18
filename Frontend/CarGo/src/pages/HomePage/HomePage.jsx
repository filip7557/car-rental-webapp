import { useNavigate } from "react-router-dom";
import CompanyVehicle from "../../components/CompanyVehicleTable/CompanyVehicleTable";
import NavBar from "../../components/NavBar/NavBar";
import "./HomePage.css";

function HomePage() {
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
				<CompanyVehicle />
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
