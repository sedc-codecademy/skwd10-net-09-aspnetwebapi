import { Box, Button } from '@mui/material';
import React from 'react';
import { NoteModel } from '../../models/notes';
import { create, notes } from '../../services/notes-services';
import NoteDialog from './components/note-dialog';
import NoteTable from './components/note-table';

interface INoteProps {
}

const Note = (props: INoteProps) => {
    const [notesModel, setNotesModel] = React.useState<NoteModel[]>([]);
    const [open, setOpen] = React.useState<boolean>(false);
    React.useEffect(() => {
        notes().then(notesModels => {
            setNotesModel(notesModels);
        }).catch(x => alert(x));
    }, []);
    const onCreate = (text: string, color: string, tags: string[]) => {
        create({ text, color, tag: tags.join(",") }).then(createdNote => {
            if (createdNote) {
                setNotesModel([...notesModel, createdNote]);
                setOpen(false);
            } else {
                alert("Note was not created");
            }
        })
    }
    return (
        <>
            <Box padding="20px" >
                <Button onClick={() => setOpen(true)}>
                    Create note
                </Button>
                <NoteTable rows={notesModel}></NoteTable>
                <NoteDialog
                    open={open}
                    onClose={() => setOpen(false)}
                    onCreate={onCreate} />
            </Box>
        </>
    )
}

export default Note;