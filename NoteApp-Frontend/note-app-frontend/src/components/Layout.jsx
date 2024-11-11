import React from 'react';
import NavbarComp from './NavbarComp';
import '../assets/styles/Layout.css' 
import Footer from './FooterComp';


const Layout = ({ children }) => {
    return (
        <div className="layout min-h-screen flex flex-col">
            <NavbarComp />
            <main className="content flex-grow">
                {children}
            </main>
            <Footer />
        </div>
    );
};

export default Layout;