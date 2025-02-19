import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import companyVehicleService from "../../services/CompanyVehicleService";
import damageReportService from "../../services/DamageReportService";
import "./DamageReportPage.css";

import DamageReportCard from "../../components/DamageReportCard/DamageReportCard";

import NavBar from "../../components/NavBar/NavBar";

function DamageReportPage() {
  const [companyVehicle, setCompanyVehicle] = useState({});
  const [damageReports, setDamageReports] = useState([]);
  const { id } = useParams();

  useEffect(() => {
    companyVehicleService.getCompanyVehicleById(id).then(setCompanyVehicle);
    damageReportService.getDamageReports(id).then(setDamageReports);
  }, []);

  console.log(damageReports);

  return (
    <div>
      <NavBar />
      <div className="damageReportPage">
        <div className="vehInfo">
          <img src={companyVehicle.imageUrl} alt="" />
          <div>
            <h1>
              {companyVehicle.vehicleMake} {companyVehicle.vehicleModel}
            </h1>
            <h2>{`${companyVehicle.plateNumber}`.toUpperCase()}</h2>
          </div>
        </div>
        <h1>Damage Reports</h1>
        <div key={damageReports.length}>
          {
            damageReports.map(damageReport => <DamageReportCard key={damageReport.id} damageReport={damageReport} />)
          }
        </div>
      </div>
    </div>
  );
}

export default DamageReportPage;
