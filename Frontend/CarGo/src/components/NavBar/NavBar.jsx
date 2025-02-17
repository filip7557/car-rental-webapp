import { Link } from "react-router-dom";
import './NavBar.css'

function NavBar() {
    return (
      <div>
        <nav className="navbar">
          <h3>CarGo</h3>
          <div className="nav-menu">
            <Link to="/">Home</Link>
            <Link to="/login">Login</Link>
            <Link to="/register">Register</Link>
          </div>
        </nav>
      </div>
    );
  }
  
  export default NavBar;