import React from 'react';
import { Box, Button, TextField, Typography } from '@mui/material';

interface ILoginFormProps {
    onLogin: (username: string, password: string) => void;
}

const LoginForm = (props: ILoginFormProps) => {
    const [username, setUsername] = React.useState<string>('');
    const [password, setPassword] = React.useState<string>('');

    return (
        <Box
            component="form"
            sx={{
                '& .MuiTextField-root': { m: 1, width: '25ch' },
            }}
            noValidate
            autoComplete="off"
            display="flex"
            justifyContent={"center"}
            alignItems="center"
            minHeight={'80vh'}
        >
            <div>

                <div>
                    <TextField
                        label="Username"
                        variant="filled"
                        onChange={(e) => setUsername(e.target.value)}
                    />
                </div>
                <div>
                    <TextField
                        label="Password"
                        type={"password"}
                        variant="filled"
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>
                <Button onClick={() => props.onLogin(username, password)}>
                    <Typography>
                        Login
                    </Typography>
                </Button>
            </div>
        </Box>);
}

export default LoginForm;