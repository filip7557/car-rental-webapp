import axiosClient from "../axiosClient";

class CompanyService {
    async createCompanyRequest(companyData) {
        try {
            const response = await axiosClient.post("/api/CompanyRequest/new-company-request", companyData);
            return response.data;
        } catch(error) {
            console.log(error)
        }
    }

    async getCompanyRequests(isActive = true) {
        try {
            const response = await axiosClient.get("/api/CompanyRequest/get-all-company-requests", isActive);
            console.log(response.data);
            return response.data || [];
        } catch(error) {
            console.error("Error fetching company requests:", error.response?.data || error.message);
            return [];
        }
    }

    async manageCompanyRequests(userId, action) {
        try {
        const response = await axiosClient.put(`/api/CompanyRequest/manage-company-request/${userId}`, action);
        return response;
        } catch (error) {
            console.log(error)
        }
    }

    async adminCreateCompany({company, user}){
        try{
            const response = await axiosClient.post(`/api/Company/create-company-by-admin`, {company, user});
            return response.data;
        } catch(error) {
            console.log(error)
        }
    }

    async getAllCompanies() {
        try {
            const response = await axiosClient.get("/api/Company/get-all-companyes-admin");
            return response.data || [];
        } catch (error) {
            console.error("Error fetching companies:", error.response?.data || error.message);
            return [];
        }
    }

    async changeCompanyStatus(id, isActive) {
        try {
            const response = await axiosClient.put(`/api/Company/delete-company,${id},${isActive}`);
            return response.data;
        } catch (error) {
            console.error("Error updating company status:", error.response?.data || error.message);
            throw error;
        }
    }

    async getCompanyInfoById(id) {
		try {
			const response = await axiosClient.get(
				`/api/Company/get-company-info-by-id/${id}`
			);
			return response.data;
		} catch (error) {
			console.error("Greška pri dohvaćanju podataka o kompaniji:", error);
			return null;
		}
	}
}

export default new CompanyService();
