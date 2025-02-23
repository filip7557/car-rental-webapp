import "./LoginPage.css";

import LoginForm from "../../components/LoginForm/LoginForm";
import NavBar from "../../components/NavBar/NavBar";

function LoginPage() {
  return (
    <div>
      <NavBar />
      <div className="loginPage">
        <h1>Log In to Your Account</h1>
        <LoginForm />
      </div>
    </div>
  );
}

export default LoginPage;
