import { Route, Routes } from "react-router-dom";
import "./App.css";

import AddDamageReportPage from "./pages/AddDamageReportPage/AddDamageReportPage.jsx";
import BookingsPage from "./pages/BookingsPage/BookingsPage";
import CompanyRegisterPage from "./pages/CompanyRegisterPage/CompanyRegisterPage.jsx";
import CompanyRequestsPage from "./pages/CompanyRequestsPage/CompanyRequestsPage.jsx";
import AddMaintenanceReportPage from "./pages/CompanyVehicleMaintenancePage/AddMaintenanceReportPage.jsx";
import CompanyVehicleMaintenancePage from "./pages/CompanyVehicleMaintenancePage/CompanyVehicleMaintenancePage.jsx";
import EditProfilePage from "./pages/EditProfilePage/EditProfilePage";
import HomePage from "./pages/HomePage/HomePage";
import LoginPage from "./pages/LoginPage/LoginPage";
import ProfilePage from "./pages/ProfilePage/ProfilePage";
import RegisterPage from "./pages/RegisterPage/RegisterPage";
import AddVehiclePage from "./pages/VehiclePage/AddVehiclePage.jsx";

function App() {
	return (
		<Routes>
			<Route path="register" element={<RegisterPage />} />
			<Route path="login" element={<LoginPage />} />
			<Route path="/" element={<HomePage />} />
			<Route path="home" element={<HomePage />} />
			<Route path="profile" element={<ProfilePage />} />
			<Route path="profile/:id" element={<EditProfilePage />} />
			<Route path="/add-vehicle" element={<AddVehiclePage />} />
			<Route path="/company-register" element={<CompanyRegisterPage />} />
			<Route path="/company-requests" element={<CompanyRequestsPage />} />
			<Route path="/addDamageReport/:id" element={<AddDamageReportPage />} />
			<Route path="/bookingsPage" element={<BookingsPage />} />
			<Route
				path="/vehicle-maintenance/:vehicleId"
				element={<CompanyVehicleMaintenancePage />}
			/>
			<Route
				path="/add-maintenance-report/:vehicleId"
				element={<AddMaintenanceReportPage />}
			/>
		</Routes>
	);
}

export default App;
