import { Routes, Route } from 'react-router-dom'
import './App.css'

import RegisterPage from './pages/RegisterPage/RegisterPage'

function App() {
  return (
    <Routes>
      <Route path='register' element={<RegisterPage />} />
    </Routes>
  )
}

export default App
