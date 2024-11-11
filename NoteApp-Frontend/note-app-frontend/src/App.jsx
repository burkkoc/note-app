import React, { useEffect } from 'react';
import Login from './components/Login';
import Home from './components/HomeComp'
import Member from './components/Member'
import { useSelector } from 'react-redux';
import { Navigate } from "react-router-dom";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { Helmet } from 'react-helmet';
import Layout from './components/Layout';
import "./App.css";
import NotesComp from './components/NotesComp';
import AccessDeniedComp from './components/AccessDeniedComp';
import PermissionModalComp from './components/PermissionModalComp';

const App = () => {
  const { user, token } = useSelector((state) => state.auth);


  return (

    <Router>
      <Helmet>
        <title>NoteApp</title>
      </Helmet>
      <Layout>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route
            path="/home"
            element={
              (user || token) ? (
                <Home page={<Home />} headerName="Home" />
              ) : (
                <Navigate to="/login" />
              )
            }
          />
          <Route
            path="/"
            element={
              (user | token) ? (
                <Home page={<Home />} headerName="Home" />
              ) : (
                <Navigate to="/login" />
              )
            }
          />

          <Route
            path="/member"
            element={
              (token) ? (
                <Member page={<Member />} headerName="Members" />
              ) : (
                <Navigate to="/login" />
              )
            }
          />

          <Route
            path="/notes"
            element={
              token ? (
                <NotesComp pageName="Notes" headerName="Notes" />
              ) : (
                <Navigate to="/login" />
              )
            }
          />

          <Route
            path="/mynotes"
            element={
              token ? (
                <NotesComp pageName="Mynotes" headerName="My Notes" />
              ) : (
                <Navigate to="/login" />
              )
            }
          />

          <Route
            path="/permission"
            element={
              token ? (
                <PermissionModalComp headerName="Permission" />
              ) : (
                <Navigate to="/login" />
              )
            }
          />
          <Route
            path="/accessdenied"
            element={token ? <AccessDeniedComp /> : <Navigate to="/login" />}
          />
        </Routes>
      </Layout>
    </Router>
  );
};

export default App;
