import React, { Component } from 'react';
import { Route } from 'react-router';
import { Redirect, Switch } from 'react-router-dom'
import { Layout } from './containers/Layout';
import  Home  from './containers/Home/Home';
import  News  from './containers/News/News';
import  Admin from './containers/Admin/Admin';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Switch>
          <Route exact path='/news/category/:category' component={Home} />
          <Route exact path='/news/tag/:tag' component={Home} />
          <Route exact path='/news/date/:date' component={Home} />
          <Route exact path='/news/search/:search?' component={Home} />
          <Route exact path='/news/:id' component={News} />
          <Route exact path='/admin' component={Admin} />
          <Redirect exact from='/news' to='/' />
          <Route exact path='/' component={Home} />
          {/* <AuthorizeRoute path='/fetch-data' component={FetchData} /> */}
          <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
        </Switch>
      </Layout>
    );
  }
}
