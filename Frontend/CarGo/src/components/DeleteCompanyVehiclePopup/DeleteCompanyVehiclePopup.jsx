import companyVehicleService from "../../services/CompanyVehicleService";
import './DeleteCompanyVehiclePopup.css';

function DeleteCompanyVehiclePopup({ setShowPopup, vehicle, setVehicles }) {
  function handleYesClick() {
    companyVehicleService.deleteCompanyVehicleById(vehicle.companyVehicleId).then(() => {
        companyVehicleService.getCompanyVehicles().then(setVehicles);
        setShowPopup(false);
      });
  }

  return (
    <div className="popupScreen">
      <div className="DeletePopup">
        <h2>{`Are you sure you want to delete ${vehicle.vehicleMake} ${vehicle.vehicleModel} with plate ${vehicle.plateNumber}?`}</h2>
        <button onClick={handleYesClick}>Yes</button>
        <button className="delete" onClick={() => setShowPopup(false)}>No</button>
      </div>
    </div>
  );
}

export default DeleteCompanyVehiclePopup;
