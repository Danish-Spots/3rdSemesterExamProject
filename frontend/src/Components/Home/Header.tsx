import React from "react";

interface Props {
  Logo: string;
  LogoName: string;
  LogoDesc: string;

  MenuOptions: {
    Name: string;
    Text: string;
  }[];

  Callback: (name: string) => undefined;
}

export const Header: React.FC<Props> = ({
  Logo,
  LogoName,
  LogoDesc,
  MenuOptions,
  Callback,
}) => {
  return (
    <header>
      <div className="header-container">
        <img src={Logo} alt={LogoName + " Logo"} width="50px" height="50px" />
        <div
          className="logo-container"
          onClick={() => {
            Callback("home");
          }}
        >
          <label id="logoName">{LogoName}</label>
          <label id="logoDesc">{LogoDesc}</label>
        </div>

        <ul className="menu-options">
          {MenuOptions.map((o) => {
            return (
              <li
                key={o.Name}
                onClick={() => {
                  Callback(o.Name);
                }}
              >
                {o.Text}
              </li>
            );
          })}
        </ul>
      </div>
    </header>
  );
};
