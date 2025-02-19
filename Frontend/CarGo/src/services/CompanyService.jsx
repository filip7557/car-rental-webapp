import axiosClient from "../axiosClient";

class CompanyRegisterService {
    async createCompanyRequest(companyData) {
        try {
            const response = await axiosClient.post("https://localhost:7100/api/CompanyRequest/new-company-request", companyData);
            return response.data;
        } catch(error) {
            console.log(error)
        }
    }

    async getCompanyRequests() {
        try {
            const response = await axiosClient.get("https://localhost:7100/api/CompanyRequest/get-all-company-requests");
            console.log(response.data);
            return response.data;
        } catch(error) {
            console.error("Error fetching company requests:", error.response?.data || error.message);
            return [];
        }
    }

    async manageCompanyRequests(userId, action) {
        try {
        const response = await axiosClient.put(`https://localhost:7100/api/CompanyRequest/manage-company-request/${userId}`, action);
        return response.data;
        } catch (error) {
            console.log(error)
        }
    }

    async adminCreateCompany(company){
        try{
            const response = await axiosClient.post(`https://localhost:7100/api/Company/create-company-by-admin`, company);
            return response.data;
        } catch(error) {
            console.log(error)
        }
    }

}

export default new CompanyRegisterService()