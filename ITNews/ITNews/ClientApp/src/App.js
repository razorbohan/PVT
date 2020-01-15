import React, { Component } from 'react';
import { Route } from 'react-router';
import { Redirect } from 'react-router-dom'
import { Layout } from './containers/Layout';
import { Home } from './containers/Home/Home';
import { News } from './containers/News/News';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path='/news/category/:category' component={Home} />
        <Route exact path='/news/tag/:tag' component={Home} />
        <Route exact path='/news/:id' component={News} />
        {/* <Redirect exact from='/news' to='/' /> */}
        <Route exact path='/' component={Home} />
        {/* <AuthorizeRoute path='/fetch-data' component={FetchData} /> */}
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout>
    );
  }
}
