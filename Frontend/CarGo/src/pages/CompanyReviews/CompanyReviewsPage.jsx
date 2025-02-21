import React, { useState, useEffect } from "react";
import CompanyService from "../../services/CompanyService";
import CompanyReviewsService from "../../services/CompanyReviewsService";
import { useParams } from "react-router-dom";
import "./CompanyReviewsPage.css";
import NavBar from "../../components/NavBar/NavBar";

const CompanyPage = () => {
  const { companyId } = useParams();
  const [company, setCompany] = useState(null);
  const [reviews, setReviews] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchCompanyData = async () => {
      try {
        const companyData = await CompanyService.getCompanyById(companyId);
        setCompany(companyData);
      } catch (err) {
        setError("Error fetching company data.");
      }
    };

    const fetchReviews = async () => {
      try {
        const reviewsData = await CompanyReviewsService.getReviewsByCompanyId(
          companyId
        );
        setReviews(reviewsData);
      } catch (err) {
        setError("Error fetching reviews.");
      }
    };

    fetchCompanyData();
    fetchReviews();
  }, [companyId]);

  if (error) {
    return <div className="error">{error}</div>;
  }

  return (
    <div>
      <NavBar />
      <div className="company-page">
        {company ? (
          <div className="company-info">
            <h2>{company.name}</h2>
            <p>{company.email}</p>
          </div>
        ) : (
          <p>Loading company info...</p>
        )}

        <div className="reviews">
          <h3>Company Reviews</h3>
          {reviews.length > 0 ? (
            <ul>
              {reviews.map((review, index) => (
                <li key={index}>
                  <p>
                    <strong>{review.user}</strong>
                  </p>
                  <p>
                    <em>{review.title}</em>
                  </p>{" "}
                  <p>{review.description}</p>
                </li>
              ))}
            </ul>
          ) : (
            <p>No reviews available.</p>
          )}
        </div>
      </div>
    </div>
  );
};

export default CompanyPage;
