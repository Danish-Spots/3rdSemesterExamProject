import React, { useEffect, useState } from "react";
import Axios, { AxiosResponse, AxiosError } from "axios";
import "./css/home.scss";

import { HomeMain } from "./Components/Home/HomeMain";
import { Header } from "./Components/Home/Header";

import Logo from "./imgs/logo.png";

const App: React.FC = () => {
  const [infoData, setInfoData] = useState<
    { Name: string; Title: string; Value: string }[]
  >([
    { Name: "testcount", Title: "Total number of tests", Value: "0" },
    { Name: "fevercount", Title: "Fever detected", Value: "0" },
    { Name: "nofevercount", Title: "Negative tests", Value: "0" },
    { Name: "testcounttoday", Title: "Tests today", Value: "0" },
    {
      Name: "fevercounttoday",
      Title: "Fever detected today",
      Value: "No data",
    },
    {
      Name: "highesttemptoday",
      Title: "Highest temperature today",
      Value: "No data",
    },
    { Name: "highesttemp", Title: "Highest temperature", Value: "No data" },
    {
      Name: "mostfeverslocation",
      Title: "Most fevers at",
      Value: "No location",
    },
  ]);

  const [headerLinks] = useState<{ Text: string; Name: string }[]>([
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
  ]);

  const [currentPage, setCurrentPage] = useState<string>("home");

  function headerLinkOnClick(name: string) {
    setCurrentPage(name);
    return undefined;
  }

  useEffect(() => {
    if (currentPage === "home") {
      let testCount: string = "0";
      let feverCount: string = "0";
      let noFeverCount: string = "0";
      Promise.all([
        Axios.get("https://fevr.azurewebsites.net/TestCount")
          .then((response: AxiosResponse) => {
            testCount = response.data;
          })
          .catch((error: AxiosError) => {
            console.log(error);
          }),
        Axios.get("https://fevr.azurewebsites.net/FeverCount")
          .then((response: AxiosResponse) => {
            feverCount = response.data;
          })
          .catch((error: AxiosError) => {
            console.log(error);
          }),
        Axios.get("https://fevr.azurewebsites.net/NoFeverCount")
          .then((response: AxiosResponse) => {
            noFeverCount = response.data;
          })
          .catch((error: AxiosError) => {
            console.log(error);
          }),
      ]).finally(() => {
        setInfoData(
          infoData.map((o) => {
            switch (o.Name) {
              case "testcount":
                return { ...o, Value: testCount };
              case "fevercount":
                return { ...o, Value: feverCount };
              case "nofevercount":
                return { ...o, Value: noFeverCount };
            }
            return o;
          })
        );
      });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [currentPage]);

  function getCurrentPage() {
    switch (currentPage) {
      case "home":
        return <HomeMain InfoData={infoData} />;

      case "login":
        return <h1 style={{ marginTop: "100px" }}>Log in</h1>;

      case "whatisfevr":
        return <h1 style={{ marginTop: "100px" }}>What is FevR</h1>;

      case "becomeacustomer":
        return <h1 style={{ marginTop: "100px" }}>Become a customer</h1>;
    }
  }

  return (
    <div>
      <Header
        Logo={Logo}
        LogoName="fevR"
        LogoDesc="The professional fever detecting system"
        Callback={headerLinkOnClick}
        MenuOptions={headerLinks}
      />
      {getCurrentPage()}
    </div>
  );
};

export default App;
