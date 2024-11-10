import React, { useState, useEffect, useRef } from "react";
import { useDispatch, useSelector } from 'react-redux';
import { fetchAllNotes } from '../redux/slices/noteSlice';
import GenericTable from './GenericTable';

const NotesComp = () => {
    const dispatch = useDispatch();
    const { notes, loading, error } = useSelector((state) => state.notes);
    const [data, setData] = useState([]);


    useEffect(() => {
        dispatch(fetchAllNotes());
    }, [dispatch]);

    useEffect(() => {
        if (notes) {
            setData(notes);
        }
    }, [notes]);

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
            ></GenericTable>
        </>
    );
};

export default NotesComp;
