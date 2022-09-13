import React from 'react';
import { Toolbar, AppBar, Box, Typography, Button } from '@mui/material';

interface INavBarProps {

}
const NavBar = (props: INavBarProps) => {
    return (
        <AppBar position="static">
            <Toolbar>
                <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                    News
                </Typography>
                <Button color="inherit">Login</Button>
            </Toolbar>
        </AppBar>
    )
}

export default NavBar;