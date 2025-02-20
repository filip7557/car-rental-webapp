import React from "react";
import "./ManageLocationsComponent.css";

const CompanyLocationComponent = ({ locations, onDeleteLocation }) => {
  if (!locations || locations.length === 0) return <p>No locations found</p>;

  return (
    <div className="company-location-table">
      <h2>Locations</h2>
      <table>
        <thead>
          <tr>
            <th>Country</th>
            <th>City</th>
            <th>Address</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {locations.map((location) => (
            <tr key={location.id}>
              <td>{location.country}</td>
              <td>{location.city}</td>
              <td>{location.address}</td>
              <td>
                <button
                  onClick={() => onDeleteLocation(location.id)}
                  className="delete-button"
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default CompanyLocationComponent;
