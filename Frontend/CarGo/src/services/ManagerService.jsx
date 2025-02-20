import axiosClient from "../axiosClient";

class ManagerService {
  async getCompanyId() {
    try {
      const response = await axiosClient.get(
        `/api/Manager?userId=${localStorage.getItem("userId")}`
      );
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }

  async getCompanyManagers(id) {
    try {
      const response = await axiosClient.get("/api/Manager/company", {
        params: { companyId: id },
      });
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }

  async addManagerToCompany(companyId, user) {
    try {
      const response = await axiosClient.post(
        `/api/Manager/${companyId}`,
        user
      );
    } catch (error) {
      console.log(error);
    }
  }

  async removeManagerToCompany(companyId, userId) {
    try {
      const response = await axiosClient.delete(`/api/Manager/${companyId}`, {
        params: { userid: userId },
      });
    } catch (error) {
      console.log(error);
    }
  }
}

const managerService = new ManagerService();
export default managerService;
