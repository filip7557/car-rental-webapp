import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import companyVehicleService from '../../services/CompanyVehicleService';
import './ManageVehiclesPage.css'

import NavBar from '../../components/NavBar/NavBar';
import CompanyVehicleCard from '../../components/CompanyVehicleTable/CompanyVehicleCard';

function ManageVehiclesPage() {

    const { id } = useParams;

    const [vehicles, setVehicles] = useState([]);

    useEffect(() => {
        companyVehicleService.getCompanyVehicles().then(setVehicles);
    }, [])

    console.log(vehicles);

    return (
        <div>
            <NavBar />
            <div className='manageVehiclesPage'>
                {vehicles.map(vehicle => <CompanyVehicleCard key={vehicle.companyVehicleId} vehicle={vehicle} />)}
            </div>
        </div>
    )
}

export default ManageVehiclesPage;