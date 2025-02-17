import axiosClient from "../axiosClient";

class RoleService {
  async getRole(roleId) {
    try {
        console.log(roleId)
        const response = await axiosClient.get(`/api/Role/${roleId}`);
        console.log(response);
        return response.data.name;
    }
    catch (error) {
        console.log(error);
    }
  }
}

const roleService = new RoleService();
export default roleService;
