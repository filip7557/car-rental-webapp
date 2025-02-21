import axiosClient from "../axiosClient";

class CompanyReviewsService {
  async getReviewsByCompanyId(id) {
    try {
      const response = await axiosClient.get(`/api/review/${id}`);
      return response.data;
    } catch (error) {
      console.error(
        "Error fetching company reviews:",
        error.response?.data || error.message
      );
      throw error;
    }
  }
}

export default new CompanyReviewsService();
