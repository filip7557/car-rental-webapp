import { useState } from "react";
import { useNavigate } from "react-router-dom";
import userService from "../../services/UserService";
import roleService from "../../services/RoleService";

import "./LoginForm.css";

function LoginForm() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  function handleEmailChange(e) {
    setEmail(e.target.value);
  }

  function handlePasswordChange(e) {
    setPassword(e.target.value);
  }

  function handleSubmit(e) {
    e.preventDefault();
    if (!(email && password)) {
        alert("Email and password fields must be filled.");
        return;
    }
    userService.loginUser(email, password)
        .then((response) => {
            if (response === "Invalid credentials.") {
                alert("Invalid credentials.");
            }
            else {
                localStorage.setItem("token", response.token);
                localStorage.setItem("userId", response.userId);
                roleService.getRole(localStorage.getItem("userId"))
                    .then((role) => {
                        localStorage.setItem("role", role);
                    })
                navigate("/home")
            }
        })
  }

  function handleLabelClick() {
    setEmail("");
    setPassword("");
    navigate("/register");

  }

  return (
    <div className="loginForm">
      <form onSubmit={handleSubmit}>
        <table className="loginFormTable">
          <tbody>
            <tr>
              <td>Email:</td>
              <td>
                <input
                  type="email"
                  name="email"
                  value={email || ""}
                  onChange={handleEmailChange}
                  placeholder="Email"
                />
              </td>
            </tr>
            <tr>
              <td>Password:</td>
              <td>
                <input
                  type="password"
                  name="password"
                  value={password || ""}
                  onChange={handlePasswordChange}
                  placeholder="Password"
                />
              </td>
            </tr>
          </tbody>
        </table>
        <button>Login</button>
        <br />
        <label onClick={handleLabelClick}>Register a new account.</label>
      </form>
    </div>
  );
}

export default LoginForm;
