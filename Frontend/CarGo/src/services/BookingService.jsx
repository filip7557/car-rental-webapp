import axios from "axios";

const API_URL = "https://localhost:7100/api/Booking";

export const getBookings = async (filters = {}) => {
  const token = localStorage.getItem("token");

  if (!token) {
    console.error("Token is missing");
    return [];
  }

  try {
    const response = await axios.get(API_URL, {
      params: filters,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching bookings:", error);
    return [];
  }
};

export const updateBookingStatus = async (bookingId, status) => {
  const token = localStorage.getItem("token");

  if (!token) {
    console.error("Token is missing");
    return;
  }

  try {
    await axios.put(
      `${API_URL}/${bookingId}/status`,
      { status },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
  } catch (error) {
    console.error("Error updating booking status:", error);
  }
};
