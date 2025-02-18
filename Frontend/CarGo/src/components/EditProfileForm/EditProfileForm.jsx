import { useState, useEffect } from "react";
import userService from "../../services/UserService";
import { useNavigate } from "react-router-dom";
import './EditProfileForm.css';

function EditProfileForm({ id }) {
  const [user, setUser] = useState({});
  const navigate = useNavigate();

  useEffect(() => {
    userService.getUser(id).then(setUser);
  }, []);

  function handleChange(e) {
    setUser({ ...user, [e.target.name]: e.target.value });
  }

  function handleSubmit(e) {
    e.preventDefault();
    if (!(user.fullName && user.email)) {
      alert("Fullname and email fields must be filled.");
      return;
    }
    userService.updateUser(id, user).then((response) => {
      if (response === "Updated.") navigate("/profile");
      else {
        alert("Something went wrong. Please try again.");
      }
    });
  }

  function handleCancelClick(e) {
    e.preventDefault();
    navigate(-1);
  }

  return (
    <div className="editProfileForm">
      <form onSubmit={handleSubmit}>
        <table className="editProfileFormTable">
          <tbody>
            <tr>
              <td>Fullname:</td>
              <td>
                <input
                  type="text"
                  name="fullName"
                  value={user.fullName || ""}
                  onChange={handleChange}
                  placeholder="Fullname"
                />
              </td>
            </tr>
            <tr>
              <td>Phonenumber:</td>
              <td>
                <input
                  type="tel"
                  name="phoneNumber"
                  value={user.phoneNumber || ""}
                  onChange={handleChange}
                  placeholder="Phonenumber"
                />
              </td>
            </tr>
            <tr>
              <td>Email:</td>
              <td>
                <input
                  type="email"
                  name="email"
                  value={user.email || ""}
                  onChange={handleChange}
                  placeholder="Email"
                />
              </td>
            </tr>
          </tbody>
        </table>
        <button>Save</button>
        <button onClick={handleCancelClick}>Cancel</button>
      </form>
    </div>
  );
}

export default EditProfileForm;
