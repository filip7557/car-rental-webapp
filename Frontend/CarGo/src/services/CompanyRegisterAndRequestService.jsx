import axiosClient from "../axiosClient";

class CompanyRegisterService {
    async createCompanyRequest(companyData) {
        const response = await axiosClient.post("https://localhost:7100/api/CompanyRequest/new-company-request", companyData);
        return response.data;
    }

    async getCompanyRequests() {
        const response = await axiosClient.get("https://localhost:7100/api/CompanyRequest/get-all-company-requests");
        console.log(response.data);
        return response.data;
    }

    async manageCompanyRequests(userId, action) {
        const response = await axiosClient.put("https://localhost:7100/api/CompanyRequest/manage-company-request", userId, action);
        return response.data;
    }
}

export default new CompanyRegisterService()