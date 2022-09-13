import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, TextField } from '@mui/material';
import React from 'react';
import ColorPicker from 'material-ui-color-picker'
import Tags from './tags';
interface INoteDialogProps {
    open: boolean;
    onClose: () => void;
    onCreate: (text: string, color: string, tags: string[]) => void;
}
const NoteDialog = (props: INoteDialogProps) => {
    const [text, setText] = React.useState<string>('');
    const [color, setColor] = React.useState<string>('');
    const [tags, setTags] = React.useState<string[]>([]);

    return (
        <Dialog open={props.open} onClose={props.onClose}>
            <DialogTitle>Subscribe</DialogTitle>
            <DialogContent>
                <TextField
                    autoFocus
                    margin="dense"
                    id="name"
                    label="Text"
                    type="email"
                    fullWidth
                    value={text}
                    multiline={true}
                    minRows={5}
                    variant="standard"
                    onChange={e => setText(e.target.value)}
                />
                <TextField
                    autoFocus
                    type={'color'}
                    margin="dense"
                    label="Color"
                    variant="standard"
                    fullWidth
                    onChange={e => setColor(e.target.value)}
                />
                <Tags tags={tags} addTag={(tag) => setTags([...tags, tag])}/>

            </DialogContent>
            <DialogActions>
                <Button onClick={props.onClose}>Cancel</Button>
                <Button onClick={() => props.onCreate(text, color, tags)}>Create</Button>
            </DialogActions>
        </Dialog>
    )
}

export default NoteDialog;