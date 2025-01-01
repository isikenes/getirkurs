import React, { useState } from "react";
import { loginUser } from "../services/api";
import alertify from "alertifyjs";
import "alertifyjs/build/css/alertify.css";

const Login = () => {
    const [formData, setFormData] = useState({
        email: "",
        password: "",
    });

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

        } catch (err) {
            alertify.error("Login failed!");
        }
    };

    return (
        <div className="container-fluid d-flex justify-content-center align-items-center bg-purple" style={{ height: "100vh" }}>
            <div className="card shadow p-4">
                <h1 className="mb-4">Login</h1>
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label>Email</label>
                        <input type="email" name="email" className="form-control mb-3" onChange={handleChange} />
                    </div>
                    <div className="form-group">
                        <label>Password</label>
                        <input type="password" name="password" className="form-control mb-3" onChange={handleChange} />
                    </div>
                    <button type="submit" className="btn btn-primary mt-2 bg-purple">Login</button>
                </form>
            </div>
        </div>
    );
};

export default Login;