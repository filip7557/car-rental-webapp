import { Link } from "react-router-dom";
import "./NavBar.css";

function NavBar() {

  function handleLogoutClick(e) {
    localStorage.clear();
    document.location.href = "/";
  }

  return (
    <div>
      <nav className="navbar">
        <h3>CarGo</h3>
        <div className="nav-menu">
          <Link to="/">Home</Link>
          {localStorage.getItem("userId") ? (
            <>
              <Link to="/bookingsPage">Bookings</Link>
              {localStorage.getItem("role") === "Administrator" ? (
                <>
                  <Link to="/create-company-by-admin">Create company</Link>
                  <Link to="/get-all-company-requests">Company Requests</Link>
                </>
              ) : localStorage.getItem("role") === "Manager" ? (
                <>
                  <Link to=" ">Manage company</Link>
                </>
              ) : (
                <>
                  <Link to="/company-register">Register company</Link>
                </>
              )}
              <Link to="/profile">Profile</Link>
              <Link onClick={handleLogoutClick} to="/">
                Logout
              </Link>
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
