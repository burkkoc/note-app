import React from 'react';
import { useSelector } from 'react-redux';
import 'flowbite/dist/flowbite.css';
import NavbarComp from './NavbarComp';
const Home = () => {
    const {user} = useSelector(state => state.auth);

return (
    <div className="home-comp-cont">
      
    <NavbarComp/>
      <div className="flex-1 p-4">
      </div>
    </div>
  );
};

export default Home;