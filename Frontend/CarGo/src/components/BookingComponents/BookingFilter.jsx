import { useState, useEffect } from "react";

const BookingFilter = ({ onFilterChange }) => {
  const getInitialStartDate = () => localStorage.getItem("startDate") || "";
  const getInitialEndDate = () => localStorage.getItem("endDate") || "";
  const getInitialStatus = () => localStorage.getItem("status") || "";

  const [startDate, setStartDate] = useState(getInitialStartDate());
  const [endDate, setEndDate] = useState(getInitialEndDate());
  const [status, setStatus] = useState(getInitialStatus());

  const statusMapping = {
    Active: "550e8400-e29b-41d4-a716-446655441111", // Active
    "In-progress": "550e8400-e29b-41d4-a716-446655441112", // In-progress
    Done: "550e8400-e29b-41d4-a716-446655441113", // Done
    Canceled: "550e8400-e29b-41d4-a716-446655441114", // Canceled
  };

  const saveFiltersToLocalStorage = () => {
    localStorage.setItem("startDate", startDate);
    localStorage.setItem("endDate", endDate);
    localStorage.setItem("status", status);
  };

  const handleFilterChange = () => {
    saveFiltersToLocalStorage();
    onFilterChange({
      startDate: startDate ? new Date(startDate).toISOString() : null,
      endDate: endDate ? new Date(endDate).toISOString() : null,
      statusId: statusMapping[status] || null,
    });
  };

  useEffect(() => {
    saveFiltersToLocalStorage();
  }, [startDate, endDate, status]);

  return (
    <div className="booking-filter">
      <div>
        <label>Start Date:</label>
        <input
          type="date"
          value={startDate}
          onChange={(e) => setStartDate(e.target.value)}
        />
      </div>
      <div>
        <label>End Date:</label>
        <input
          type="date"
          value={endDate}
          onChange={(e) => setEndDate(e.target.value)}
        />
      </div>
      <div>
        <label>Status:</label>
        <select value={status} onChange={(e) => setStatus(e.target.value)}>
          <option value="">All Statuses</option>
          <option value="Active">Active</option>
          <option value="In-progress">In-progress</option>
          <option value="Done">Done</option>
          <option value="Canceled">Canceled</option>
        </select>
      </div>
      <button onClick={handleFilterChange}>Apply Filters</button>
    </div>
  );
};

export default BookingFilter;
