import React from "react";
import { Link, useNavigate } from "react-router-dom";
import logo from "../assets/logo.png";

const Navbar = () => {
    const navigate = useNavigate();

    const token = localStorage.getItem("token");
    const isAuthenticated = !!token;

    const handleLogout = () => {
        localStorage.removeItem("token");
        navigate("/login");
    };

    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-darkpurple ">
            <div className="container-fluid">
                <Link className="navbar-brand d-flex align-items-center" to="/">
                    <img
                        src={logo}
                        alt="MyApp Logo"
                        style={{ height: "30px", marginRight: "10px" }}
                    />
                </Link>

                <button
                    className="navbar-toggler"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target="#navbarNav"
                    aria-controls="navbarNav"
                    aria-expanded="false"
                    aria-label="Toggle navigation"
                >
                    <span className="navbar-toggler-icon"></span>
                </button>

                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav text-center ms-auto fw-bold">
                        {!isAuthenticated && (
                            <>
                                <li className="nav-item">
                                    <Link className="nav-link px-2" to="/login">Login</Link>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link px-2" to="/register">Register</Link>
                                </li>
                            </>
                        )}

                        {isAuthenticated && (
                            <>
                                <li className="nav-item">
                                    <Link className="nav-link px-2" to="/profile">Profile</Link>
                                </li>
                                <li className="nav-item d-flex justify-content-center align-items-center">
                                    <button className="btn btn-danger btn-sm nav-link px-2" onClick={handleLogout}>
                                        Logout
                                    </button>
                                </li>
                            </>
                        )}


                    </ul>
                </div>

            </div>
        </nav>
    );
};

export default Navbar;