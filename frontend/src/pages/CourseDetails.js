import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getCourseById } from "../services/api";
import alertify from "alertifyjs";
import "alertifyjs/build/css/alertify.css";
import LoadingSpinner from "../components/LoadingSpinner";

const CourseDetails = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [course, setCourse] = useState(null);
    const [loading, setLoading] = useState(true);
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    
    useEffect(() => {
        const fetchCourse = async () => {
            try {
                const response = await getCourseById(id);
                if(!response) {
                    navigate("/404");
                } else{
                    setCourse(response);
                    setLoading(false);
                }
            } catch (error) {
                console.log(error);
                navigate("/404");
            }
        };

        setIsLoggedIn(!!localStorage.getItem("token"));
        fetchCourse();
    }, [id, navigate]);

    const handlePurchase = () => {
        if (!isLoggedIn) {
            alertify.error("You need to login to purchase!");
            navigate("/login");
            return;
        };

        //todo purchase modal
    }

    if (loading) return <LoadingSpinner loading={loading} />;

    return (
        <div className="container full-height mt-5 px-5">
            <div className="row">

                <div className="col-lg-6 p-3">
                    <img
                        src={course.imageUrl}
                        alt={course.title}
                        className="img-fluid mb-3" 
                        style={{ borderRadius: "10px" }}
                    />
                </div>

                <div className="col-lg-6 p-3 ps-5">
                    <h1 className="mb-1">{course.title}</h1>
                    <p className="text-muted">{course.category}</p>

                    <p className="fs-5 mb-3"><strong>Learn from {course.instructorName}</strong> </p>
                    <hr />
                    <p className="fs-5 my-5">{course.description}</p>
                    <hr />
                    <p className="fs-5">
                        {course.hours} hours on-demand video
                    </p>
                    <p>
                    <h5 className="fw-bold">For only ${course.price}!</h5>
                    </p>

                    <button
                        onClick={handlePurchase}
                        className="btn btn-lg fw-bold bg-yellow btn-block mt-3 px-5 border border-dark border-3 shadow"
                    >
                        Buy Now
                    </button>
                </div>


            </div>
        </div>
    );
};

export default CourseDetails;