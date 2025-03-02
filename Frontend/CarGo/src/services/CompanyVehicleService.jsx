import axiosClient from "../axiosClient";

class CompanyVehicleService {
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

	async getCompanyVehicles(
		pageNumber,
		pageSize,
		orderBy,
		vehicleMakeId = null
	) {
		//this.sortOrder = this.isAsc ? "ASC" : "DESC";
		try {
			if (!vehicleMakeId) vehicleMakeId = null;
			const response = await axiosClient.get("/api/CompanyVehicle", {
				params: {
					pageNumber: pageNumber,
					rpp: pageSize,
					orderBy,
					// PageNumber: this.PageNumber,
					vehMakeId: this.vehicleMakeId,
					vehiModId: this.vehicleModelId,
					vehTypeId: this.vehicleTypeId,
				},
			});
			console.log(response.data);
			return response.data;
		} catch (error) {
			console.log(error);
			return [];
		}
	}

	async getAvailableCompanyVehicles(
		pageNumber,
		pageSize,
		orderBy,
		vehicleMakeId = null
	) {
		//this.sortOrder = this.isAsc ? "ASC" : "DESC";
		try {
			if (!vehicleMakeId) vehicleMakeId = null;
			const response = await axiosClient.get("/api/CompanyVehicle/available", {
				params: {
					pageNumber: pageNumber,
					rpp: pageSize,
					orderBy,
					// PageNumber: this.PageNumber,
					vehMakeId: this.vehicleMakeId,
					vehiModId: this.vehicleModelId,
					vehTypeId: this.vehicleTypeId,
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
}

const companyVehicleService = new CompanyVehicleService();
export default companyVehicleService;
