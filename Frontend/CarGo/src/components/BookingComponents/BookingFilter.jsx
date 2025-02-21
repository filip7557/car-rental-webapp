import { useState, useEffect } from "react";
import BookingStatusService from "../../services/BookingStatusService";

const BookingFilter = ({ onFilterChange }) => {
  const getInitialStartDate = () => localStorage.getItem("startDate") || "";
  const getInitialEndDate = () => localStorage.getItem("endDate") || "";
  const getInitialStatus = () => localStorage.getItem("status") || "";

  const [startDate, setStartDate] = useState(getInitialStartDate());
  const [endDate, setEndDate] = useState(getInitialEndDate());
  const [status, setStatus] = useState(getInitialStatus());
  const [statuses, setStatuses] = useState([]);

  useEffect(() => {
    const fetchStatuses = async () => {
      const data = await BookingStatusService.getBookingStatuses();
      setStatuses(data);
    };
    fetchStatuses();
  }, []);

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
      statusId: status || null,
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
          {statuses.map((s) => (
            <option key={s.id} value={s.id}>
              {s.name}
            </option>
          ))}
        </select>
      </div>
      <button onClick={handleFilterChange}>Apply Filters</button>
    </div>
  );
};

export default BookingFilter;
