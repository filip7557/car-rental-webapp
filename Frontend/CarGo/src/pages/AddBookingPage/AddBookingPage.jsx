import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import companyVehicleService from "../../services/CompanyVehicleService";
import ManageLocationsService from "../../services/ManageLocationsService";
import "./AddBookingPage.css";

import NavBar from "../../components/NavBar/NavBar";
import VehicleCard from "../../components/VehicleCard/VehicleCard";
import { addBooking, getTotalPrice } from "../../services/BookingService";

function AddBookingPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [vehicle, setVehicle] = useState({});
  const [booking, setBooking] = useState({
    pickUpLocationId: "",
    dropOffLocationId: "",
  });
  const [locations, setLocations] = useState([]);
  const [totalPrice, setTotalPrice] = useState(0);

  useEffect(() => {
    companyVehicleService.getCompanyVehicleById(id).then((response) => {
      setVehicle(response);
      console.log(response);
      ManageLocationsService.getLocations(response.companyId).then(
        setLocations
      );
    });
  }, []);

  function handleChange(e) {
    setBooking({ ...booking, [e.target.name]: e.target.value });
  }

  function handleAddClick() {
    if (
      booking.pickUpLocationId !== "" &&
      booking.dropOffLocationId !== "" &&
      booking.startDate !== "" &&
      booking.endDate != "" &&
      totalPrice !== 0
    ) {
      booking.companyVehicleId = vehicle.companyVehicleId;
      booking.totalPrice = totalPrice;
      addBooking(booking).then(navigate("/bookingsPage"));
    } else {
      alert("Input all fields.");
    }
  }

  useEffect(() => {
    getTotalPrice(booking.startDate, booking.endDate, vehicle.dailyPrice).then(
      (response) => {
        if (response === "Input data to calculate.") setTotalPrice(0);
        else setTotalPrice(response);
      }
    );
  }, [booking]);

  console.log(booking);

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
            value={booking.pickUpLocationId || "Select pickup location"}
            onChange={handleChange}
          >
            <option value={""}>Select pickup location</option>
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
            onChange={handleChange}
          >
            <option value={""}>Select pickup location</option>
            {locations.map((location) => (
              <option key={location.id} value={location.id}>
                {location.address}, {location.city}, {location.country}
              </option>
            ))}
          </select>
        </div>
        <div>
          <h2>Start date</h2>
          <input
            type="date"
            name="startDate"
            value={booking.startDate}
            onChange={handleChange}
          />
        </div>
        <div>
          <h2>End date</h2>
          <input
            type="date"
            name="endDate"
            value={booking.endDate}
            onChange={handleChange}
          />
        </div>
        <div>
          <h2>Total price</h2>
          <label>
            <strong>{totalPrice?.toFixed(2) || 0} €</strong>
          </label>
        </div>
        <div>
          <button onClick={handleAddClick}>Book</button>
          <button onClick={() => navigate(-1)}>Cancel</button>
        </div>
      </div>
    </div>
  );
}

export default AddBookingPage;
