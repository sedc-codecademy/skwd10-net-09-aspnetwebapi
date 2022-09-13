import { CreateNoteModel, NoteModel } from "../models/notes";
import { NotesApi } from "./base-service";

export async function notes() {
    try {
        const result = await NotesApi.get<NoteModel[]>("api/v1/note/");
        return result.data
    }
    catch (err) {
        console.log(err);
        alert(err);
        return [];
    }
}

export async function create(model: CreateNoteModel) {
    try {
        const result = await NotesApi.post<NoteModel>("api/v1/note/", model);
        return result.data
    }
    catch (err) {
        console.log(err);
        alert(err);
        return null;
    }
}