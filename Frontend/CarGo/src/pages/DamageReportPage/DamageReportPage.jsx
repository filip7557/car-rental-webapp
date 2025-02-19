import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom'
import './DamageReportPage.css'

import NavBar from '../../components/NavBar/NavBar'

function DamageReportPage() {

    const [companyVehicle, setCompanyVehicle] = useState({})
    const { id } = useParams();

    return (
        <div>
            <NavBar />
            <div className='damageReportPage'>

            </div>
        </div>
    )
}

export default DamageReportPage