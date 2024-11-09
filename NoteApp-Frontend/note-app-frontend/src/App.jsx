import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import Login from './components/Login';

const App = () => {
  const dispatch = useDispatch();
  const { user } = useSelector((state) => state.auth);

  return (
    <div>
      {user ? (
        <div>
          <h2>Hoş geldin, {user}!</h2>
          <button onClick={() => dispatch(logout())}>Çıkış Yap</button>
        </div>
      ) : (
        <Login />
      )}
    </div>
  );
};

export default App;
