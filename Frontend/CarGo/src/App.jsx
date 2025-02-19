import { Route, Routes } from "react-router-dom";
import "./App.css";

import AddReviewPage from "./pages/AddReviewPage/AddReviewPage";
import RegisterPage from "./pages/RegisterPage/RegisterPage.jsx";
import LoginPage from "./pages/LoginPage/LoginPage.jsx";
import HomePage from "./pages/HomePage/HomePage.jsx";
import ProfilePage from "./pages/ProfilePage/ProfilePage.jsx";
import EditProfilePage from "./pages/EditProfilePage/EditProfilePage.jsx";
import AddVehiclePage from "./pages/VehiclePage/AddVehiclePage.jsx";
import CompanyRegisterPage from "./pages/CompanyRegisterPage/CompanyRegisterPage.jsx";
import CompanyRequestsPage from "./pages/CompanyRequestsPage/CompanyRequestsPage.jsx";
import AddMaintenanceReportPage from "./pages/CompanyVehicleMaintenancePage/AddMaintenanceReportPage.jsx";
import AddDamageReportPage from "./pages/AddDamageReportPage/AddDamageReportPage.jsx";
import BookingsPage from "./pages/BookingsPage/BookingsPage.jsx";
import CompanyVehicleMaintenancePage from "./pages/CompanyVehicleMaintenancePage/CompanyVehicleMaintenancePage.jsx";
import DamageReportPage from "./pages/DamageReportPage/DamageReportPage.jsx";
import CompanyCreatePage from "./pages/CompanyCreatePage/CompanyCreatePage.jsx";

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
      <Route path="/damageReport/:id" element={<DamageReportPage />} />
      <Route path="/create-company-by-admin" element={<CompanyCreatePage />} />
      <Route path="/addReview/:id" element={<AddReviewPage />} />
    </Routes>
  );
}

export default App;
