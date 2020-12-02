import React, { useEffect, useRef } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import { HomeMain } from "./Components/Home/HomeMain";
import { Header } from "./Components/Home/Header";

import Logo from "./imgs/logo.png";
import LoginMain from "./Components/Login/LoginMain";
import { ProtectedRoute } from "./Components/ProtectedRoute";

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
  const hasSessionKey = useRef(false);

  useEffect(() => {
    hasSessionKey.current = sessionStorage.getItem("SessionKey") !== null;
  });

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
            isAuthenticated={hasSessionKey.current}
            component={HomeMain}
          ></ProtectedRoute>
        </Switch>
      </div>
    </Router>
  );
};

export default App;
