import React from "react";
import {
  BrowserRouter as Router, Route,
  Switch,
} from "react-router-dom";
import PrivateRoute from './private';
import Greeting from '../pages/greeting';
import NotFound from "../pages/NotFound";
import SearchCompanies from "../pages/searchCompanies";
import Results from "../pages/results";

const AppRouter = () => (
  <Router>
    <Switch>
      <Route exact path='/' component={Greeting} />
      <PrivateRoute exact path={'/searchCompanies'} component={SearchCompanies} />
      <PrivateRoute exact path={'/results'} component={Results} />
      <Route component={NotFound} />
    </Switch>
  </Router>
);

export default (AppRouter);