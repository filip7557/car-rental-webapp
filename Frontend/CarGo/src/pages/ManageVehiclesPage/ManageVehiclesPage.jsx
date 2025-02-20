import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import companyVehicleService from "../../services/CompanyVehicleService";
import "./ManageVehiclesPage.css";

import NavBar from "../../components/NavBar/NavBar";
import EditVehicleCard from "../../components/EditVehicleCard/EditVehicleCard";
import DeleteCompanyVehiclePopup from "../../components/DeleteCompanyVehiclePopup/DeleteCompanyVehiclePopup";
function ManageVehiclesPage() {
  const navigate = useNavigate();

  const [vehicles, setVehicles] = useState({});
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(2);
  const [pages, setPages] = useState([]);
  const [deleteVeh, setDeleteVeh] = useState("");
  const [showPopup, setShowPopup] = useState(false);

  useEffect(() => {
    setPageNumber(1);
    companyVehicleService
      .getCompanyVehicles(pageNumber, pageSize)
      .then(setVehicles);
  }, []);

  useEffect(() => {
    let numberOfPages = Math.ceil(vehicles.totalRecords / vehicles.pageSize);
    let newPages = [];
    for (let i = 1; i <= numberOfPages; i++) {
      newPages.push(i);
    }
    setPages(newPages);
  }, [vehicles]);

  useEffect(() => {
    companyVehicleService
      .getCompanyVehicles(pageNumber, pageSize)
      .then(setVehicles);
  }, [pageNumber, pageSize]);

  function handlePageClick(page) {
    setPageNumber(page);
  }

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
          <div className="vehicles">
            {vehicles?.data?.map((vehicle) => (
              <EditVehicleCard
                key={vehicle.companyVehicleId}
                vehicle={vehicle}
                setVehicles={setVehicles}
                setDeleteVehId={setDeleteVeh}
                setShowPopup={setShowPopup}
              />
            ))}
          </div>
          <div className="pagging">
            {pages.map((page) => (
              <label
                key={page}
                className={page === pageNumber ? "currentPage" : ""}
                onClick={() => handlePageClick(page)}
              >
                {page}{" "}
              </label>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

export default ManageVehiclesPage;
