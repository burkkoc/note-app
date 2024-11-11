import React from 'react';
import NavbarComp from './NavbarComp';
import '../assets/styles/Layout.css'
import Footer from './FooterComp';
import SidebarComp from './SidebarComp';


const Layout = ({ children }) => {
  return (
    <div className="layout min-h-screen flex flex-col">
      <NavbarComp />
      <main className="flex flex-grow">
        <div className="flex w-full">
          <SidebarComp className="fixed bottom-0 left-0 h-screen w-[260px] bg-gray-800" />

          <div className="flex-grow flex justify-center items-start p-5">
            {children}
          </div>
        </div>
      </main>
      <Footer />
    </div>
  );
};

export default Layout;