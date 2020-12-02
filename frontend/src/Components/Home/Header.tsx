import React from "react";
import { Link } from "react-router-dom";

interface Props {
  Logo: string;
  LogoName: string;
  LogoDesc: string;

  MenuOptions: {
    Name: string;
    Text: string;
  }[];
}

export const Header: React.FC<Props> = ({
  Logo,
  LogoName,
  LogoDesc,
  MenuOptions,
}) => {
  return (
    <header>
      <div className="header-container">
        <img src={Logo} alt={LogoName + " Logo"} width="50px" height="50px" />
        <Link to="/">
          <div className="logo-container">
            <label id="logoName">{LogoName}</label>
            <label id="logoDesc">{LogoDesc}</label>
          </div>
        </Link>

        <ul className="menu-options">
          {MenuOptions.map((o) => {
            return (
              <li key={o.Name}>
                <Link to={o.Name}>{o.Text}</Link>
              </li>
            );
          })}
        </ul>
      </div>
    </header>
  );
};
