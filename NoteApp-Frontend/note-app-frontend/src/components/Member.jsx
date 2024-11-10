import React, { useState, useEffect, useRef } from "react";
import { useDispatch, useSelector } from 'react-redux';
import { fetchMembers } from '../redux/slices/memberSlice';
import GenericTable from './GenericTable';

const MemberComp = () => {
    const dispatch = useDispatch();
    const { members, loading, error } = useSelector((state) => state.members);
    const [data, setData] = useState([]);


    useEffect(() => {
        dispatch(fetchMembers());
    }, [dispatch]);

    useEffect(() => {
        if (members) {
            setData(members);
        }
    }, [members]);


    return (
        <>
            <GenericTable
                data={data}
            ></GenericTable>
        </>
    );
};

export default MemberComp;
