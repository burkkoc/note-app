import React from 'react';
import "../assets/styles/Navbar.css";
import { useDispatch, useSelector } from 'react-redux';
import { logout } from '../redux/slices/authSlice';
import { useNavigate } from 'react-router-dom'; 

const NavbarComp = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const handleMembersClick = () => {
    navigate('/member');
  };

  const handleNotesClick = () => {
    navigate('/notes'); 
  };

  const handleLogout = () => {
    dispatch(logout());
    navigate('/login');
  };
  const handleHomeRedirect = () => {
    navigate('/home'); 
  };
  const token = useSelector((state) => state.auth.token);

  return (
    <nav className="bg-gray-900 text-white shadow-lg py-4">
      <div className="container mx-auto flex items-center justify-between px-6" >
        <div className="flex items-center space-x-4 cursor-pointer"onClick={handleHomeRedirect}>
          <img src="../../public/memberLogoMale.png" alt="Logo" className="h-8 w-auto" />
          <span className="text-2xl font-bold">NoteApp</span>
        </div>
        {token && <div className="hidden md:flex space-x-6">
          <a onClick={handleMembersClick} className="text-gray-400 hover:text-gray-200 hover:no-underline transition-colors duration-200 cursor-pointer">
            Members
          </a>
          <a onClick={handleNotesClick} className="text-gray-400 hover:text-gray-200 hover:no-underline transition-colors duration-200 cursor-pointer">
            Notes
          </a>
          <button onClick={handleLogout} className="text-gray-400 hover:text-gray-200 transition-colors duration-200">
            Logout
          </button>
        </div>}
        <div className="md:hidden">
          <button className="text-white focus:outline-none">
            <svg className="w-6 h-6" xmlns="http://www.w3.org/2000/svg" fill="none" stroke="currentColor" viewBox="0 0 24 24" strokeWidth="2">
              <path strokeLinecap="round" strokeLinejoin="round" d="M4 6h16M4 12h16M4 18h16"></path>
            </svg>
          </button>
        </div>
      </div>
    </nav>
  );
};

export default NavbarComp;
