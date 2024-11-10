import React from 'react';
import { useSelector } from 'react-redux';
import 'flowbite/dist/flowbite.css';
const Home = () => {
    const {user} = useSelector(state => state.auth);

return (
    <div className="home-comp-cont">
      
      <div className="flex-1 p-4">
        <h1>Hello {user.firstName}</h1>
      </div>
    </div>
  );
};

export default Home;