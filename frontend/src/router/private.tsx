import React from 'react';
import {connect} from 'react-redux';
import {Route, Redirect, useHistory} from 'react-router-dom';
// import MenuComponent from '../components/menuComponent';
// import LayoutComponent from '../components/layoutComponent';

interface State {
  isAuthenticated: boolean;
}

interface OwnProps {
  component: React.FC<any>;
  isPrivate?: boolean;
  exact?: boolean,
  path?: string | string[],
}

interface Props extends State, OwnProps {};

export const PrivateRoute = (
  {
    isAuthenticated,
    component: Component,
    isPrivate,
    exact,
    path,
  }: Props) => (
  <Route
    exact={exact}
path={path}
component={(props: any) => (
  isPrivate ?
    (isAuthenticated ? ( null
      // <LayoutComponent
      //     menu={() => <MenuComponent />}
      //     content={() => <Component {...props} />}
      // />
    ):(
      <Redirect to="/sign_in" />
    ))
    :
    <>
      <Component {...props} />
</>
)}
/>
);

const mapStateToProps = (state: any) => ({
  isAuthenticated: !!state?.auth?.data
});

export default connect(mapStateToProps)(PrivateRoute);