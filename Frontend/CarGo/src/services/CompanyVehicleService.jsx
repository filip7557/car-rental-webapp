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
			const response = await axios.get(
				`https://localhost:7100/api/CompanyVehicle/${id}`
			);
			return response.data;
		} catch {
			alert("Greška pri dohvaćanju vozila");
			return [];
		}
	}
}

const companyVehicleService = new CompanyVehicleService();
export default companyVehicleService;
