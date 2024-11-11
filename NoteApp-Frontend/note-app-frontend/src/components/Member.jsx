import React, { useState, useEffect, useRef } from "react";
import { useDispatch, useSelector } from 'react-redux';
import { fetchMembers } from '../redux/slices/memberSlice';
import GenericTable from './GenericTable';
import { Navigate, useNavigate } from "react-router-dom";

const MemberComp = () => {
    const dispatch = useDispatch();
    const { members, loading, error } = useSelector((state) => state.members);
    const navigate = useNavigate();


    useEffect(() => {
      if (error && error.includes('member')) {
        navigate('/accessdenied');  
      }
    }, [error, navigate]);
    
    const [formData, setFormData] = useState({
        FirstName: '',
        LastName: '',
        Email: '',
        PhoneNumber: '',
        Gender: true
      });

    useEffect(() => {
        dispatch(fetchMembers());
        
    }, [dispatch]);

    if (loading) {
        return <p>Loading...</p>; 
      }
      if (error) {
        return <p>Error: {error}</p>; 
      }
    return (
        <>
            <GenericTable
                data={members}
                loading={loading}
                pageName={"Member"}
                formData = {formData}
                setFormData = {setFormData}
            ></GenericTable>
        </>
    );
};

export default MemberComp;
