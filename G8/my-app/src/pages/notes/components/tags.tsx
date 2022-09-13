import { Box, Chip, TextField } from '@mui/material';
import React from 'react';
interface ITagsProps {
    addTag: (tag: string) => void;
    tags: string[];
}
const Tags = (props: ITagsProps) => {
    const [tag, setTagValue] = React.useState('');
    return (
        <>
            <TextField
                autoFocus
                margin="dense"
                label="Tags"
                variant="standard"
                fullWidth
                value={tag}
                onChange={(e) => setTagValue(e.target.value)}
                onKeyPress={e => {
                    if (e.key === 'Enter') {
                        //@ts-ignore
                        props.addTag(e.target.value)
                        setTagValue('');
                        e.preventDefault();
                    }
                }}
            />
            {props.tags.map(x => <Chip key={x} label={x} />)}
        </>)
}
export default Tags;