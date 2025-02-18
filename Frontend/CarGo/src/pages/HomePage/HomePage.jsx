import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import "./HomePage.css";

import NavBar from "../../components/NavBar/NavBar";

function HomePage() {
  const navigate = useNavigate();

  const [userId, setUserId] = useState("");

  useEffect(() => {
    const id = localStorage.getItem("userId");
    setUserId(id);
  }, []);

  function handleLoginClick() {
    navigate("/login");
  }

  function handleRegisterClick() {
    navigate("/register");
  }

  return (
    <div>
      <NavBar />
      <div className="homePage">
        <h1>
          Welcome to <strong>CarGo</strong>!
        </h1>
        {userId == null || userId == "" ? (
          <>
            <p>You must be logged in to use our services.</p>
            <button onClick={handleLoginClick}>Login</button>
            <button onClick={handleRegisterClick}>Register</button>
          </>
        ) : (
          <></>
        )}
      </div>
    </div>
  );
}

export default HomePage;
