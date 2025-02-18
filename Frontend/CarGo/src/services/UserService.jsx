import axiosClient from "../axiosClient.jsx";
import roleService from "./RoleService.jsx";

class UserService {
  async registerUser(user) {
    try {
      const response = await axiosClient.post("/api/Auth/register", {
        fullName: user.fullName,
        phoneNumber: user.phoneNumber,
        email: user.email,
        password: user.password,
      });
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }

  async loginUser(email, password) {
    try {
      const response = await axiosClient.get("/api/Auth/login", {
        params: {
          email: email,
          password: password,
        },
      });
      if (response.data.userId) {
        localStorage.setItem("token", response.data.token);
        localStorage.setItem("userId", response.data.userId);
        const user = await this.getUser(response.data.userId);
        localStorage.setItem("role", user.role);
      }
      return response.data;
    } catch (error) {
      return error.response.data;
    }
  }

  logoutUser() {
    localStorage.clear();
  }

  async getUser(id) {
    try {
      const response = await axiosClient.get(`/api/User/${id}`);
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }

  async updateUser(id, user) {
    try {
      const response = await axiosClient.put(`/api/User/${id}`, {
        fullName: user.fullName,
        phoneNumber: user.phoneNumber,
        email: user.email,
      });
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }
}

const userService = new UserService();
export default userService;
