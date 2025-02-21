import { Route, Routes } from "react-router-dom";
import "./App.css";

import AddDamageReportPage from "./pages/AddDamageReportPage/AddDamageReportPage.jsx";
import AddReviewPage from "./pages/AddReviewPage/AddReviewPage";
import CompanyVehiclePage from "./pages/AvailableVehiclePage/AvailableVehiclePage.jsx/AvailableVehiclePage.jsx";
import BookingsPage from "./pages/BookingsPage/BookingsPage";
import CompanyCreatePage from "./pages/CompanyCreatePage/CompanyCreatePage.jsx";
import CompanyRegisterPage from "./pages/CompanyRegisterPage/CompanyRegisterPage.jsx";
import CompanyRequestsPage from "./pages/CompanyRequestsPage/CompanyRequestsPage.jsx";
import AddMaintenanceReportPage from "./pages/CompanyVehicleMaintenancePage/AddMaintenanceReportPage.jsx";
import CompanyVehicleMaintenancePage from "./pages/CompanyVehicleMaintenancePage/CompanyVehicleMaintenancePage.jsx";
import DamageReportPage from "./pages/DamageReportPage/DamageReportPage.jsx";
import EditProfilePage from "./pages/EditProfilePage/EditProfilePage.jsx";
import HomePage from "./pages/HomePage/HomePage.jsx";
import LoginPage from "./pages/LoginPage/LoginPage.jsx";
import ManageCompanyPage from "./pages/ManageCompanyPage/ManageCompanyPage.jsx";
import ManageVehiclesPage from "./pages/ManageVehiclesPage/ManageVehiclesPage.jsx";
import ManageLocationsPage from "./pages/ManageLocationsPage/ManageLocationsPage.jsx";
import NotificationsPage from "./pages/NotificationsPage/NotificationsPage.jsx";
import ProfilePage from "./pages/ProfilePage/ProfilePage.jsx";
import RegisterPage from "./pages/RegisterPage/RegisterPage.jsx";
import AddVehiclePage from "./pages/VehiclePage/AddVehiclePage.jsx";
import CompaniesPage from "./pages/CompaniesPage/CompaniesPage.jsx";
import ManageManagersPage from "./pages/ManageManagersPages/ManageManagersPage.jsx";
import AddBookingPage from "./components/AddBookingPage/AddBookingPage.jsx";

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
      <Route path="/cvehiclePage" element={<CompanyVehiclePage />} />
      <Route path="/vehicle-maintenance/:vehicleId" element={<CompanyVehicleMaintenancePage />} />
      <Route path="/add-maintenance-report/:vehicleId" element={<AddMaintenanceReportPage />} />
      <Route path="/damageReport/:id" element={<DamageReportPage />} />
      <Route path="/create-company-by-admin" element={<CompanyCreatePage />} />
      <Route path="/add-Review/:id" element={<AddReviewPage />} />
      <Route path="/notifications" element={<NotificationsPage />} />
      <Route path="/manageCompanyVehicles" element={<ManageVehiclesPage />} />
      <Route path="/manageCompany" element={<ManageCompanyPage />} />
      <Route
        path="/manageLocations/:id"
        element={<ManageLocationsPage />}
      />
      <Route path="/manageManagers" element={<ManageManagersPage />} />
			<Route path="/all-companies" element={<CompaniesPage />} />
      <Route path="/addBooking/:id" element={<AddBookingPage />} />
		</Routes>
	);
}

export default App;
