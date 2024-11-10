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
              (token) ? (
                <NotesComp page={<NotesComp />} headerName="Notes" />
              ) : (
                <Navigate to="/login" />
              )
            }
          />
        </Routes>
      </Layout>
    </Router>
  );
};

export default App;
