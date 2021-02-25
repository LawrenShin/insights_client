import React from "react";
import {
  BrowserRouter as Router, Route,
  Switch,
} from "react-router-dom";
import PrivateRoute from './private';
import Greeting from '../pages/Greeting';
import NotFound from "../pages/NotFound";

const AppRouter = () => (
  <Router>
    <Switch>
      <PrivateRoute exact path='/' component={Greeting} />
      <Route component={NotFound} />
    </Switch>
  </Router>
);

export default (AppRouter);