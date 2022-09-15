import React, { useState } from "react";
import "./App.css";
import HomePage from "./Home";
import Login from "./Login";
import Register from "./Register";
import constants from "./constants";

function App() {
  const [selectedPage, setSelectedPage] = useState("login");
  const [loggedUser, setLoggeduser] = useState(null);
  console.log(constants.secret);
  const logout = () => {
    setLoggeduser(null);
    sessionStorage.removeItem("movieToken");
  };

  return (
    <div className="App">
      {!loggedUser ? (
        <div>
          <span onClick={() => setSelectedPage("login")}>Login</span>
          <span onClick={() => setSelectedPage("register")}>Register</span>

          {selectedPage === "login" ? (
            <Login loginUser={setLoggeduser} />
          ) : (
            <Register successfullRegister={setSelectedPage} />
          )}
        </div>
      ) : (
        <HomePage user={loggedUser} logout={logout} />
      )}
      <div></div>
    </div>
  );
}

export default App;
