import axiosClient from "../axiosClient";

const API_URL = "/api/review";

export const addReview = async (reviewData) => {
  try {
    const response = await axiosClient.post(API_URL, reviewData);
    return response.data;
  } catch (error) {
    console.error("Error adding review:", error);
    throw error;
  }
};
