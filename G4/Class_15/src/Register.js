import { useState } from "react";

const Register = (props) => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [confirmedPassword, setConfirmedPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const onSubmit = async (e) => {
    e.preventDefault();
    try {
      const result = await fetch("http://localhost:5052/api/user/register", {
        headers: {
          "Content-Type": "application/json",
        },
        method: "POST",
        body: JSON.stringify({
          firstname: firstName,
          lastname: lastName,
          username: userName,
          password: password,
          confirmpassword: confirmedPassword,
        }),
      });
      const res = await result.json();
      if (result.status === 200) {
        setFirstName("");
        setLastName("");
        setUserName("");
        setPassword("");
        setConfirmedPassword("");
        setErrorMessage("");
        props.successfullRegister("login");
      } else {
        setErrorMessage(res.error);
      }
    } catch (error) {
      console.log(error.message);
      // console.log(error);
    }
  };

  return (
    <form onSubmit={onSubmit}>
      <label htmlFor="firstName">First Name</label>
      <input
        type="text"
        name="firstName"
        id="firstName"
        value={firstName}
        onChange={(e) => setFirstName(e.target.value)}
      />
      <br />
      <label htmlFor="lastName">Last Name</label>
      <input
        type="text"
        name="lastName"
        id="lastName"
        value={lastName}
        onChange={(e) => setLastName(e.target.value)}
      />
      <br />

      <label htmlFor="userName">User Name</label>
      <input
        type="text"
        name="userName"
        id="userName"
        value={userName}
        onChange={(e) => setUserName(e.target.value)}
      />
      <br />
      <label htmlFor="pass">Password</label>
      <input
        type="password"
        name="pass"
        id="pass"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <br />
      <label htmlFor="confpass">Confirm Password</label>
      <input
        type="password"
        name="confpass"
        id="confpass"
        value={confirmedPassword}
        onChange={(e) => setConfirmedPassword(e.target.value)}
      />
      <br />
      <button type="submit">Register</button>
      {errorMessage && <p style={{ color: "red" }}>{errorMessage}</p>}
    </form>
  );
};

export default Register;
