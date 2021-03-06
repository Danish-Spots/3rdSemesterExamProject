import React from "react";
import { Redirect, Route } from "react-router-dom";

export const ProtectedRoute = ({
  component,
  isAuthenticated,
  ...rest
}: any) => {
  const routeComponent = (props: any) =>
    isAuthenticated ? (
      React.createElement(component, props)
    ) : (
      <Redirect to={{ pathname: "/login" }} />
    );
  return <Route {...rest} render={routeComponent} />;
};
