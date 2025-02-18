import { Routes, Route } from "react-router-dom";
import "./App.css";

import RegisterPage from "./pages/RegisterPage/RegisterPage";
import LoginPage from "./pages/LoginPage/LoginPage";
import HomePage from "./pages/HomePage/HomePage";
import BookingsPage from "./pages/BookingsPage/BookingsPage";
import ProfilePage from "./pages/ProfilePage/ProfilePage";
import EditProfilePage from "./pages/EditProfilePage/EditProfilePage";
import AddVehiclePage from "./pages/VehiclePage/AddVehiclePage.jsx";
import CompanyRequestsPage from "./pages/CompanyRequestsPage/CompanyRequestsPage.jsx";
import CompanyRegisterPage from "./pages/CompanyRegisterPage/CompanyRegisterPage.jsx";
import AddDamageReportPage from "./pages/AddDamageReportPage/AddDamageReportPage.jsx";

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
    </Routes>
  );
}

export default App;
