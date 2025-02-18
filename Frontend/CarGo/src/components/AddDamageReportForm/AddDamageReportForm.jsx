import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import './AddDamageReportForm.css'

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

    return (
        <div className='addDamageReportForm'>
            <form onSubmit={handleSubmit}>
                <table className="addDamageReportFormTable">
                    <tbody>
                        <tr><td>Title:</td><td><input type="text" name="title" value={damageReport.title || ""} onChange={handleChange} placeholder="Fullname" /></td></tr>
                        <tr><td>Description:</td><td><input type="text" name="description" value={damageReport.description || ""} onChange={handleChange} placeholder="Phonenumber" /></td></tr>                    </tbody>
                </table>
                <button>Save</button>
                <button>Cancel</button>
            </form>
        </div>
    )
}

export default AddDamageReportForm;