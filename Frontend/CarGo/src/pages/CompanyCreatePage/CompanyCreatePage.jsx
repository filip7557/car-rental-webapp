import React from 'react';
import CompanyCreate from '../../components/CompanyCreate/CompanyCreate';
import NavBar from '../../components/NavBar/NavBar';

const CompanyRequestsPage = () => {
  return (
    <div>
      <NavBar></NavBar>
      <h1>Company Requests</h1>
      <CompanyCreate />
    </div>
  );
};

export default CompanyRequestsPage;
