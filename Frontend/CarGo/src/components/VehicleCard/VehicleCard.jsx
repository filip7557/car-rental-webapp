import './VehicleCard.css';

function VehicleCard({ vehicle }) {
  
    return (
      <div className="vehicleCard">
        <h3>
          {vehicle?.vehicleMake} {vehicle?.vehicleModel}
        </h3>
        {vehicle?.imageUrl ? (
          <img src={vehicle?.imageUrl} alt={vehicle?.vehicleModel} width="150" />
        ) : (
          <p>No Image Available</p>
        )}
        <p>
          <strong>Company:</strong> {vehicle?.companyName || "N/A"}
        </p>
        <p>
          <strong>Price:</strong> {vehicle?.dailyPrice} € per day
        </p>
        <p>
          <strong>Plate:</strong> {vehicle?.plateNumber || "N/A"}
        </p>
        <p>
          <strong>Color:</strong> {vehicle?.color || "N/A"}
        </p>
        <p>
          <strong>Engine Power:</strong> {vehicle?.enginePower} HP
        </p>
      </div>
    );
  }
  
  export default VehicleCard;