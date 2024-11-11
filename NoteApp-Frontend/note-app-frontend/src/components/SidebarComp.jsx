import React, { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Sidebar } from "flowbite-react";
import { logout } from '../redux/slices/authSlice';
import { Link } from "react-router-dom";
import { HiInbox, HiTable, HiUser } from "react-icons/hi";
import '../assets/styles/SidebarComp.css';
import PermissionModalComp from './PermissionModalComp'; 

const SidebarComp = () => {
  const dispatch = useDispatch();
  const token = useSelector((state) => state.auth.token);

  const { filteredClaims } = useSelector(state => state.auth);
  const canReadAnyMember = filteredClaims.includes("CanReadAnyMember");
  const canReadAnyNote = filteredClaims.includes("CanReadAnyNote");
  const canGivePermission = filteredClaims.includes("CanGivePermission");
  const canRemovePermission = filteredClaims.includes("CanRemovePermission");

  const shouldShowPermissionAccesButton = ((canGivePermission) || (canRemovePermission));


  const handleLogout = () => {
    dispatch(logout());
  };

  const [showModal, setShowModal] = useState(false);

  const openModal = () => {
    
    if (!canGivePermission && !canRemovePermission) {
      navigate('/accessdenied'); 
    } else {
      setShowModal(true); 
    }
  }; 
  
  return (
    <>
      {token && (
        <Sidebar className="sidebar-cont">
          <Sidebar.Items>
            <Sidebar.ItemGroup>
              <Sidebar.Item as={Link} to="/home" icon={HiInbox} className="hover:bg-gray-900 hover:no-underline transition-colors duration-200 cursor-pointer">
                Menu
              </Sidebar.Item>
              <Sidebar.Collapse icon={HiUser} label="Membership" className="hover:bg-gray-900 hover:no-underline transition-colors duration-200 cursor-pointer">
                <Sidebar.Item as={Link} to="/member" label={!canReadAnyMember && "NeedAccess"} labelColor="red" className="hover:bg-gray-900 hover:no-underline transition-colors duration-200 cursor-pointer">All Members</Sidebar.Item>
                <Sidebar.Item
                  as={Link}
                  onClick={openModal}  
                  label={!shouldShowPermissionAccesButton && "NeedAccess"}
                  labelColor="red"
                  className="hover:bg-gray-900 hover:no-underline transition-colors duration-200 cursor-pointer"
                >
                  Permissions
                </Sidebar.Item>
              </Sidebar.Collapse>
              <Sidebar.Collapse icon={HiTable} label="Notes" className="hover:bg-gray-900 hover:no-underline transition-colors duration-200 cursor-pointer ">
                <Sidebar.Item to="/mynotes" className="hover:bg-gray-900 hover:no-underline transition-colors duration-200 cursor-pointer" label="Disabled" labelColor="yellow" disabled>My Notes</Sidebar.Item>
                <Sidebar.Item as={Link} to="/notes" label={(!canReadAnyNote) && "NeedAccess"} labelColor ="red" className="hover:bg-gray-900 hover:no-underline transition-colors duration-200 cursor-pointer">All Notes</Sidebar.Item>
              </Sidebar.Collapse>
            </Sidebar.ItemGroup>
            <Sidebar.ItemGroup className="hover:bg-gray-900 hover:no-underline transition-colors duration-200 cursor-pointer">
              <Sidebar.Item onClick={handleLogout} icon={HiTable} className="hover:bg-gray-900 hover:no-underline transition-colors duration-200 cursor-pointer">
                Log out
              </Sidebar.Item>
            </Sidebar.ItemGroup>
          </Sidebar.Items>
        </Sidebar>
      )}

<PermissionModalComp showModal={showModal} setShowModal={setShowModal} />

    </>
  );
};

export default SidebarComp;
