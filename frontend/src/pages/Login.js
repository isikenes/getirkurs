import React, { useState } from "react";
import { loginUser } from "../services/api";
import alertify from "alertifyjs";
import "alertifyjs/build/css/alertify.css";
import { useNavigate } from "react-router-dom";

const Login = () => {
    const [formData, setFormData] = useState({
        email: "",
        password: "",
    });

    const navigate = useNavigate();

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const { email, password } = formData;

        if (!email || !password) {
            alertify.error("All fields are required!");
            return;
        }

        try {
            const response = await loginUser(formData);
            alertify.success("Login successful!");
            localStorage.setItem("token", response.data.token);
            navigate("/");


        } catch (err) {
            alertify.error("Login failed!");
        }
    };

    return (
        <div className="container-fluid d-flex justify-content-center align-items-center bg-purple full-height">
            <div className="row w-100">
                <div className="col-12 col-md-3 col-lg-3 mx-auto">
                    <div className="card shadow-lg p-4">
                        <h1 className="mb-4 text-center">Login</h1>
                        <form onSubmit={handleSubmit}>
                            <div className="form-group">
                                <label>Email</label>
                                <input type="email" name="email" className="form-control mb-3" onChange={handleChange} />
                            </div>
                            <div className="form-group">
                                <label>Password</label>
                                <input type="password" name="password" className="form-control mb-3" onChange={handleChange} />
                            </div>
                            <button type="submit" className="btn btn-primary w-100 mt-2 bg-purple">Login</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Login;