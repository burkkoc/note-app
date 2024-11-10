import React, { useState } from 'react'
import '../assets/styles/GenericTable.css'
import { useDispatch } from 'react-redux'
import {
  updateMember,
  deleteMember,
  createMember
} from '../redux/slices/memberSlice'

const GenericTable = ({ data, loading, pageName, formData, setFormData }) => {
  const [currentPage, setCurrentPage] = useState(1)
  const [showEditModal, setShowEditModal] = useState(false)
  const [showCreateModal, setShowCreateModal] = useState(false)
  const [showDeleteModal, setShowDeleteModal] = useState(false)

  const [selectedRow, setSelectedRow] = useState(null)
  const dispatch = useDispatch()

  const rowsPerPage = 10

  const indexOfLastRow = currentPage * rowsPerPage
  const indexOfFirstRow = indexOfLastRow - rowsPerPage
  const currentRows = data.slice(indexOfFirstRow, indexOfLastRow)

  const emptyRows = rowsPerPage - currentRows.length
  const rowsToDisplay = [...currentRows, ...Array(emptyRows).fill({})]
  if (!data || data.length === 0) {
    return <p>No entry available</p>
  }
  if (loading) {
    return <p>Loading table data...</p>
  }

  const headers = Object.keys(data[0]).filter(header => header !== 'id')
  const openEditModal = row => {
    setSelectedRow(row)
    setShowEditModal(true)
  }

  const openDeleteModal = row => {
    setSelectedRow(row)
    setShowDeleteModal(true)
  }
  const openCreateModal = () => {
    setShowCreateModal(true)
  }

  const closeModal = method => {
    setSelectedRow(null)
    method(false)
  }

  const saveModal = () => {
    if (pageName === 'Member') {
      dispatch(
        updateMember({
          id: selectedRow.id,
          PhoneNumber: selectedRow.phoneNumber
        })
      )
      setShowEditModal(false)
    }
  }
  const createEntry = () => {
    if (pageName === 'Member') {
      setFormData(formData)
      dispatch(
        createMember({
          formData
        })
      )
      console.log(formData)
    }
  }

  const deleteConfirmButton = () => {
    console.log(selectedRow.id)

    if (pageName === 'Member') {
      dispatch(
        deleteMember({
          id: selectedRow.id
        })
      )
      setShowDeleteModal(false)
    }
  }

  return (
    <div className='relative'>
      <div className='overflow-x-auto bg-gray-800 p-5 rounded-lg shadow-lg mt-5'>
        <table className='w-full text-sm text-left text-gray-200 dark:text-gray-400 '>
          <caption className='p-5 text-lg font-semibold text-left text-gray-300 bg-gray-900 dark:text-white dark:bg-gray-900 rounded-lg mb-3 '>
            {pageName} List
            <button
              onClick={() => openCreateModal(data)}
              name='editButton'
              className='font-semibold text-gray-300 bg-green-800 hover:bg-green-900 rounded px-4 py-2 shadow-md transition-all ml-8'
            >
              Add New {pageName}
            </button>
          </caption>
          <thead className='text-xs text-gray-500 uppercase bg-gray-900 dark:bg-gray-800 dark:text-gray-400'>
            <tr>
              {headers.map(header => (
                <th key={header} className='px-6 py-3 text-center bg-gray-900'>
                  {header.charAt(0).toUpperCase() + header.slice(1)}
                </th>
              ))}
              <th className='px-6 py-3 text-center bg-gray-900'>Actions</th>
            </tr>
          </thead>
          <tbody>
            {rowsToDisplay.map((member, index) => (
              <tr
                key={index}
                className='bg-gray-800 border-b dark:bg-gray-900 dark:border-gray-700'
              >
                {headers.map(header => (
                  <td
                    key={header}
                    className='px-6 py-4 text-center bg-gray-800'
                  >
                    {header === 'gender'
                      ? member[header] === true
                        ? 'Male'
                        : member[header] === false
                        ? 'Female'
                        : '-'
                      : member[header] !== undefined
                      ? member[header]
                      : '-'}
                  </td>
                ))}
                <td className='px-6 py-4 text-center bg-gray-800'>
                  {
                    <button
                      onClick={() => openEditModal(member)}
                      name='editButton'
                      className='font-semibold text-gray-300 bg-yellow-700 hover:bg-yellow-900 rounded px-4 py-2 shadow-md transition-all'
                    >
                      Edit
                    </button>
                  }
                  {
                    <button
                      onClick={() => openDeleteModal(member)}
                      name='deleteButton'
                      className='font-semibold text-gray-300 bg-red-800 hover:bg-red-900 rounded px-4 py-2 shadow-md transition-all ml-2'
                    >
                      Delete
                    </button>
                  }
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* EditModal */}
      {showEditModal && (
        <div className='fixed inset-0 flex justify-center items-center z-50'>
          <div className='absolute inset-0 bg-black bg-opacity-50 backdrop-blur-sm z-40'></div>

          <div className='relative bg-gray-800 p-5 rounded-lg shadow-lg text-gray-200 w-1/3 z-50'>
            <h2 className='text-xl font-semibold mb-4'>Edit </h2>
            {Object.keys(selectedRow || {}).map(
              key =>
                (pageName === 'Member'
                  ? key === 'phoneNumber'
                  : key !== 'id') && (
                  <div key={key} className='mb-2 text-left'>
                    <label className='block text-gray-400'>{key}:</label>
                    <input
                      className='w-full px-3 py-2 bg-gray-700 text-gray-200 rounded focus:outline-none'
                      value={selectedRow[key] || ''}
                      onChange={e =>
                        setSelectedRow({
                          ...selectedRow,
                          [key]: e.target.value
                        })
                      }
                    />
                  </div>
                )
            )}

            <div className='mt-4 flex justify-end'>
              <button
                onClick={() => closeModal(setShowEditModal)}
                className='px-4 py-2 bg-red-700 rounded hover:bg-red-800 text-gray-200 mr-2'
              >
                Cancel
              </button>
              <button
                onClick={saveModal}
                className='px-4 py-2 bg-green-700 rounded hover:bg-green-800 text-gray-200'
              >
                Save
              </button>
            </div>
          </div>
        </div>
      )}

      {/* DeleteModal */}
      {showDeleteModal && (
        <div className='fixed inset-0 flex justify-center items-center z-50'>
          <div className='absolute inset-0 bg-black bg-opacity-50 backdrop-blur-sm z-40'></div>

          <div className='relative bg-gray-800 p-5 rounded-lg shadow-lg text-gray-200 w-1/3 z-50'>
            <h2 className='text-xl font-semibold mb-4 text-red-600'>Delete </h2>
            <p className='mb-2'>
              Are you sure you want to delete the following record?
            </p>
            {Object.keys(selectedRow || {}).map(
              key =>
                key !== 'id' && (
                  <div key={key} className='mb-2 text-left'>
                    <label className='block text-gray-400'>{key}:</label>
                    <input
                      className='w-full px-3 py-2 bg-gray-700 text-gray-200 rounded focus:outline-none'
                      value={
                        key === 'gender'
                          ? selectedRow[key] === true
                            ? 'Male'
                            : selectedRow[key] === false
                            ? 'Female'
                            : ''
                          : selectedRow[key] || ''
                      }
                      disabled
                    />
                  </div>
                )
            )}
            <div className='mt-4 flex justify-end'>
              <button
                onClick={() => closeModal(setShowDeleteModal)}
                className='px-4 py-2 bg-green-700 rounded hover:bg-green-800 text-gray-200'
              >
                Cancel
              </button>
              <button
                onClick={deleteConfirmButton}
                className='ml-2 px-4 py-2 bg-red-700 rounded hover:bg-red-800 text-gray-200 mr-2 '
              >
                Delete
              </button>
            </div>
          </div>
        </div>
      )}

      {/* CreateModal */}
      {showCreateModal && (
        <div className='fixed inset-0 flex justify-center items-center z-50'>
          <div className='absolute inset-0 bg-black bg-opacity-50 backdrop-blur-sm z-40'></div>

          <div className='relative bg-gray-800 p-5 rounded-lg shadow-lg text-gray-200 w-1/3 z-50'>
            <h2 className='text-xl font-semibold mb-4'>Create User</h2>

            {Object.keys(formData).map(key =>
              key === 'Gender' ? (
                <div key={key} className='mb-4 text-left'>
                  <label className='block text-gray-400 capitalize'>
                    {key}:
                  </label>
                  <select
                    className='w-full px-3 py-2 bg-gray-700 text-gray-200 rounded focus:outline-none'
                    value={
                      formData[key] === true
                        ? 'Male'
                        : formData[key] === false
                        ? 'Female'
                        : ''
                    }
                    onChange={e => {
                      // Seçilen değeri true/false olarak kaydet
                      setFormData({
                        ...formData,
                        [key]: e.target.value === 'Male' ? true : false
                      })
                    }}
                  >
                    <option value='Male'>Male</option>
                    <option value='Female'>Female</option>
                  </select>
                </div>
              ) : (
                <div key={key} className='mb-4 text-left'>
                  <label className='block text-gray-400 capitalize'>
                    {key}:
                  </label>
                  <input
                    className='w-full px-3 py-2 bg-gray-700 text-gray-200 rounded focus:outline-none'
                    value={formData[key] || ''}
                    onChange={e =>
                      setFormData({
                        ...formData,
                        [key]: e.target.value
                      })
                    }
                  />
                </div>
              )
            )}

            <div className='mt-4 flex justify-end'>
              <button
                onClick={() => closeModal(setShowCreateModal)}
                className='px-4 py-2 bg-red-700 rounded hover:bg-red-800 text-gray-200 mr-2'
              >
                Cancel
              </button>
              <button
                onClick={createEntry}
                className='px-4 py-2 bg-green-700 rounded hover:bg-green-800 text-gray-200'
              >
                Create
              </button>
            </div>
          </div>
        </div>
      )}
      <div className='flex justify-center mt-4'>
        <button
          onClick={() => setCurrentPage(prev => Math.max(prev - 1, 1))}
          className='px-4 py-2 mx-1 bg-gray-700 text-white rounded hover:bg-gray-600'
        >
          Previous
        </button>
        {Array.from(
          { length: Math.ceil(data.length / rowsPerPage) },
          (_, index) => (
            <button
              key={index}
              onClick={() => setCurrentPage(index + 1)}
              className={`px-4 py-2 mx-1 ${
                currentPage === index + 1
                  ? 'bg-yellow-500 text-black'
                  : 'bg-gray-700 text-white'
              } rounded hover:bg-gray-600`}
            >
              {index + 1}
            </button>
          )
        )}
        <button
          onClick={() =>
            setCurrentPage(prev =>
              Math.min(prev + 1, Math.ceil(data.length / rowsPerPage))
            )
          }
          className='px-4 py-2 mx-1 bg-gray-700 text-white rounded hover:bg-gray-600'
        >
          Next
        </button>
      </div>
    </div>
  )
}

export default GenericTable
