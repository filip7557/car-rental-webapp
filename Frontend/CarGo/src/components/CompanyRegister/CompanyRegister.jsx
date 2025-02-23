import { useState } from "react";
import { useNavigate } from "react-router-dom";
import CompanyRegisterAndRequestService from "../../services/CompanyService";

const CompanyRegister = () => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    const companyData = {
      name: name,
      email: email,
      isActive: true,
      isApproved: false,
    };

    try {
      await CompanyRegisterAndRequestService.createCompanyRequest(companyData);
      alert("Company registered successfully!");
      navigate("/");
      
    } catch (error) {
      alert("Error: " + (error.response?.data || "Request failed"));
    }
  };

  return (
    <div className="container">
      <h1>Register Company</h1>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Company Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />
        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <div className="buttons">
          <button type="submit">Send</button>
          <button type="button" onClick={() => navigate("/")}>Cancel</button>
        </div>
      </form>
    </div>
  );
};

export default CompanyRegister;
