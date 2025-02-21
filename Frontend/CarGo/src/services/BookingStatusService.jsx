import axiosClient from "../axiosClient";

const BookingStatusService = {
  getBookingStatuses: async () => {
    try {
      const response = await axiosClient.get(
        "/api/BookingStatus/bookingStatus"
      );
      return response.data;
    } catch (error) {
      console.error("Error fetching booking statuses:", error);
      throw error;
    }
  },
};

export default BookingStatusService;
