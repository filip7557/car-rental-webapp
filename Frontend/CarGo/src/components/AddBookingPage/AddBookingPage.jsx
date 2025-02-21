import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import companyVehicleService from "../../services/CompanyVehicleService";
import ManageLocationsService from "../../services/ManageLocationsService";
import "./AddBookingPage.css";

import NavBar from "../NavBar/NavBar";
import VehicleCard from "../VehicleCard/VehicleCard";

function AddBookingPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [vehicle, setVehicle] = useState({});
  const [booking, setBooking] = useState({});
  const [locations, setLocations] = useState([]);

  useEffect(() => {
    companyVehicleService.getCompanyVehicleById(id).then((response) => {
      setVehicle(response);
      console.log(response)
      ManageLocationsService.getLocations(response.companyId).then(setLocations);
    });
  }, []);

  console.log(vehicle);

  return (
    <div>
      <NavBar />
      <div className="addBookingPage">
        <h1>Book this Vehicle</h1>
        <VehicleCard vehicle={vehicle} />
        <div>
          <h2>Pick up location</h2>
          <select
            name="pickUpLocationId"
            value={booking.pickUpLocationId || "Select pickup date"}
          >
            {locations.map((location) => (
              <option key={location.id} value={location.id}>
                {location.address}, {location.city}, {location.country}
              </option>
            ))}
          </select>
        </div>
        <div>
          <h2>Drop off location</h2>
          <select
            name="dropOffLocationId"
            value={booking.dropOffLocationId || "Select pickup date"}
          >
            {locations.map((location) => (
              <option key={location.id} value={location.id}>
                {location.address}, {location.city}, {location.country}
              </option>
            ))}
          </select>
        </div>
        <div>
            <h2>Start date</h2>
            <input type="datetime" name="startDate" value={booking.startDate} />
        </div>
        <div>
            <h2>End date</h2>
            <input type="datetime" name="endDate" value={booking.endDate} />
        </div>
        <div>
            <h2>Total price</h2>
            <label><strong>{(booking.endDate - booking.startDate) * vehicle.dailyPrice}</strong></label>
        </div>
        <div>
            <button>Book</button>
            <button onClick={() => navigate(-1)}>Cancel</button>
        </div>
      </div>
    </div>
  );
}

export default AddBookingPage;
