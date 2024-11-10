import React, { useState, useEffect, useRef } from "react";
import { useDispatch, useSelector } from 'react-redux';
import { fetchMembers } from '../redux/slices/memberSlice';
import GenericTable from './GenericTable';

const MemberComp = () => {
    const dispatch = useDispatch();
    const { members, loading, error } = useSelector((state) => state.members);
    const [data, setData] = useState([]);
    const [formData, setFormData] = useState({
        FirstName: '',
        LastName: '',
        Email: '',
        PhoneNumber: '',
        Gender: true
      });

    useEffect(() => {
        dispatch(fetchMembers());
        console.log(members);
        
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
