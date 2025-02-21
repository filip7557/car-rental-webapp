import axiosClient from "../axiosClient";

const API_URL = "/api/Booking";

export const getBookings = async (filters = {}) => {
  try {
    const response = await axiosClient.get(API_URL, {
      params: filters,
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching bookings:", error);
    return [];
  }
};

export const cancelBooking = async (bookingId) => {
  try {
    const response = await axiosClient.put(`${API_URL}/${bookingId}/status`);
    return response.data;
  } catch (error) {
    console.error("Error canceling booking:", error);
    return null;
  }
};

export const addBooking = async (booking) => {
  try {
    const response = await axiosClient.post("/api/Booking", booking)
    console.log(response.data);
  } catch (error) {
    console.log(error);
  }
}

export const getTotalPrice = async (startDate, endDate, dailyPrice) => {
  try {
    console.log(startDate, endDate, dailyPrice)
    const response = await axiosClient.get("/api/Booking/getTotalPrice", {
      params: {
        startDate,
        endDate,
        dailyPrice,
      },
    });
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.log(error);
  }
};
