import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import ContactUs from './components/ContactUs';

export default () => (
  <Layout>
    <Route exact path='/' component={ContactUs} />
  </Layout>
);
