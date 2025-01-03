import React, { useState, useEffect } from 'react';
import { deleteProfile, getProfile, updatePassword, updateProfile } from '../services/api';
import alertify from 'alertifyjs';
import "alertifyjs/build/css/alertify.css";
import LoadingSpinner from '../components/LoadingSpinner';
import { useNavigate } from 'react-router-dom';
import Modal from 'react-modal';

Modal.setAppElement('#root');

const Profile = () => {
    const [formData, setFormData] = useState({
        displayName: '',
        username: '',
        email: '',
    });

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const [modalIsOpen, setModalIsOpen] = useState(false);
    const [currentPassword, setCurrentPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const [deleteModalIsOpen, setDeleteModalIsOpen] = useState(false);

    useEffect(() => {
        const fetchProfile = async () => {
            try {
                const token = localStorage.getItem('token');
                if (!token) {
                    navigate('/login');
                    return;
                }

                const profile = await getProfile();
                setFormData(profile);
            } catch (error) {
                if (error.response && error.response.status === 401) {
                    navigate('/login');
                } else {
                    setError(error);
                    alertify.error('Failed to fetch profile!');
                }
            } finally {
                setLoading(false);
            }
        };
        fetchProfile();
    }, [navigate]);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await updateProfile(formData);
            alertify.success('Profile updated successfully!');
        } catch (error) {
            alertify.error('Failed to update profile!');
        }
    };

    const openModal = (e) => {
        e.preventDefault();
        setModalIsOpen(true);
    };

    const closeModal = () => {
        setModalIsOpen(false);
    };


    const openDeleteModal = (e) => {
        e.preventDefault();
        setDeleteModalIsOpen(true);
    };

    const closeDeleteModal = () => {
        setDeleteModalIsOpen(false);
    };

    const handlePasswordReset = async (e) => {
        e.preventDefault();
        try {
            await updatePassword({
                currentPassword,
                newPassword,
            });
            alertify.success('Password reset successfully!');
            closeModal();
        } catch (error) {
            alertify.error('Failed to reset password!');
        }
    };

    const handleDeleteProfile = async () => {
        try {
            await deleteProfile();
            alertify.success('Profile deleted successfully!');
            localStorage.removeItem('token');
            navigate('/');
        } catch (error) {
            alertify.error('Failed to delete profile!');
        }
    };

    const customStyles = {
        content: {
            top: '50%',
            left: '50%',
            right: 'auto',
            bottom: 'auto',
            marginRight: '-50%',
            transform: 'translate(-50%, -50%)',
            width: '400px',
            padding: '20px',
            borderRadius: '8px',
        },
    };

    if (loading) return <LoadingSpinner loading={loading} />;
    if (error) return (
        <div className="container full-height d-flex justify-content-center align-items-center">
            <div className="fs-5 color-purple">{error.message}</div>
        </div>
    );

    return (
        <div className="container mt-5 full-height">
            <h1 className="mb-4">Update Profile</h1>
            <form onSubmit={handleSubmit}>
                <div className="form-group mb-3">
                    <label>Display Name</label>
                    <input
                        type="text"
                        name="displayName"
                        className="form-control"
                        value={formData.displayName}
                        onChange={handleChange}
                    />
                </div>
                <div className="form-group mb-3">
                    <label>Username</label>
                    <input
                        type="text"
                        name="username"
                        className="form-control"
                        value={formData.username}
                        onChange={handleChange}
                    />
                </div>
                <div className="form-group mb-3">
                    <label>Email</label>
                    <input
                        type="email"
                        name="email"
                        className="form-control"
                        value={formData.email}
                        onChange={handleChange}
                    />
                </div>
                <div className="d-flex mt-3">
                <button type="submit" className="btn bg-purple text-white me-2">Update Profile</button>
                    <button onClick={openModal} className="btn btn-secondary mx-2">Reset Password</button>
                    <button onClick={openDeleteModal} className="btn btn-danger mx-2">Delete Account</button>
                </div>
                
            </form>



            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                contentLabel="Reset Password Modal"
                style={customStyles}
            >
                <h2>Reset Password</h2>
                <form onSubmit={handlePasswordReset}>
                    <div className="form-group mb-3">
                        <label>Current Password</label>
                        <input
                            type="password"
                            name="currentPassword"
                            className="form-control"
                            value={currentPassword}
                            onChange={(e) => setCurrentPassword(e.target.value)}
                            required
                        />
                    </div>
                    <div className="form-group mb-3">
                        <label>New Password</label>
                        <input
                            type="password"
                            name="newPassword"
                            className="form-control"
                            value={newPassword}
                            onChange={(e) => setNewPassword(e.target.value)}
                            required
                        />
                    </div>
                    <button type="submit" className="btn btn-primary">Submit</button>
                    <button type="button" onClick={closeModal} className="btn btn-secondary mx-2">Cancel</button>
                </form>
            </Modal>

            <Modal
                isOpen={deleteModalIsOpen}
                onRequestClose={closeDeleteModal}
                contentLabel="Delete Profile Confirmation"
                style={customStyles}
            >
                <p className='fw-bold fs-4'>Are you sure you want to delete your account?</p>
                <button onClick={handleDeleteProfile} className="btn btn-danger">Yes, Delete</button>
                <button onClick={closeDeleteModal} className="btn btn-secondary mx-2">Cancel</button>
            </Modal>


        </div>
    );
};

export default Profile;