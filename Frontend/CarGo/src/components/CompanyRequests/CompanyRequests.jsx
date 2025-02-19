import React, { useEffect, useState } from 'react';
import CompanyService from '../../services/CompanyService';
import './CompanyRequests.css';

const CompanyRequests = () => {
  const [companyRequests, setCompanyRequests] = useState([]);
  
  const getCompanyRequests = async () => {
    try {
      const data = await CompanyService.getCompanyRequests()
      setCompanyRequests(data.length > 0 ? data : null);
    } catch (error) {
      console.error("Error fetching company requests:", error);
      setCompanyRequests(null);
    }
  };

  // Handle accept/reject action
  const handleAction = async (userId, action) => {
    try {
      const response = await CompanyService.manageCompanyRequests(userId, action);
      if (response.status === 200) {
        getCompanyRequests();
      }
    } catch (error) {
      console.error("Error managing company request:", error);
    }
  };

  useEffect(() => {
    getCompanyRequests();
  }, []);

  return (
    <div>
      {companyRequests === null ? (
        <div><h2>No requests.</h2></div>
      ) : (
        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Email</th>
              <th>Is Active</th>
              <th>Is Approved</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {companyRequests.map(request => 
              <tr key={request.userId}>
                <td>{request.name}</td>
                <td>{request.email}</td>
                <td>{request.isActive ? 'Yes' : 'No'}</td>
                <td>{request.isApproved ? 'Yes' : 'No'}</td>
                <td>
                  <button onClick={() => handleAction(request.userId, "true")}>
                    Accept
                  </button>
                  <button onClick={() => handleAction(request.userId, "false")}>
                    Reject
                  </button>
                </td>
              </tr>
            )}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default CompanyRequests;
