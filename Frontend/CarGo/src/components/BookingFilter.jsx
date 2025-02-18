import { useState } from "react";

const BookingFilter = ({ onFilterChange }) => {
  const [date, setDate] = useState("");
  const [status, setStatus] = useState("");

  const handleFilterChange = () => {
    onFilterChange({ date, status });
  };

  return (
    <div>
      <input
        type="date"
        value={date}
        onChange={(e) => setDate(e.target.value)}
      />
      <select value={status} onChange={(e) => setStatus(e.target.value)}>
        <option value="">All Statuses</option>
        <option value="Pending">Pending</option>
        <option value="Completed">Completed</option>
        <option value="Canceled">Canceled</option>
      </select>
      <button onClick={handleFilterChange}>Apply</button>
    </div>
  );
};

export default BookingFilter;
