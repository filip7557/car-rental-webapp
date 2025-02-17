import axiosClient from "../axiosClient.jsx";

class UserService {
  async registerUser(user) {
    try {
      const response = await axiosClient.post("/api/Auth/register", {
        fullName: user.fullName,
        phoneNumber: user.phoneNumber,
        email: user.email,
        password: user.password,
      });
      console.log(response);
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }
}

const userService = new UserService();
export default userService;
