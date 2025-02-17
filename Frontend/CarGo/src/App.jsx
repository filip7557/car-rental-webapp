import { Routes, Route } from 'react-router-dom'
import './App.css'

import RegisterPage from './pages/RegisterPage/RegisterPage'
import LoginPage from './pages/LoginPage/LoginPage'

function App() {
  return (
    <Routes>
      <Route path='register' element={<RegisterPage />} />
      <Route path='login' element={<LoginPage />} />
    </Routes>
  )
}

export default App
