import axios from "axios";

const NotesApi = axios.create({
    baseURL: "https://localhost:7027",
    withCredentials: true
});
export {
    NotesApi
}