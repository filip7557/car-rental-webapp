import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { addReview } from "../../services/AddReviewService";
import "./AddReviewPage.css";
import NavBar from "../../components/NavBar/NavBar";

const AddReview = () => {
  let { id } = useParams();
  const navigate = useNavigate();

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async () => {
    if (!title || !description) {
      alert("Please fill in all fields.");
      return;
    }

    debugger;
    let reviewData = {
      title,
      description,
      bookingId: id,
      createdByUserId: localStorage.getItem("userId"),
    };

    setLoading(true);
    try {
      await addReview(reviewData);
      alert("Review added successfully!");
      navigate("/bookingsPage");
    } catch (error) {
      alert("Error submitting review.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <NavBar />
      <div className="add-review-container">
        <h2 className="review-header">Add Review</h2>
        <h3 className="booking-id">Booking ID: {id}</h3>

        <div className="form-group">
          <label className="form-label">Title:</label>
          <input
            type="text"
            className="input-field"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Enter title"
          />
        </div>

        <div className="form-group">
          <label className="form-label">Description:</label>
          <textarea
            className="textarea-field"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Enter description"
          />
        </div>

        <div className="button-group">
          <button
            className="submit-btn"
            onClick={handleSubmit}
            disabled={loading}
          >
            {loading ? "Submitting..." : "Add Review"}
          </button>

          <button
            className="damage-report-btn"
            onClick={() => navigate(`/addDamageReport/${id}`)}
          >
            Report Damage
          </button>
          <button
            className="cancel-btn"
            onClick={() => navigate("/bookingsPage")}
          >
            Cancel
          </button>
        </div>
      </div>
    </div>
  );
};

export default AddReview;
