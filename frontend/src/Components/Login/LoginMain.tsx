import Axios, { AxiosError, AxiosResponse } from "axios";
import React, { FormEvent, useContext, useState } from "react";
import { RouteComponentProps, Router, withRouter } from "react-router-dom";
import { isPropertySignature } from "typescript";
import "../../css/login.scss";

interface Props extends RouteComponentProps {}

const LoginMain: React.FC<Props> = (props) => {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  const AttemptLogin = async () => {
    Axios.get(
      `https://fevr.azurewebsites.net/api/Users/login/${username}/${password}`
    )
      .then((response: AxiosResponse) => {
        if (response.status === 200) {
          console.log("response data", response.data);
          sessionStorage.setItem("SessionKey", response.data);
          props.history.push("/home_secure");
        }
      })
      .catch((error: AxiosError) => {
        console.log("An error occured while trying to login", error);
      });
  };

  const handleSubmit = (evt: FormEvent) => {
    evt.preventDefault();
    AttemptLogin();
  };

  return (
    <div id="LoginBox">
      <h2>Sign In</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="username"
          id="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <input
          type="password"
          name="password"
          id="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <input type="submit" id="SignIn" value="Sign In" />
      </form>
    </div>
  );
};

export default withRouter(LoginMain);
