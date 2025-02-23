import React, { useState, useEffect } from "react";
import ManageLocationsService from "../../services/ManageLocationsService";
import CompanyLocationComponent from "../../components/ManageLocationsComponent/ManageLocationsComponent";
import NavBar from "../../components/NavBar/NavBar";
import "./ManageLocationsPage.css";
import { useParams } from "react-router-dom";

const ManageLocationsPage = () => {
  const { id } = useParams();

  const [locations, setLocations] = useState([]);
  const [newLocation, setNewLocation] = useState({
    country: "",
    city: "",
    address: "",
  });

  useEffect(() => {
    const fetchLocations = async () => {
      try {
        const data = await ManageLocationsService.getLocations(id);
        setLocations(data);
      } catch (error) {
        console.error("Error fetching locations:", error);
      }
    };
    fetchLocations();
  }, []);

  const handleAddLocation = async () => {
    try {
      const newLoc = await ManageLocationsService.addLocation(id, newLocation);
      console.log("New location added:", newLoc);

      const data = await ManageLocationsService.getLocations(id);
      setLocations(data);

      setNewLocation({ country: "", city: "", address: "" });
    } catch (error) {
      console.error("Error adding location:", error);
    }
  };

  const handleDeleteLocation = async (locationId) => {
    try {
      await ManageLocationsService.deleteLocation(id, locationId);
      setLocations(locations.filter((loc) => loc.id !== locationId));
    } catch (error) {
      console.error("Error deleting location:", error);
    }
  };

  return (
    <div className="manage-locations-page">
      <NavBar />
      <div className="location-form">
        <h2>Add New Location</h2>
        <input
          type="text"
          placeholder="Country"
          value={newLocation.country}
          onChange={(e) =>
            setNewLocation({ ...newLocation, country: e.target.value })
          }
        />
        <input
          type="text"
          placeholder="City"
          value={newLocation.city}
          onChange={(e) =>
            setNewLocation({ ...newLocation, city: e.target.value })
          }
        />
        <input
          type="text"
          placeholder="Address"
          value={newLocation.address}
          onChange={(e) =>
            setNewLocation({ ...newLocation, address: e.target.value })
          }
        />
        <button onClick={handleAddLocation}>Save</button>
      </div>

      <CompanyLocationComponent
        locations={locations}
        onDeleteLocation={handleDeleteLocation}
      />
    </div>
  );
};

export default ManageLocationsPage;
