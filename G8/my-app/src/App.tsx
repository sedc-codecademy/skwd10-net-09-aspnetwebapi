import React from 'react';
import Navbar from './components/nav-bar';
import { Route, Routes, useNavigate } from 'react-router-dom';
import { AppRoutes } from './consts/app-routes';
import Login from './pages/login/login';
import { User } from './models/user';
import Note from './pages/notes/notes';
import { claims } from './services/user-service';
function App() {
  const [user, setUser] = React.useState<User | null>(null);
  const navigate = useNavigate();
  React.useEffect(() => {
    claims().then(user => {
      if(user){
        setUser(user);
        navigate(AppRoutes.Notes);
      }
      else{
        navigate(AppRoutes.BASE);
      }
    })
  }, []);
  return (
    <>
      <Navbar></Navbar>
      <Routes>
        <Route path={AppRoutes.BASE} element={<Login setUser={setUser}></Login>}>
        </Route>
        {Boolean(user) && <Route path={AppRoutes.Notes} element={<Note></Note>}></Route>}
      </Routes>
    </>
  );
}

export default App;
