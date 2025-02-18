import { useParams } from 'react-router-dom';
import './AddDamageReportPage.css'

import NavBar from '../../components/NavBar/NavBar';
import AddDamageReportForm from '../../components/AddDamageReportForm/AddDamageReportForm';

function AddDamageReportPage() {

    const { id } = useParams();

    return (
        <div>
            <NavBar />
            <div className='addDamageReportPage'>
                <AddDamageReportForm bookingId={id} />
            </div>
        </div>
    )
}

export default AddDamageReportPage;