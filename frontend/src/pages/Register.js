import React, {useState} from "react";
import { registerUser } from "../services/api";
import { useNavigate } from "react-router-dom";
import alertify from "alertifyjs";
import "alertifyjs/build/css/alertify.css";

const Register=()=> {
    const [formData, setFormData] = useState({
        displayName: "",
        email: "",
        username:"", 
        password: "",
    });
    const navigate=useNavigate();

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const { displayName, email, username, password } = formData;

        if (!displayName || !email || !username || !password) {
            alertify.error("All fields are required!");
            return;
        }

        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailPattern.test(email)) {
            alertify.error("Please enter a valid email address!");
            return;
        }

        try{
            await registerUser(formData);
            alertify.success("Registration successful!");
            navigate("/login");
        }
        catch(err){
            alertify.error("Registration failed!");
        }
    }

    return (
        <div className="container-fluid d-flex justify-content-center align-items-center bg-purple full-height">
            <div className="row w-100">
                <div className="col-12 col-md-6 col-lg-4 mx-auto">
                    <div className="card shadow-lg p-4">
                        <h1 className="mb-4 text-center">Register</h1>
                        <form onSubmit={handleSubmit}>
                            <div className="form-group">
                                <label>Display Name</label>
                                <input type="text" name="displayName" className="form-control mb-3" onChange={handleChange} />
                            </div>
                            <div className="form-group">
                                <label>Email</label>
                                <input type="email" name="email" className="form-control mb-3" onChange={handleChange} />
                            </div>
                            <div className="form-group">
                                <label>Username</label>
                                <input type="text" name="username" className="form-control mb-3" onChange={handleChange} />
                            </div>
                            <div className="form-group">
                                <label>Password</label>
                                <input type="password" name="password" className="form-control mb-3" onChange={handleChange} />
                            </div>
                            <button type="submit" className="btn btn-primary w-100 mt-2 bg-purple">Register</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Register;