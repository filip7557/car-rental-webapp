import axiosClient from "../axiosClient";

class DamageReportService {
  async getDamageReports(id) {
    try {
      const response = await axiosClient.get("/api/DamageReport", {
        params: { companyVehicleId: id },
      });
      console.log(response.data)
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }

  async createDamageReport(damageReport) {
    try {
        const response = await axiosClient.post("/api/DamageReport", {
            title: damageReport.title,
            description: damageReport.description,
            bookingId: damageReport.bookingId
        })
        console.log(response.data);
    }
    catch (error) {
        console.log(error);
    }
  }
}

const damageReportService = new DamageReportService();
export default damageReportService;
