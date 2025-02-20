import axiosClient from "../axiosClient";

class CompanyVehicleService {
  // currentPage = 1;
  // isAsc = true;
  // currentSort = "Id";
  // sortOrder = "ASC";
  // companyIdFilter = null;
  // vehicleModelFilter = null;
  vehicleModelId = "";
  vehicleMakeId = "";
  vehicleTypeId = "";

  async getVehicleTypes() {
    try {
      const response = await axiosClient.get("/api/VehicleType/vehicleTypes");
      console.log(response.data);
      return response.data;
    } catch (error) {
      console.log(error);
      return [];
    }
  }
  async getVehicleModels() {
    try {
      const response = await axiosClient.get("/api/VehicleModel");
      console.log(response.data);
      return response.data;
    } catch (error) {
      console.log(error);
      return [];
    }
  }

  async getVehicleMakes() {
    try {
      const response = await axiosClient.get("/api/VehicleMakes/vehicleMakes");
      console.log(response.data);
      return response.data;
    } catch (error) {
      console.log(error);
      return [];
    }
  }

  async getCompanyVehicles(vehicleMakeId, vehicleModelId, vehicleTypeId) {
    //this.sortOrder = this.isAsc ? "ASC" : "DESC";
    try {
      if (!vehicleMakeId) vehicleMakeId = null;
      const response = await axiosClient.get("/api/CompanyVehicle", {
        params: {
          vehMakeId: this.vehicleMakeId,
          vehiModId: this.vehicleModelId,
          vehTypeId: this.vehicleTypeId,
		  isActive: true,
        },
      });
      console.log(response.data);
      return response.data;
    } catch (error) {
      console.log(error);
      return [];
    }
  }

  async getCompanyVehicleById(id) {
    try {
      const response = await axiosClient.get(`/api/CompanyVehicle/${id}`);
      console.log(response.data);
      return response.data;
    } catch (error) {
      console.log(error);
      return [];
    }
  }

  async getCompanyVehiclesByCompanyId(id) {
    try {
      const response = await axiosClient.get("/api/CompanyVehicle", {
        params: { companyId: id },
      });
      console.log(response.data);
      return response.data;
    } catch (error) {
      console.log(error);
      return [];
    }
  }

  async deleteCompanyVehicleById(id) {
    try {
      const response = await axiosClient.delete(`/api/CompanyVehicle/${id}`, {
        params: { id: id },
      });
    } catch (error) {
      console.log(error);
    }
  }
}

const companyVehicleService = new CompanyVehicleService();
export default companyVehicleService;
