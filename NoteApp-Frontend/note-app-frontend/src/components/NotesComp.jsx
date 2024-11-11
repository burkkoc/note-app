import React, { useState, useEffect, useRef } from "react";
import { useDispatch, useSelector } from 'react-redux';
import { fetchAllNotes } from '../redux/slices/noteSlice';
import GenericTable from './GenericTable';

const NotesComp = () => {
    const dispatch = useDispatch();
    const { notes, loading, error } = useSelector((state) => state.notes);
    const [formData, setFormData] = useState({
        Title: '',
        Content: ''
      });

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
                pageName={"Note"}
                formData = {formData}
                setFormData = {setFormData}
            ></GenericTable>
        </>
    );
};

export default NotesComp;
