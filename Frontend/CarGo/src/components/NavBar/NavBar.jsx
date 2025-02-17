import { Link } from "react-router-dom";
import userService from "../../services/UserService";
import "./NavBar.css";

function NavBar() {

  function handleLogoutClick() {
    userService.logoutUser();
  }

  return (
    <div>
      <nav className="navbar">
        <h3>CarGo</h3>
        <div className="nav-menu">
          <Link to="/">Home</Link>
          {localStorage.getItem("userId") ? (
            <>
            <Link to="/profile">Profile</Link>
            <Link onClick={handleLogoutClick} to="">Logout</Link>
            </>
          ) : (
            <>
              <Link to="/login">Login</Link>
              <Link to="/register">Register</Link>
            </>
          )}
        </div>
      </nav>
    </div>
  );
}

export default NavBar;
