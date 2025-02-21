import NavBar from '../../components/NavBar/NavBar';
import Companies from '../../components/Companies/Companies'

function CompaniesPage() {
    return (
        <div>
            <NavBar />
            <div className='addDamageReportPage'>
                <Companies />
            </div>
        </div>
    )
}

export default CompaniesPage;