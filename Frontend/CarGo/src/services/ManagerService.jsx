import axiosClient from "../axiosClient";

class ManagerService {

    async getManagerId() {
        try {
        const response = await axiosClient.get(`/api/Manager?userId=${localStorage.getItem("userId")}`);
        return response.data;
        }
        catch (error) {
            console.log(error);
        }
    }

}

const managerService = new ManagerService();
export default managerService;