import React, { useContext } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import { HomeMain } from "./Components/Home/HomeMain";
import { Header } from "./Components/Header/Header";

import Logo from "./imgs/logo.png";
import LoginMain from "./Components/Login/LoginMain";
import { ProtectedRoute } from "./Components/ProtectedRoute";
import { HomeSecureMain } from "./Components/HomeSecure/HomeSecureMain";
import { DevicesMain } from "./Components/Devices/DevicesMain";
import { MapViewMain } from "./Components/MapView/MapViewMain";
import WhatIsFevR from "./Components/WhatIsFevR/WhatIsFevR";
import { ProfileMain } from "./Components/Profile/ProfileMain";
import LogoutMain from "./Components/Logout/LogoutMain";
import { UserStoreContext } from "./stores/UserStore";
import { observer } from "mobx-react-lite";
import { ListViewMain } from "./Components/ListView/ListViewMain";

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

  const secureHeaderLinks = [
    {
      Name: "",
      Text: "Dashboard",
    },
    {
      Name: "devices",
      Text: "Devices",
    },
    {
      Name: "listview",
      Text: "Data Records",
    },
    {
      Name: "mapview",
      Text: "Map View",
    },
    {
      Name: "profile",
      Text: "Profile",
    },
    {
      Name: "logout",
      Text: "Logout",
    },
  ];

  return (
    <Router>
      <div>
        <Header
          Logo={Logo}
          LogoName="fevR"
          LogoDesc="The professional fever detecting system"
          MenuOptions={userStore.isLoggedIn ? secureHeaderLinks : headerLinks}
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
          <Route path="/listview">
            <ListViewMain />
          </Route>
          <Route path="/becomeacustomer">
            <h1 style={{ marginTop: "100px" }}>Become a customer</h1>
          </Route>
          <ProtectedRoute
            path="/home_secure"
            isAuthenticated={userStore.isLoggedIn}
            component={HomeSecureMain}
          />
          <ProtectedRoute
            path="/devices"
            isAuthenticated={userStore.isLoggedIn}
            component={DevicesMain}
          />
          <ProtectedRoute
            path="/datarecords"
            isAuthenticated={userStore.isLoggedIn}
            component={HomeSecureMain}
          />
          <ProtectedRoute
            path="/mapview"
            isAuthenticated={userStore.isLoggedIn}
            component={MapViewMain}
          />
          <ProtectedRoute
            path="/logout"
            isAuthenticated={userStore.isLoggedIn}
            component={LogoutMain}
          />
        </Switch>
      </div>
    </Router>
  );
});

export default App;
