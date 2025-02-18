import axios from "axios";

const axiosClient = axios.create({
  baseURL: "http://172.20.8.103:7100", // Set your base URL here
  headers: {
    "Content-Type": "application/json",
    // Add any other headers you need, like auth tokens
  },
});

axiosClient.interceptors.request.use(
  async (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default axiosClient;
