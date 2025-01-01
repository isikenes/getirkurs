import React, { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";
import { validateToken } from "../services/api";

const ProtectedRoute = ({ children }) => {
    const [isValid, setIsValid] = useState(false);

    useEffect(() => {
        const checkToken = async () => {
            const token = localStorage.getItem("token");
            if (!token) return setIsValid(false);

            try {
                await validateToken(token);
                setIsValid(true);
            } catch (err) {
                setIsValid(false);
            }
        };
        checkToken();
    }, []);

    if (isValid === null) return <p>Loading...</p>;

    return isValid ? children : <Navigate to="/login" />;
};

export default ProtectedRoute;