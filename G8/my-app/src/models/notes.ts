
export interface NoteModel{
    id: number;
    text: string;
    color:string;
    tag?: string;
    username:string; 
}

export interface CreateNoteModel{
    text: string;
    color:string;
    tag?: string;
}