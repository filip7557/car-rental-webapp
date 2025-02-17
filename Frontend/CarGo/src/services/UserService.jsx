import axiosClient from "../axiosClient.jsx";

class UserService {
  async registerUser(user) {
    try {
      const response = await axiosClient.post("/api/Auth/register", { user });
      console.log(response);
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }
}

const userService = new UserService();
export default userService;
