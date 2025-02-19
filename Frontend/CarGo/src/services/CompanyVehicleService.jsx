import axiosClient from "../axiosClient";

class CompanyVehicleService {
	// currentPage = 1;
	// isAsc = true;
	// currentSort = "Id";
	// sortOrder = "ASC";
	// companyIdFilter = null;
	// vehicleModelFilter = null;

	async getCompanyVehicles() {
		//this.sortOrder = this.isAsc ? "ASC" : "DESC";
		try {
			const response = await axiosClient.get("/api/CompanyVehicle");
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
}

const companyVehicleService = new CompanyVehicleService();
export default companyVehicleService;
