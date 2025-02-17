import './LoginPage.css'

import LoginForm from '../../components/LoginForm/LoginForm'

function LoginPage() {
    return (
        <div className='loginPage'>
            <h1>Log In to Your Account</h1>
            <LoginForm />
        </div>
    )
}

export default LoginPage