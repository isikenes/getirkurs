import React, { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";
import { validateToken } from "../services/api";
import LoadingSpinner from "./LoadingSpinner";

const ProtectedRoute = ({ children }) => {
    const [isValid, setIsValid] = useState(null);

    useEffect(() => {
        const checkToken = async () => {
            const token = localStorage.getItem("token");
            if (!token) {
                setIsValid(false);
                return;
            }

            try {
                const response = await validateToken(token);
                setIsValid(response.isValid || true);
            } catch (err) {
                setIsValid(false);
            }
        };

        checkToken();
    }, []);

    if (isValid === null) return (
        <div className="container full-height d-flex justify-content-center align-items-center">
            <LoadingSpinner loading={true} />
        </div>
    )

    return isValid ? children : <Navigate to="/login" />;
};

export default ProtectedRoute;