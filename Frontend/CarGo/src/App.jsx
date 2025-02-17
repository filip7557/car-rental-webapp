import { Routes, Route } from 'react-router-dom'
import './App.css'

import RegisterPage from './pages/RegisterPage/RegisterPage'
import LoginPage from './pages/LoginPage/LoginPage'
import HomePage from './pages/HomePage/HomePage'
import ProfilePage from './pages/ProfilePage/ProfilePage'

function App() {
  return (
    <Routes>
      <Route path='register' element={<RegisterPage />} />
      <Route path='login' element={<LoginPage />} />
      <Route path='/' element={<HomePage />} />
      <Route path='profile' element={<ProfilePage />} />
    </Routes>
  )
}

export default App
