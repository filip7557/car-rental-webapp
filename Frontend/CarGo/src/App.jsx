import { Route, Routes } from "react-router-dom";
import "./App.css";

import RegisterPage from "./pages/RegisterPage/RegisterPage";
import AddVehiclePage from "./pages/VehiclePage/AddVehiclePage.jsx";
import CompanyRequestsPage from './pages/CompanyRequestsPage'
import CompanyRegisterPage from './pages/CompanyRegisterPage'
import RegisterPage from './pages/RegisterPage/RegisterPage'
import LoginPage from './pages/LoginPage/LoginPage'
import HomePage from './pages/HomePage/HomePage'
import ProfilePage from './pages/ProfilePage/ProfilePage'
import EditProfilePage from './pages/EditProfilePage/EditProfilePage'

function App() {
  return (
    <Routes>
      <Route path='register' element={<RegisterPage />} />
      <Route path='login' element={<LoginPage />} />
      <Route path='/' element={<HomePage />} />
      <Route path='profile' element={<ProfilePage />} />
      <Route path='profile/:id' element={<EditProfilePage />} />
      <Route path="/add-vehicle" element={<AddVehiclePage />} />
      <Route path="/company-register" element={<CompanyRegisterPage />} />
      <Route path="/company-requests" element={<CompanyRequestsPage />} />
    </Routes>
  )
}

export default App;
