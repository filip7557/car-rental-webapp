import axios from "axios";

const API_URL = "http://172.20.8.103:7100/api/Booking";

// Funkcija za dohvaćanje svih rezervacija s filtrima
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

export const cancelBooking = async (bookingId) => {
  const token = localStorage.getItem("token");

  if (!token) {
    console.error("Token is missing");
    return;
  }

  try {
    const statusId = "550e8400-e29b-41d4-a716-446655441114";
    const response = await axios.put(
      `${API_URL}/${bookingId}/status`,
      JSON.stringify(statusId),
      {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );
    return response.data;
  } catch (error) {
    console.error("Error canceling booking:", error);
    return null;
  }
};
