import { useNavigate } from "react-router-dom";
import "./EditVehicleCard.css";

function EditVehicleCard({ vehicle, setDeleteVehId, setShowPopup }) {
  const navigate = useNavigate();

  function handleEditClick() {
    navigate(`/edit-vehicle/${vehicle.companyVehicleId}`);
  }

  function handleDeleteClick() {
	setDeleteVehId(vehicle);
	setShowPopup(true);
  }


  function handleDamageReportsClick() {
    navigate(`/damageReport/${vehicle.companyVehicleId}`);
  }
  return (
    <div className="editVehicleCard">
      <h3>
        {vehicle.vehicleMake} {vehicle.vehicleModel}
      </h3>
      {vehicle.imageUrl ? (
        <img src={vehicle.imageUrl} alt={vehicle.vehicleModel} width="150" />
      ) : (
        <p>No Image Available</p>
      )}
      <p>
        <strong>Company:</strong> {vehicle.companyName || "N/A"}
      </p>
      <p>
        <strong>Price:</strong> {vehicle.dailyPrice.toFixed(2)} € per day
      </p>
      <p>
        <strong>Plate:</strong> {vehicle.plateNumber || "N/A"}
      </p>
      <p>
        <strong>Color:</strong> {vehicle.color || "N/A"}
      </p>
      <p>
        <strong>Engine Power:</strong> {vehicle.enginePower} HP
      </p>
      <button onClick={handleDamageReportsClick}>Damage Reports</button>
      <button onClick={handleEditClick} className="button">
        Edit
      </button>
      <button onClick={handleDeleteClick} className="button">
        Delete
      </button>
    </div>
  );
}

export default EditVehicleCard;
