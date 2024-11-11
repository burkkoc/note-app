import React, { useState, useEffect, useRef } from "react";
import { useDispatch, useSelector } from 'react-redux';
import { fetchAllNotes } from '../redux/slices/noteSlice';
import GenericTable from './GenericTable';
import { Navigate, useNavigate } from "react-router-dom";


const NotesComp = ({pageName}) => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const { notes, loading, error } = useSelector((state) => state.notes);
    const [formData, setFormData] = useState({
        Title: '',
        Content: ''
      });

      useEffect(() => {
        if (error && error.includes('note')) {
          navigate('/accessdenied');  
        }
      }, [error, navigate]);

      
    useEffect(() => {
        dispatch(fetchAllNotes());
    }, [dispatch]);

  

    if (loading) {
        return <div>Loading...</div>;
      }
    
      if (error) {
        return <div>Error: {error}</div>;
      }


    return (
        <>
            <GenericTable
                data={notes}
                loading={loading}
                pageName={pageName}
                formData = {formData}
                setFormData = {setFormData}
            ></GenericTable>
        </>
    );
};

export default NotesComp;
