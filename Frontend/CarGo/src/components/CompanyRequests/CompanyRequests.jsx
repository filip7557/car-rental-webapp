import React, { useEffect, useState } from 'react';
import CompanyService from '../../services/CompanyService';
import './CompanyRequests.css';

const CompanyRequests = () => {
  const [companyRequests, setCompanyRequests] = useState([]);
  const [isActiveFilter, setIsActiveFilter] = useState(true);
  const [page, setPage] = useState(1);
  const [rpp, setRpp] = useState(10);
  
  const getCompanyRequests = async () => {
    try {
      const data = await CompanyService.getCompanyRequests(isActiveFilter, page, rpp)
      setCompanyRequests(data);
    } catch (error) {
      console.error("Error fetching company requests:", error);
      setCompanyRequests([]);
    }
  };

  async function handleAction(userId, isAccepted){
    try {
      const response = await CompanyService.manageCompanyRequests(userId, isAccepted)
      getCompanyRequests();
    } catch (error) {
      console.error("Error managing company request:", error.response?.data || error.message);
    }
  };

  useEffect(() => {
    getCompanyRequests();
  }, [isActiveFilter, page, rpp]);

  return (
    <div>
      <h2>Company Requests</h2>
      <div>
        <label>
          Active Requests
          <input
            type="checkbox"
            checked={isActiveFilter}
            onChange={() => setIsActiveFilter((prev) => !prev)}
          />
        </label>
      </div>

      {companyRequests.length === 0 ? (
        <p>No requests found.</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Email</th>
              <th>Is Approved</th>
            </tr>
          </thead>
          <tbody>
            {companyRequests.map((request) => (
              <tr key={request.userId}>
                <td>{request.name}</td>
                <td>{request.email}</td>
                <td>{request.isApproved ? "Yes" : "No"}</td>
                <td>
                  {request.isActive ? (
                    <>
                      <button onClick={() => handleAction(request.userId, true)}>Accept</button>
                      <button onClick={() => handleAction(request.userId, false)}>Reject</button>
                    </>
                  ) : (
                    <span>Processed</span>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      <div>
        <button onClick={() => setPage((prev) => Math.max(prev - 1, 1))} disabled={page === 1}>
          Previous
        </button>
        <span> Page {page} </span>
        <button onClick={() => setPage((prev) => prev + 1)}>Next</button>
      </div>
    </div>
  );
};

export default CompanyRequests;
