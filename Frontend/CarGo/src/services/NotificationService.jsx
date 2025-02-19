import axiosClient from "../axiosClient";

class NotificationService {
  currentPage = 1;

  async getNotifications() {
    try {
      const response = await axiosClient.get("/api/Notification", {
        params: { currentPage: this.currentPage },
      });
      console.log(response.data);
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }
}

const notificationService = new NotificationService();
export default notificationService;
