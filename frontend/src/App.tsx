import React, { useContext, useEffect, useState } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import { HomeMain } from "./Components/Home/HomeMain";
import { Header } from "./Components/Home/Header";

import { UserStoreContext } from "./stores/UserStore";

import Logo from "./imgs/logo.png";
import LoginMain from "./Components/Login/LoginMain";
import { ProtectedRoute } from "./Components/ProtectedRoute";
import { HomeSecureMain } from "./Components/HomeSecure/HomeSecureMain";
import { DevicesMain } from "./Components/Devices/DevicesMain";
import { MapViewMain } from "./Components/MapView/MapViewMain";
import WhatIsFevR from "./Components/WhatIsFevR/WhatIsFevR";
import { ProfileMain } from "./Components/Profile/ProfileMain";
import { observer } from "mobx-react-lite";

const App: React.FC = observer(() => {
  const userStore = useContext(UserStoreContext);

  const headerLinks = [
    {
      Name: "login",
      Text: "Log In",
    },
    {
      Name: "whatisfevr",
      Text: "What is FevR",
    },
    {
      Name: "becomeacustomer",
      Text: "Become a customer",
    },
  ];
  const [hasSessionKey, setHasSessionKey] = useState<boolean>(false);

  useEffect(() => {
    setHasSessionKey(sessionStorage.getItem("SessionKey") !== null);
    console.log("Session Key:", hasSessionKey);
  }, []);

  return (
    <Router>
      <div>
        <Header
          Logo={Logo}
          LogoName="fevR"
          LogoDesc="The professional fever detecting system"
          MenuOptions={headerLinks}
        />
        <Switch>
          <Route exact path="/">
            <HomeMain />
          </Route>
          <Route path="/login">
            <LoginMain />
          </Route>
          <Route path="/whatisfevr">
            <WhatIsFevR />
          </Route>
          <Route path="/profile">
            <ProfileMain />
          </Route>
          <Route path="/becomeacustomer">
            <h1 style={{ marginTop: "100px" }}>Become a customer</h1>
          </Route>
          <ProtectedRoute
            path="/home_secure"
            isAuthenticated={userStore.isLoggedIn}
            component={HomeSecureMain}
          />
          <Route
            path="/devices"
            isAuthenticated={userStore.isLoggedIn}
            component={DevicesMain}
          />
          <Route
            path="/datarecords"
            isAuthenticated={userStore.isLoggedIn}
            component={HomeSecureMain}
          />
          <Route
            path="/mapview"
            isAuthenticated={userStore.isLoggedIn}
            component={MapViewMain}
          />
        </Switch>
      </div>
    </Router>
  );
});

export default App;
