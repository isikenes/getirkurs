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