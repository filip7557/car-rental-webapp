import "./RegisterPage.css";

import RegisterForm from "../../components/RegisterForm/RegisterForm";
import NavBar from "../../components/NavBar/NavBar";

function RegisterPage() {
  return (
    <div>
      <NavBar />
      <div className="registerPage">
        <h1>Register a New Account</h1>
        <RegisterForm />
      </div>
    </div>
  );
}

export default RegisterPage;
