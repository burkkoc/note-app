import React from 'react';
import { useSelector } from 'react-redux';
import { Sidebar } from "flowbite-react";
import 'flowbite/dist/flowbite.css';
import { NavLink } from "react-router-dom";
import {  HiInbox, HiTable, HiUser } from "react-icons/hi";

const Home = () => {
    const {user} = useSelector(state => state.auth);

return (
    <Sidebar className='sidebar-cont'>
      <Sidebar.Items>
        <Sidebar.ItemGroup>
        <Sidebar.Item href="#" icon={HiInbox}>
            Menu
          </Sidebar.Item>
        <Sidebar.Collapse icon={HiUser} label="Membership">
        <Sidebar.Item href="#">My Profile</Sidebar.Item>
        <Sidebar.Item href="#" label="NeedAccess" labelColor="red">All Members</Sidebar.Item>
          </Sidebar.Collapse >
          <Sidebar.Collapse icon={HiTable} label="Notes" >
            <Sidebar.Item href="#">My Notes</Sidebar.Item>
            <Sidebar.Item href="#" label="NeedAccess" labelColor="red">All Notes</Sidebar.Item>
          </Sidebar.Collapse>
          
        </Sidebar.ItemGroup>
        <Sidebar.ItemGroup>
        <Sidebar.Item href="#" icon={HiTable}>
            Log out
          </Sidebar.Item>
        </Sidebar.ItemGroup>
      </Sidebar.Items>
    </Sidebar>

    
  );
};

export default Home;