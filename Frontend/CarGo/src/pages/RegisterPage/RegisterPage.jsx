import './RegisterPage.css'

import RegisterForm from '../../components/RegisterForm/RegisterForm'

function RegisterPage() {
    return (
        <div className="registerPage">
            <h1>Register a New Account</h1>
            <RegisterForm />
        </div>
    )
}

export default RegisterPage