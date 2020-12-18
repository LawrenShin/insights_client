import React from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route,
} from "react-router-dom";
// import PrivateRoute from './PrivateRoute';
import SignIn from '../pages/SignIn';
// import NotFound from "../NotFound";

const AppRouter = () => (
  <Router>
    <Switch>
      {/*<Route exact path='/' component={SignIn} isPrivate />*/}
      <Route exact path='/sign_in' component={SignIn} isPrivate={false} />
      {/*<Route exact path='/map' component={Map} isPrivate={false} />*/}
    {/* <Route component={NotFound} /> */}
    </Switch>
  </Router>
);

export default (AppRouter);