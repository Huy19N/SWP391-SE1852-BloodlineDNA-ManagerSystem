import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7722/api/",
});

export default api;
