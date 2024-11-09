import React from 'react';
import Login from './components/Login';
import Home from './components/HomeComp'
import { useSelector } from 'react-redux';
import { Navigate } from "react-router-dom";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

const App = () => {
  const { user, token } = useSelector((state) => state.auth);
  return (
    <Router>

      <Routes>
        <Route path="/login" element={<Login />} />
        <Route
          path="/home"
          element={
            ( user || token)? (
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
      </Routes>

    </Router>
  );
};

export default App;
