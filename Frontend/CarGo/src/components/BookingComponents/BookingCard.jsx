import React from "react";
import { useNavigate, Link } from "react-router-dom";
import "./BookingCard.css";

const BookingCard = ({ booking, onCancelBooking }) => {
  const navigate = useNavigate();

  if (!booking) return null;

  const handleReviewClick = (bookingId) => {
    navigate(`/add-review/${bookingId}`);
  };

  return (
    <div className="booking-card">
      <h3>
        <Link
          to={`/companyReviews/${booking.companyId}`}
          className="company-name-link"
        >
          {booking.companyName || "N/A"}
        </Link>
      </h3>
      <p>
        <strong>Vehicle Make:</strong> {booking.vehicleMake || "N/A"}
      </p>
      <p>
        <strong>Vehicle Model:</strong> {booking.vehicleModel || "N/A"}
      </p>
      <p>
        <strong>Status:</strong> {booking.bookingStatus || "N/A"}
      </p>
      <p>
        <strong>Start Date:</strong>{" "}
        {booking.startDate
          ? new Date(booking.startDate).toLocaleDateString()
          : "N/A"}
      </p>
      <p>
        <strong>End Date:</strong>{" "}
        {booking.endDate
          ? new Date(booking.endDate).toLocaleDateString()
          : "N/A"}
      </p>
      <p>
        <strong>Total Price:</strong>{" "}
        {booking.totalPrice ? `$${booking.totalPrice.toFixed(2)}` : "N/A"}
      </p>
      {booking.bookingStatus !== "Canceled" && (
        <button
          onClick={() => onCancelBooking(booking.id)}
          className="cancel-button"
        >
          Cancel Booking
        </button>
      )}
      <button
        onClick={() => handleReviewClick(booking.id)}
        className="review-button"
      >
        Go to Review
      </button>
    </div>
  );
};

export default BookingCard;
