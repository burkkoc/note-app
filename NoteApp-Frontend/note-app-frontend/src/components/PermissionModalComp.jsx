import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchMembers } from '../redux/slices/memberSlice';
import { assignMember } from '../redux/slices/authSlice';

const PermissionModalComp = ({ showModal, setShowModal }) => {
    const dispatch = useDispatch();

    const { members, loading, error } = useSelector((state) => state.members);
    const [selectedUserId, setSelectedUserId] = useState('');
    const [selectedPermission, setSelectedPermission] = useState('');

    useEffect(() => {

        if (showModal) {
            dispatch(fetchMembers());
        }
    }, [dispatch, showModal]);

    
    const renderUserOptions = () => {
        if (loading) return <option>Loading...</option>;
        if (error) return <option>Error: {error}</option>;
        if (!members.length) return <option>No users available</option>;

        return members
            .filter((member) => !member.email.includes('admin'))
            .map((member) => (
                <option key={member.id} value={member.id}>
                    {member.email}
                </option>
            ));
    };
    const handleUserSelect = (e) => {
        setSelectedUserId(e.target.value);
    };
    const handleSubmit = () => {

        dispatch(assignMember({ id: selectedUserId, claimName: selectedPermission }));
        setShowModal(false);
    };

    return (
        <>
            {showModal && (
                <div className="fixed inset-0 flex justify-center items-center z-50">
                    <div
                        className="absolute inset-0 bg-black bg-opacity-50 backdrop-blur-sm z-40"
                        onClick={() => setShowModal(false)}
                    ></div>
                    <div className="relative bg-gray-800 p-5 rounded-lg shadow-lg text-gray-200 w-1/3 z-50">
                        <h2 className="text-xl font-semibold mb-4 text-red-600">Assign Permissions</h2>
                        <div className="mb-4">
                            <label htmlFor="user" className="block text-gray-400 mb-2">
                                Select User
                            </label>
                            <select
                                id="user"
                                value={selectedUserId}
                                onChange={handleUserSelect}
                                className="w-full px-3 py-2 bg-gray-700 text-gray-200 rounded focus:outline-none focus:ring-2 focus:ring-red-600"
                            >
                                {renderUserOptions()}
                            </select>
                        </div>

                        <div className="mb-4">
                            <label htmlFor="permission" className="block text-gray-400 mb-2">
                                Select Permission
                            </label>
                            <select
                                id="permission"
                                value={selectedPermission}
                                onChange={(e) => setSelectedPermission(e.target.value)}
                                className="w-full px-3 py-2 bg-gray-700 text-gray-200 rounded focus:outline-none focus:ring-2 focus:ring-red-600"
                            >
                                <option value="CanCreateMember">Can Create Member</option>
                                <option value="CanEditMember">Can Edit Member</option>
                                <option value="CanDeleteMember">Can Delete Member</option>
                                <option value="CanReadAnyMember">Can Read Any Member</option>
                                <option value="CanReadAnyNote">Can Read Any Note</option>
                                <option value="CanDeleteAnyNote">Can Delete Any Note</option>
                                <option value="CanGivePermission">Can Give Permission</option>
                                <option value="CanRemovePermission">Can Remove Permission</option>
                            </select>
                        </div>

                        <div className="mt-4 flex justify-end">
                            <button
                                onClick={() => setShowModal(false)}
                                className="px-4 py-2 bg-green-700 rounded hover:bg-green-800 text-gray-200 mr-2"
                            >
                                Cancel
                            </button>
                            <button
                                onClick={handleSubmit}
                                className="px-4 py-2 bg-red-700 rounded hover:bg-red-800 text-gray-200"
                            >
                                Assign
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </>
    );
};

export default PermissionModalComp;
