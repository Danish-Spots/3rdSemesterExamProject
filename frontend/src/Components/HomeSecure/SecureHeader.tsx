import React from "react";
import { Link } from "react-router-dom";
import "../../css/home_secure.scss";

interface Props {
  Logo: string;
  LogoName: string;
  LogoDesc: string;

  MenuOptions: {
    Name: string;
    Text: string;
  }[];
}

export const SecureHeader: React.FC<Props> = ({
  Logo,
  LogoName,
  LogoDesc,
  MenuOptions,
}) => {
  return (
    <div id="NavBar">
      <Link to="/home_secure">
        <div id="LogoContainer">
          <img src={Logo} alt={LogoName + " Logo"} width="50px" height="50px" />
          <div className="logoText">
            <label id="logoName">{LogoName}</label>
            <label id="logoDesc">{LogoDesc}</label>
          </div>
        </div>
      </Link>
      <ul id="Menu">
        {MenuOptions.map((o) => {
          return (
            <li key={o.Name} className="MenuItem">
              <Link to={o.Name}>{o.Text}</Link>
            </li>
          );
        })}
      </ul>
    </div>
  );
};
