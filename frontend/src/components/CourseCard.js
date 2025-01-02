import React from "react";
import { useNavigate } from "react-router-dom";

const CourseCard = ({ course }) => {
    const navigate = useNavigate();

    const handleClick = () => {
        navigate(`/courses/${course.id}`);
    };

    return (
        <div className="card shadow m-3 bg-purple text-white">
            <div className="card-img-wrapper">
                <img
                    src={course.imageUrl}
                    className="card-img-top"
                    alt={course.title}
                />
            </div>
            <div className="card-body">
                <h5 className="card-title fs-3">{course.title}</h5>
                <p className="card-text">{course.description.substring(0, 30)}...</p>
                <p className="card-text fs-5"><strong>Price:</strong> ${course.price}</p>
                <button onClick={handleClick} className="btn bg-yellow border border-dark border-2 text-dark fw-bold">
                    Details
                </button>
            </div>
        </div>
    );
};

export default CourseCard;