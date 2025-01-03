import axios from "axios";

const api = axios.create({
    baseURL: "https://localhost:7100/api",
    headers: {
        'Content-Type': 'application/json',
    },
});

api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export const registerUser = (userData) => api.post('/Account/register', userData);
export const loginUser = (loginData) => api.post('/Account/login', loginData);

export const validateToken = async () => {
    try {
        const response = await api.get('/Account/validate-token');
        
        if (response.data && response.data.valid) {
            return true;
        } else {
            throw new Error("Invalid token");
        }
    } catch (error) {
        console.error("Token validation failed:", error);
        throw error;
    }
};

export const getAllCourses = async () => {
    try {
        const response = await api.get("/Course");
        return response.data;
    } catch (error) {
        console.error("Error fetching courses:", error);
        throw error;
    }
};

export const getCourseById = async (id) => {
    try {
        const response = await api.get(`/Course/${id}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching course by ID:", error);
        throw error;
    }
};

export const getProfile = async () => {
    try {
        const response = await api.get('/Account/profile');
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const updateProfile = async (userDTO) => {
    try {
        const response = await api.put('/Account/profile', userDTO);
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const updatePassword = async (passwordData) => {
    try {
        const response = await api.put('/Account/update-password', passwordData);
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const deleteProfile = async () => {
    try {
        const response = await api.delete('/Account');
        return response.data;
    } catch (error) {
        throw error;
    }
}