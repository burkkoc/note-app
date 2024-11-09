import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchMembers } from '../redux/slices/memberSlice'; 

const MembersList = () => {
  const dispatch = useDispatch();
  const { members, loading, error } = useSelector((state) => state.members); 

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      dispatch(fetchMembers());  
    }
  }, [dispatch]);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;
console.log(members);

  return (
    <div>
      <h1>Members List</h1>
      <ul>
        {members.map((member) => (
          <li key={member.id}>{member.name}</li>
        ))}
      </ul>
    </div>
  );
};

export default MembersList;
