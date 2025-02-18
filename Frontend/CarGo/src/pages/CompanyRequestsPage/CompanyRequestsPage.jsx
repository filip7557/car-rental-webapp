import React from 'react';
import CompanyRequests from '../../components/CompanyRequests/CompanyRequests';
import NavBar from '../../components/NavBar/NavBar';

const CompanyRequestsPage = () => {
  return (
    <div>
      <NavBar></NavBar>
      <h1>Manage Company Requests</h1>
      <CompanyRequests />
    </div>
  );
};

export default CompanyRequestsPage;
