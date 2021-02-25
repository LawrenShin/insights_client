import React, {useEffect} from 'react';
import {connect} from 'react-redux';
import {Route, Redirect, useHistory} from 'react-router-dom';

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

const isAuthRedirect = (
  isAuthenticated: boolean,
  Component: React.ElementType,
  props: any
) => (
  isAuthenticated ? (
    <Component {...props} />
  ):(
    <Redirect to="/sign_in" />
  )
);

export const PrivateRoute = (
  {
    isAuthenticated,
    component: Component,
    isPrivate,
    exact,
    path,
  }: Props) => {
  const history = useHistory();

  useEffect(() => {
    // if (isAuthenticated) history.push('/companies');
    // if (!isAuthenticated) history.push('/');
  }, [isAuthenticated]);

  return(
    <Route
      exact={exact}
      path={path}
      component={(props: any) => (
        isPrivate ?
          isAuthRedirect(isAuthenticated, Component, props)
          :
          <Component {...props} />
      )}
    />
  );
}

const mapStateToProps = (state: any) => ({
  // isAuthenticated: !!state.SignInReducer.token,
  isAuthenticated: false,
});

export default connect(mapStateToProps)(PrivateRoute);