import React, { useEffect, useState } from 'react';
import CompanyRegisterAndRequestService from '../../services/CompanyRegisterAndRequestService';
import './CompanyRequests.css';

const CompanyRequests = () => {
  const [companyRequests, setCompanyRequests] = useState([]);
  
  const getCompanyRequests = async () => {
    try {
      setCompanyRequests(await CompanyRegisterAndRequestService.getCompanyRequests());
    } catch (error) {
      console.error("Error fetching company requests:", error);
    }
  };

  // Handle accept/reject action
  const handleAction = async (userId, action) => {
    try {
      const response = await CompanyRegisterAndRequestService.manageCompanyRequests(userId, action);
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
      <h2>Company Requests</h2>
      <table className='companyRequests'>
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
                <button>
                  Accept
                </button>
                <button>
                  Reject
                </button>
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
};

export default CompanyRequests;
