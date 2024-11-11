import React from 'react';
import {  useSelector } from 'react-redux';
import 'flowbite/dist/flowbite.css';
const Home = () => {
    const {user, claims} = useSelector(state => state.auth);

return (
    <div className="home-comp-cont">
      
      <div className="flex-1 p-4">
        <h1>Hello {user.firstName}!</h1>
        {claims ? (
          
          <ul>
          {Object.entries(claims).map(([key, value]) => (
            <li key={key}>
              <strong>{key}:</strong> {value.toString()}
            </li>
          ))}
        </ul>
      ) : (
        <p>Claim bilgileri yükleniyor...</p>
      )}
        <p>This is your landing page.</p>
      </div>
    </div>
  );
};

export default Home;