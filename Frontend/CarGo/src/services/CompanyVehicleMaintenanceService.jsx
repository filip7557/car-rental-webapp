import axiosClient from "../axiosClient";

class CompanyVehicleMaintenanceService {
	async getVehicleMaintenance(vehicleId, rpp = 10, pageNumber = 1) {
		try {
			if (!vehicleId) {
				console.error("Greška: vehicleId nije definiran ili je prazan.");
				return [];
			}

			const response = await axiosClient.get(`/api/CompanyVehicleMaintenance`, {
				params: { id: vehicleId, rpp, pageNumber },
			});

			console.log("Podaci o održavanju vozila:", response.data);

			return response.data.data || [];
		} catch (error) {
			console.error(
				"Greška pri dohvaćanju podataka o održavanju vozila:",
				error.response?.data || error
			);
			return [];
		}
	}

	async addMaintenanceReport(data) {
		try {
			const response = await axiosClient.post(
				`/api/CompanyVehicleMaintenance`,
				data
			);
			return response.data;
		} catch (error) {
			console.error(
				"Greška pri dodavanju izvještaja o održavanju:",
				error.response?.data || error
			);
			throw error;
		}
	}
}

export default new CompanyVehicleMaintenanceService();
