import React from "react";

const BookingCard = ({ booking }) => {
  if (!booking) return null;

  return (
    <div className="booking-card">
      <h3>{booking.companyVehicleId || "N/A"}</h3>
      <p>
        <strong>Vehicle:</strong> {`${booking.companyVehicleId || "N/A"}`}
      </p>
      <p>
        <strong>Status:</strong> {booking.statusId || "N/A"}
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
      <a href={`/review/${booking.id}`} className="review-link">
        Leave a review
      </a>
    </div>
  );
};

export default BookingCard;
