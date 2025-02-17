import axios from 'axios';

const axiosClient = axios.create({
    baseURL: 'https://localhost:7100', // Set your base URL here
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem("token")}`
      // Add any other headers you need, like auth tokens
    },
  });

  export default axiosClient;