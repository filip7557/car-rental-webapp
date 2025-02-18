import axiosClient from "../axiosClient";

class ImageService {
  async uploadImages(images) {
    try {
      const response = await axiosClient.post("/api/Image/saveList", images);
      console.log(response.data);
    } catch (error) {
      console.log(error);
    }
  }
}

const imageService = new ImageService();
export default imageService;
