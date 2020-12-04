import React, { useEffect, useRef, useState } from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  useLocation,
} from "react-router-dom";

import { HomeMain } from "./Components/Home/HomeMain";
import { Header } from "./Components/Home/Header";

import Logo from "./imgs/logo.png";
import LoginMain from "./Components/Login/LoginMain";
import { ProtectedRoute } from "./Components/ProtectedRoute";
import { HomeSecureMain } from "./Components/HomeSecure/HomeSecureMain";
import { SecureHeader } from "./Components/HomeSecure/SecureHeader";
import { DevicesMain } from "./Components/Devices/DevicesMain";
import { MapViewMain } from "./Components/MapView/MapViewMain";

const App: React.FC = () => {
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
  const secureHeaderLinks = [
    {
      Name: "home_secure",
      Text: "Home",
    },
    {
      Name: "devices",
      Text: "Devices",
    },
    {
      Name: "datarecords",
      Text: "Data Records",
    },
    {
      Name: "mapview",
      Text: "Map View",
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
            <h1 style={{ marginTop: "100px" }}>What is fevr</h1>
          </Route>
          <Route path="/becomeacustomer">
            <h1 style={{ marginTop: "100px" }}>Become a customer</h1>
          </Route>
          <ProtectedRoute
            path="/home_secure"
            isAuthenticated={hasSessionKey}
            component={HomeSecureMain}
          />
          <Route
            path="/devices"
            isAuthenticated={hasSessionKey}
            component={DevicesMain}
          />
          <Route
            path="/datarecords"
            isAuthenticated={hasSessionKey}
            component={HomeSecureMain}
          />
          <Route
            path="/mapview"
            isAuthenticated={hasSessionKey}
            component={MapViewMain}
          />
        </Switch>
      </div>
    </Router>
  );
};

export default App;
