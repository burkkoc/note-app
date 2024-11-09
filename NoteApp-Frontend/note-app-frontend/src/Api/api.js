import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7241/api', 
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `${token}`;
  }
  return config;
}, (error) => Promise.reject(error));

export default api;