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
      return response.data;
    } catch (error) {
      return error.response.data;
    }
  }
}

const userService = new UserService();
export default userService;
