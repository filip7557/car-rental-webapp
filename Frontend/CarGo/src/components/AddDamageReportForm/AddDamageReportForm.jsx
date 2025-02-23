import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "./AddDamageReportForm.css";
import damageReportService from "../../services/DamageReportService";

function AddDamageReportForm({ bookingId }) {
  const [damageReport, setDamageReport] = useState({
    title: "",
    description: "",
    bookingId: bookingId,
  });
  const [images, setImages] = useState([]);
  const [image, setImage] = useState("");
  const [index, setIndex] = useState(1);
  const navigate = useNavigate();

  useEffect(() => {
    const userId = localStorage.getItem("userId");
    if (!userId) navigate("/login");
  }, []);

  function handleChange(e) {
    setDamageReport({ ...damageReport, [e.target.name]: e.target.value });
  }

  function handleSubmit(e) {
    e.preventDefault();
    if (damageReport.title === "") alert("Title field must be filled.");
    else {
      damageReportService.createDamageReport(damageReport, images).then(() => {
        navigate(-1);
      });
    }
  }

  function handleCancelClick(e) {
    e.preventDefault();
    setDamageReport({ ...damageReport, title: "" });
    setDamageReport({ ...damageReport, description: "" });
    navigate(-1);
  }

  function handleImageChange(event) {
    const reader = new FileReader();

    reader.onload = function (e) {
      setImages([...images, { image: e.target.result, index: index }]);
      setIndex(index + 1);
    };

    const files = event.target.files;
    for (const file of files) {
      reader.readAsDataURL(file);
    }
  }

  function handleRemoveClick(e, index) {
    e.preventDefault();
    setImages((prevImages) =>
      prevImages.filter((image) => image.index !== index)
    );
  }

  return (
    <div className="addDamageReportForm">
      <form onSubmit={handleSubmit}>
        <table className="addDamageReportFormTable">
          <tbody>
            <tr>
              <td>Title:</td>
              <td>
                <input
                  type="text"
                  name="title"
                  value={damageReport.title || ""}
                  onChange={handleChange}
                  placeholder="Title"
                />
              </td>
            </tr>
            <tr>
              <td>Description:</td>
              <td>
                <input
                  type="text"
                  name="description"
                  value={damageReport.description || ""}
                  onChange={handleChange}
                  placeholder="Description"
                />
              </td>
            </tr>
          </tbody>
        </table>
        <div className="images" key={images.length}>
          {images.map((image) => (
            <div key={image.index}>
              <img src={image.image} alt="image" />
              <br />
              <button onClick={(e) => handleRemoveClick(e, image.index)}>
                Remove
              </button>
            </div>
          ))}
        </div>
        <input
          type="file"
          name="image"
          accept="image/*"
          onChange={handleImageChange}
          value={image}
        />
        <div>
          <button>Save</button>
          <button onClick={handleCancelClick}>Cancel</button>
        </div>
      </form>
    </div>
  );
}

export default AddDamageReportForm;
