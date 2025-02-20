import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import companyVehicleService from "../../services/CompanyVehicleService";
import "./ManageVehiclesPage.css";

import NavBar from "../../components/NavBar/NavBar";
import EditVehicleCard from "../../components/EditVehicleCard/EditVehicleCard";
import DeleteCompanyVehiclePopup from "../../components/DeleteCompanyVehiclePopup/DeleteCompanyVehiclePopup";
function ManageVehiclesPage() {
  const navigate = useNavigate();

  const [vehicles, setVehicles] = useState([]);
  const [deleteVeh, setDeleteVeh] = useState("");
  const [showPopup, setShowPopup] = useState(false);

  useEffect(() => {
    companyVehicleService.getCompanyVehicles().then(setVehicles);
  }, []);

  function handleAddClick() {
    navigate("/add-vehicle");
  }

  const popup = showPopup ? (
    <DeleteCompanyVehiclePopup
      setVehicles={setVehicles}
      setShowPopup={setShowPopup}
      vehicle={deleteVeh}
    />
  ) : undefined;

  return (
    <div>
      {popup}
      <div>
        <NavBar />
        <div className="manageVehiclesPage">
          <h1>Manage Company Vehicles</h1>
          <div className="addVehicle">
            <button onClick={handleAddClick}>Add vehicle</button>
          </div>
          <div key={vehicles.length} className="vehicles">
            {vehicles.map((vehicle) => (
              <EditVehicleCard
                key={vehicle.companyVehicleId}
                vehicle={vehicle}
                setVehicles={setVehicles}
                setDeleteVehId={setDeleteVeh}
                setShowPopup={setShowPopup}
              />
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

export default ManageVehiclesPage;
