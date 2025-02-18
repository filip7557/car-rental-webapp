import { useParams } from "react-router-dom";
import './EditProfilePage.css'

import NavBar from "../../components/NavBar/NavBar";
import EditProfileForm from "../../components/EditProfileForm/EditProfileForm";

function EditProfilePage() {

    const { id } = useParams();

    return (
        <div>
            <NavBar />
            <div className="editProfilePage">
                <EditProfileForm id={id} />
            </div>
        </div>
    )
}

export default EditProfilePage