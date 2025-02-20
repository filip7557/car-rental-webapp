import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import companyVehicleService from '../../services/CompanyVehicleService';
import './ManageVehiclesPage.css'

import NavBar from '../../components/NavBar/NavBar';

function ManageVehiclesPage() {

    const { id } = useParams;

    const [vehicles, setVehicles] = useState([]);

    useEffect(() => {
        companyVehicleService.getCompanyVehiclesByCompanyId(id).then(setVehicles);
    }, [])

    return (
        <div>
            <NavBar />
            <div className='manageVehiclesPage'>
                {vehicles.map(vehicle => )}
            </div>
        </div>
    )
}

export default ManageVehiclesPage;