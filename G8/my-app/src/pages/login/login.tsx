import React from 'react';
import { User } from '../../models/user';
import { login } from '../../services/user-service';
import LoginForm from './components/login-form';
import { useNavigate } from 'react-router-dom';
import { AppRoutes } from '../../consts/app-routes';
interface ILoginProps {
    setUser: (user: User) => void;
}

const Login = (props: ILoginProps) => {
    const navigate = useNavigate();
    const onLogin = (username: string, password: string) => {
        login(username, password).then(x => {
            if (x !== null) {
                props.setUser(x);
                navigate(AppRoutes.Notes);
            }
        })
    }
    return (
        <LoginForm onLogin={onLogin}></LoginForm>
    )
}

export default Login;