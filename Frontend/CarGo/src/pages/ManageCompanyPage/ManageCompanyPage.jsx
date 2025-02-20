import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import CompanyService from "../../services/CompanyService";
import managerService from "../../services/ManagerService";
import "./ManageCompanyPage.css";

import NavBar from "../../components/NavBar/NavBar";

const ManageCompanyPage = () => {
  const [company, setCompany] = useState({});
  const [companyId, setCompanyId] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    managerService.getManagerId().then(setCompanyId);
  }, []);

  useEffect(() => {
    const fetchCompanyInfo = async () => {
      try {
        if (companyId) {
          const response = await CompanyService.getCompanyInfoById(companyId);

          //console.log("RESPONSE", response);

          setCompany(response);
        }
      } catch (error) {
        console.error("Greška pri dohvaćanju podataka o kompaniji:", error);
        setError("Došlo je do greške pri dohvaćanju podataka.");
      }
    };

    fetchCompanyInfo();
  }, [companyId]);

  const handleNavigate = (path) => {
    navigate(path);
  };

  return (
    <div>
      <NavBar />
      <div className="manageCompanyPage">
        <h1>{company?.name || ""}</h1>
        <h2>
          <strong>Email:</strong> {company.email}
        </h2>

        <div>
          <button onClick={() => handleNavigate("/manageCompanyVehicles")}>
            Manage Vehicle
          </button>
        </div>

        <div>
          <button
            onClick={() =>
              handleNavigate(`/path-to-manage-location/${companyId}`)
            }
          >
            Manage Location
          </button>
        </div>

        <div>
          <button onClick={() => handleNavigate("/path-to-manage-managers")}>
            Manage Managers
          </button>
        </div>

        <div>
          <button onClick={() => handleNavigate("/path-to-manage-bookings")}>
            Manage Bookings
          </button>
        </div>
      </div>
    </div>
  );
};

export default ManageCompanyPage;
