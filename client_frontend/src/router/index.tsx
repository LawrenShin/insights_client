import React from "react";
import {
  BrowserRouter as Router, Route,
  Switch,
} from "react-router-dom";
import PrivateRoute from './private';
import Greeting from '../pages/Greeting';
import NotFound from "../pages/NotFound";
import SearchCompanies from "../pages/SearchCompanies";

const AppRouter = () => (
  <Router>
    <Switch>
      <Route exact path='/' component={Greeting} />
      <PrivateRoute exact path={'/searchCompanies'} component={SearchCompanies} />
      <Route component={NotFound} />
    </Switch>
  </Router>
);

export default (AppRouter);