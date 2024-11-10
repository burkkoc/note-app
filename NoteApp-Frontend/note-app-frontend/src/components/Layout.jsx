import React from 'react';
import NavbarComp from './NavbarComp';
import '../assets/styles/Layout.css' 


const Layout = ({ children }) => {
    return (
        <div className="layout">
            <NavbarComp />
            <main className="content">
                {children}
            </main>
        </div>
    );
};

export default Layout;