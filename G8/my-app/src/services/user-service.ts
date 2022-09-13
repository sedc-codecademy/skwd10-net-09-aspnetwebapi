import { AxiosResponse } from "axios";
import { User } from "../models/user";
import { NotesApi } from "./base-service";


export async function login(username: string, password: string): Promise<User | null> {
    try{
        const result = await NotesApi.post<User>("api/v1/user/login", { usernameOrEmail: username, password });
        return result.data
    }
    catch(err){
        console.log(err);
        alert(err);
        return null;
    }
}

export async function claims(): Promise<User | null> {
    try{
        const result = await NotesApi.get("api/v1/user/claims");
        return result.data
    }
    catch(err){
        console.log(err);
        alert(err);
        return null;
    }
}