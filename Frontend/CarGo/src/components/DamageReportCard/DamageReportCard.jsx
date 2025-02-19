import { useState } from "react";
import "./DamageReportCard.css";

function DamageReportCard({ damageReport }) {
  const [showImages, setShowImages] = useState(false);

  function handleClick() {
    setShowImages(!showImages);
  }

  console.log(damageReport.images);

  return (
    <div className="damageReportCard">
      <h4>{damageReport.title}</h4>
      <h5>Date: {damageReport.dateCreated}</h5>
      <h5>Driver: {damageReport.driver}</h5>
      <p>{damageReport.description}</p>
      <div className="images">
        <button onClick={handleClick}>
          {showImages ? "Hide Images" : "Show Images"}
        </button>
        {showImages ? (
          <div>
            {damageReport.images.map((image) => (
              <img key={image.id} src={image.imageFile} alt="" />
            ))}
          </div>
        ) : (
          <></>
        )}
      </div>
    </div>
  );
}

export default DamageReportCard;
