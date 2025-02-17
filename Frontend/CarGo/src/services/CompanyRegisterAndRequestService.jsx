import axiosClient from "../axiosClient";

class CompanyRegisterService {
    async createCompanyRequest(companyData) {
        const response = await axiosClient.post("https://localhost:7100/api/CompanyRequest/new-company-request", companyData);
        return response.data;
    }
}

export default new CompanyRegisterService()