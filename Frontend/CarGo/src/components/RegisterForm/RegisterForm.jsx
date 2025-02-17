import { useState } from "react";
import { useNavigate } from "react-router-dom";
import userService from "../../services/UserService";
import './RegisterForm.css'

function RegisterForm() {

    const [user, setUser] = useState({});
    const navigate = useNavigate();

    function handleChange(e) {
        setUser({...user, [e.target.name]: e.target.value});
    }

    function handleSubmit(e) {
        e.preventDefault();
        if (!(user.fullName && user.email && user.password)) {
            alert("Fullname, email and password fields must be filled.")
            return;
        }
        userService.registerUser(user)
            .then((response) => {
                if (response === "true") {
                    navigate("/login");
                }
                else {
                    alert("Something went wrong. Please try again.");
                }
            });
    }

    function handleLabelClick() {
        setUser({});
        navigate("/login");
    }

    return (
        <div className="registerForm">
            <form onSubmit={handleSubmit}>
                <table className="registerFormTable">
                    <tbody>
                        <tr><td>Fullname:</td><td><input type="text" name="fullName" value={user.fullName || ""} onChange={handleChange} placeholder="Fullname" /></td></tr>
                        <tr><td>Phonenumber:</td><td><input type="tel" name="phoneNumber" value={user.phoneNumber || ""} onChange={handleChange} placeholder="Phonenumber" /></td></tr>
                        <tr><td>Email:</td><td><input type="email" name="email" value={user.email || ""} onChange={handleChange} placeholder="Email" /></td></tr>
                        <tr><td>Password:</td><td><input type="password" name="password" value={user.password || ""} onChange={handleChange} placeholder="Password" /></td></tr>
                    </tbody>
                </table>
                <button>Register</button><br />
                <label onClick={handleLabelClick}>Already have an account?</label>
            </form>
        </div>
    )
}

export default RegisterForm