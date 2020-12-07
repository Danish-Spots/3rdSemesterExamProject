import Axios, { AxiosError, AxiosResponse } from "axios";
import { observer } from "mobx-react-lite";
import React, { FormEvent, useContext, useState } from "react";
import { RouteComponentProps, withRouter } from "react-router-dom";
import "../../css/login.scss";
import { UserStoreContext } from "../../stores/UserStore";

interface Props extends RouteComponentProps {}

const LoginMain: React.FC<Props> = observer((props) => {
  const userStore = useContext(UserStoreContext);

  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  const AttemptLogin = async () => {
    Axios.get(
      `https://fevr.azurewebsites.net/api/Users/login/${username}/${password}`
    )
      .then((response: AxiosResponse) => {
        userStore.isLoading = false;
        if (response.status === 200) {
          console.log("response data", response.data);
          sessionStorage.setItem("SessionKey", response.data.sessionKey);
          sessionStorage.setItem("Username", username);
          sessionStorage.setItem("ProfileID", response.data.profileID);

          userStore.change_login(
            false,
            true,
            username,
            response.data.sessionKey,
            response.data.profileID
          );
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
          placeholder="username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <input
          type="password"
          name="password"
          placeholder="password"
          id="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button type="submit" id="SignIn">
          Sign in
        </button>
      </form>
    </div>
  );
});

export default withRouter(LoginMain);
