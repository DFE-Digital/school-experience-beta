import * as React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { SignIn } from './components/signin';
import { Dashboard } from './components/dashboard';

const routes = () => (
    <Router>
        <Switch>
        <Route exact path='/' component={SignIn} />
        <Route path='/dashboard' component={Dashboard} />
        </Switch>
    </Router>

);

export default routes;