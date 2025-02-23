import { useParams, useNavigate } from "react-router-dom";
import { useEffect } from "react";
import "./EditProfilePage.css";

import NavBar from "../../components/NavBar/NavBar";
import EditProfileForm from "../../components/EditProfileForm/EditProfileForm";

function EditProfilePage() {
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    const userId = localStorage.getItem("userId");
    if (!userId) navigate("/login");
  }, []);

  return (
    <div>
      <NavBar />
      <div className="editProfilePage">
        <EditProfileForm id={id} />
      </div>
    </div>
  );
}

export default EditProfilePage;
