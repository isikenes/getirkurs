import axios from "axios";

const api = axios.create({
    baseURL: "https://localhost:7100/api",
});

export const registerUser = (userData) => api.post('/Account/register', userData);
export const loginUser = (loginData) => api.post('/Account/login', loginData);

export const validateToken = async (token) => {
    try {
        const response = await api.get('/Account/validate-token', {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });
        return response.data;
    } catch (error) {
        throw new Error('Token validation failed');
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