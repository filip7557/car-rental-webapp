import axiosClient from "../axiosClient";

class VehicleService {
	async getVehicleModels() {
		try {
			const response = await axiosClient.get("/api/VehicleModel");
			return response.data;
		} catch (error) {
			console.error("Greška pri dohvaćanju modela vozila:", error);
			return [];
		}
	}

	async getCarColors() {
		try {
			const response = await axiosClient.get("/api/CarColor/carColors");
			return response.data;
		} catch (error) {
			console.error("Greška pri dohvaćanju boja vozila:", error);
			return [];
		}
	}

	async getLocations() {
		try {
			const response = await axiosClient.get("/Location");
			return response.data;
		} catch (error) {
			console.error("Greška pri dohvaćanju lokacija:", error);
			return [];
		}
	}

	async getCompanies() {
		try {
			const response = await axiosClient.get("/api/Company/get-all-companyes");
			return response.data;
		} catch (error) {
			console.error("Greška pri dohvaćanju kompanija:", error);
			return [];
		}
	}

	async addVehicle(
		vehicleModelId,
		dailyPrice,
		colorId,
		plateNumber,
		imageUrl,
		currentLocationId,
		isOperational,
		isActive,
		companyId
	) {
		try {
			const payload = {
				id: crypto.randomUUID(),
				vehicleModelId,
				dailyPrice: dailyPrice ? parseFloat(dailyPrice) : undefined,
				colorId,
				plateNumber,
				imageUrl,
				currentLocationId,
				isOperational,
				isActive,
				companyId,
			};

			const filteredPayload = Object.fromEntries(
				Object.entries(payload).filter(
					([_, value]) => value !== undefined && value !== null && value !== ""
				)
			);

			await axiosClient.post("/api/CompanyVehicle", filteredPayload);
			return { success: true };
		} catch (error) {
			console.error(
				"Greška pri dodavanju vozila:",
				error.response?.data || error
			);
			return { success: false, message: "Greška prilikom dodavanja vozila." };
		}
	}
}

export default new VehicleService();
