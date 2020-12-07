import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { UserStoreContext } from "../../stores/UserStore";

interface Props extends RouteComponentProps {}

const LoginMain: React.FC<Props> = observer((props) => {
  const userStore = useContext(UserStoreContext);

  useEffect(() => {
    sessionStorage.removeItem("SessionKey");
    sessionStorage.removeItem("Username");
    sessionStorage.removeItem("ProfileID");
    userStore.change_setDefault();

    props.history.push("/");
  }, []);

  return (
    <div id="LoginBox">
      <p>Logging Out</p>
    </div>
  );
});

export default withRouter(LoginMain);
