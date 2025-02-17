import { Route, Routes } from "react-router-dom";
import "./App.css";

import HomePage from "./pages/HomePage/HomePage";
import LoginPage from "./pages/LoginPage/LoginPage";
import RegisterPage from "./pages/RegisterPage/RegisterPage";
import AddVehiclePage from "./pages/VehiclePage/AddVehiclePage.jsx";

function App() {
  return (
    <Routes>
      <Route path='register' element={<RegisterPage />} />
      <Route path='login' element={<LoginPage />} />
      <Route path='/' element={<HomePage />} />
      <Route path='profile' element={<ProfilePage />} />
      <Route path='profile/:id' element={<EditProfilePage />} />
      <Route path="/company-register" element={<CompanyRegister />} />
      <Route path="/add-vehicle" element={<AddVehiclePage />} />
    </Routes>
  )
}

export default App;
