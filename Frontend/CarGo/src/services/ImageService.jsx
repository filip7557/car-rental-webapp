import axiosClient from "../axiosClient";

class ImageService {
  async uploadImages(images) {
    try {
      const response = await axiosClient.post("/api/Image/saveList", images);
    } catch (error) {
      console.log(error);
    }
  }
}

const imageService = new ImageService();
export default imageService;
