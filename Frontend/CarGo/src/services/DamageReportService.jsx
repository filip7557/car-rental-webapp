import axiosClient from "../axiosClient";
import imageService from "./ImageService";

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

  async createDamageReport(damageReport, images) {
    try {
        const response = await axiosClient.post("/api/DamageReport", {
            title: damageReport.title,
            description: damageReport.description,
            bookingId: damageReport.bookingId
        })
        console.log(response.data);
        const damageReportId = response.data;
        const newImages = [];
        console.log(images);
        images.forEach((image) => {
          newImages.push({imageFile: image.image, damageReportId: damageReportId})
        })
        console.log(newImages);
        await imageService.uploadImages(newImages);
    }
    catch (error) {
        console.log(error);
    }
  }
}

const damageReportService = new DamageReportService();
export default damageReportService;
