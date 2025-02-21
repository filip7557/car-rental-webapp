import { useState, useEffect, use } from "react";
import managerService from "../../services/ManagerService";
import userService from "../../services/UserService";
import "./ManageManagersPage.css";

import NavBar from "../../components/NavBar/NavBar";
import ManagerCard from "../../components/ManagerCard/ManagerCard";

function ManageManagersPage() {
  const [companyId, setCompanyId] = useState("");
  const [currentManagers, setCurrentManagers] = useState([]);
  const [foundManagers, setFoundManagers] = useState([]);
  const [email, setEmail] = useState("");

  useEffect(() => {
    managerService.getCompanyId().then((response) => {
      setCompanyId(response);
      managerService.getCompanyManagers(response).then(setCurrentManagers);
    });
  }, []);

  function handleChange(e) {
    setEmail(e.target.value);
  }

  function addManager(manager) {
    managerService.addManagerToCompany(companyId, manager).then(() => {
      managerService.getCompanyManagers(companyId).then(setCurrentManagers);
    });
    setEmail("");
    setFoundManagers([]);
    alert(`${manager.fullName} has been added as manager.`);
  }

  function removeManager(manager) {
    managerService.removeManagerToCompany(companyId, manager.id).then(() => {
      managerService.getCompanyManagers(companyId).then(setCurrentManagers);
    });
  }

  function handleSearchClick() {
    userService.getUserByEmail(email).then((user) => {
        setFoundManagers([...foundManagers, user]);
    });
  }

  return (
    <div>
      <NavBar />
      <div className="manageManagersPage">
        <h1>Manage Managers</h1>
        <div>
          <h2>Add a Manager</h2>
          <input
            type="email"
            name="email"
            value={email}
            onChange={handleChange}
            placeholder="Email"
          />
          <button onClick={handleSearchClick}>Search</button>
          <div className="managers">
            {foundManagers.length > 0 ? (
              foundManagers.map((manager) => (
                <ManagerCard
                  key={manager.id}
                  manager={manager}
                  addManager={addManager}
                  isInSearch={true}
                />
              ))
            ) : email.length < 1 ? (
              <p>Type in an email to start searching.</p>
            ) : (
              <p>No results.</p>
            )}
          </div>
        </div>
        <hr />
        <div>
          <h2>Current Managers</h2>
          <div className="managers">
            {currentManagers.length > 0 ? (
              currentManagers.map((manager) => (
                <ManagerCard
                  key={manager.id}
                  manager={manager}
                  isInSearch={false}
                  removeManager={removeManager}
                />
              ))
            ) : (
              <p>There are no managers yet.</p>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default ManageManagersPage;
