import axiosClient from "../axiosClient";

const ManageLocationsService = {
  getLocations: async (companyId) => {
    try {
      const response = await axiosClient.get(
        `/Location/company/${companyId}/locations`
      );
      return response.data;
    } catch (error) {
      console.error("Error fetching locations:", error);
      throw error;
    }
  },

  addLocation: async (companyId, locationData) => {
    try {
      const response = await axiosClient.post(
        `/Location/${companyId}`,
        locationData
      );
      return response.data;
    } catch (error) {
      console.error("Error adding location:", error);
      throw error;
    }
  },

  deleteLocation: async (companyId, locationId) => {
    try {
      const response = await axiosClient.delete(
        `/Location/${companyId}/${locationId}`
      );
      return response.data;
    } catch (error) {
      console.error("Error deleting location:", error);
      throw error;
    }
  },
};

export default ManageLocationsService;
