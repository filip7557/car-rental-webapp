import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import './AddDamageReportForm.css'
import damageReportService from '../../services/DamageReportService';

function AddDamageReportForm({ bookingId }) {

    const [damageReport, setDamageReport] = useState({title: "", description: "", bookingId: bookingId})
    const navigate = useNavigate();

    useEffect(() => {
        const userId = localStorage.getItem("userId")
        if (!userId)
            navigate("/login");
    }, [])

    function handleChange(e) {
        setDamageReport({...damageReport, [e.target.name]: e.target.value})
    }

    function handleSubmit(e) {
        e.preventDefault();
        if (damageReport.title === "")
            alert("Title field must be filled.")
        else {
            damageReportService.createDamageReport(damageReport)
                .then(() => {
                    navigate(-1)
                })
        }
    }

    function handleCancelClick(e) {
        e.preventDefault();
        setDamageReport({...damageReport, title: ""})
        setDamageReport({...damageReport, description: ""})
        navigate(-1)
    }

    return (
        <div className='addDamageReportForm'>
            <form onSubmit={handleSubmit}>
                <table className="addDamageReportFormTable">
                    <tbody>
                        <tr><td>Title:</td><td><input type="text" name="title" value={damageReport.title || ""} onChange={handleChange} placeholder="Title" /></td></tr>
                        <tr><td>Description:</td><td><input type="text" name="description" value={damageReport.description || ""} onChange={handleChange} placeholder="Description" /></td></tr>
                    </tbody>
                </table>
                <button>Save</button>
                <button onClick={handleCancelClick}>Cancel</button>
            </form>
        </div>
    )
}

export default AddDamageReportForm;