import React from 'react';
import "../assets/styles/Navbar.css";
import { useDispatch } from 'react-redux';
import { fetchMembers } from '../redux/slices/memberSlice';
import { useNavigate } from 'react-router-dom'; // React Router yönlendirme için

const NavbarComp = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const handleMembersClick = () => {
    dispatch(fetchMembers()); // API isteği gönder
    navigate('/members'); // Members sayfasına yönlendir
  };

  const handleLogout = () => {
    // Çıkış işlemi burada yapılır
  };

  return (
    <nav className="bg-gray-900 text-white shadow-lg py-4">
      <div className="container mx-auto flex items-center justify-between px-6">
        <div className="flex items-center space-x-4">
          <img src="../../public/memberLogoMale.png" alt="Logo" className="h-8 w-auto" />
          <span className="text-2xl font-bold">NoteApp</span>
        </div>
        <div className="hidden md:flex space-x-6">
          {/* Members linkine onClick ekleyin */}
          <a onClick={handleMembersClick} className="text-white hover:text-gray-300 focus:outline-none focus:ring-0 cursor-pointer">
            Members
          </a>
          <a href="/notes" className="text-white hover:text-gray-300 focus:outline-none focus:ring-0">
            Notes
          </a>
          <a href="/login" onClick={handleLogout} className="text-white hover:text-gray-300 focus:outline-none focus:ring-0">
            Logout
          </a>
        </div>
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
