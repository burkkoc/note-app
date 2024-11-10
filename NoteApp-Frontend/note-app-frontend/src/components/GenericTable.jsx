// import React from 'react';
// import "../assets/styles/GenericTable.css";

// const GenericTable = ({ data }) => {
//   // Eğer veri yoksa, boş tablo yerine bir mesaj gösterebiliriz
//   if (!data || data.length === 0) {
//     return <p>No members available</p>;
//   }

//   // Tablo başlıklarını dinamik olarak almak, 'id'yi hariç tutarak
//   const headers = Object.keys(data[0]).filter((header) => header !== "id");

//   return (
//     <div className="overflow-x-auto bg-gray-800 p-5 rounded-lg shadow-lg mt-5">
//       <table className="w-full text-sm text-left text-gray-200 dark:text-gray-400">
//         <caption className="p-5 text-lg font-semibold text-left text-gray-300 bg-gray-900 dark:text-white dark:bg-gray-900">
//           Members List
//           <p className="mt-1 text-sm font-normal text-gray-500 dark:text-gray-400">
//             Browse a list of members with their details.
//           </p>
//         </caption>
//         <thead className="text-xs text-gray-500 uppercase bg-gray-700 dark:bg-gray-800 dark:text-gray-400">
//           <tr>
//             {headers.map((header) => (
//               <th key={header} className="px-6 py-3 text-center">{header.charAt(0).toUpperCase() + header.slice(1)}</th>
//             ))}
//             <th scope="col" className="px-6 py-3 text-center">
//               <span className="sr-only">Edit</span>
//             </th>
//           </tr>
//         </thead>
//         <tbody>
//           {data.map((member, index) => (
//             <tr key={index} className="bg-gray-800 border-b dark:bg-gray-900 dark:border-gray-700">
//               {headers.map((header) => (
//                 <td key={header} className="px-6 py-4 text-center">
//                   {header === "gender"
//                     ? member[header] === true
//                       ? "Male"
//                       : member[header] === false
//                       ? "Female"
//                       : "-"
//                     : member[header] ?? "-"}
//                 </td>
//               ))}
//               <td className="px-6 py-4 text-center">
//                 <a href="#" className="font-semibold text-gray-300 bg-yellow-900 hover:bg-yellow-500 rounded px-4 py-2 shadow-md transition-all duration-200 ease-in-out hover:no-underline">
//                   Edit
//                 </a>
//                 <a href="#" className="ml-2 font-semibold text-gray-300 bg-red-900 hover:bg-red-800 rounded px-4 py-2 shadow-md transition-all duration-200 ease-in-out hover:no-underline">
//                   Delete
//                 </a>
//               </td>
//             </tr>
//           ))}
//         </tbody>
//       </table>
//     </div>
//   );
// };

// export default GenericTable;
import React, { useState } from "react";
import "../assets/styles/GenericTable.css";

const GenericTable = ({ data }) => {
  // Eğer veri yoksa, boş tablo yerine bir mesaj gösterebiliriz
  if (!data || data.length === 0) {
    return <p>No members available</p>;
  }

  // Tablo başlıklarını dinamik olarak almak, 'id'yi hariç tutarak
  const headers = Object.keys(data[0]).filter((header) => header !== "id");

  // Sayfalama için gerekli state'ler
  const [currentPage, setCurrentPage] = useState(1);
  const rowsPerPage = 10;

  // Veriyi 10'luk gruplara bölüyoruz
  const indexOfLastRow = currentPage * rowsPerPage;
  const indexOfFirstRow = indexOfLastRow - rowsPerPage;
  const currentRows = data.slice(indexOfFirstRow, indexOfLastRow);

  // Sayfa değiştirme fonksiyonu
  const paginate = (pageNumber) => setCurrentPage(pageNumber);

  // Sayfa sayısı hesaplama
  const totalPages = Math.ceil(data.length / rowsPerPage);

  // Eğer veri sayısı 10'dan azsa, boş satırlar ekliyoruz
  const emptyRows = rowsPerPage - currentRows.length;
  const rowsToDisplay = [...currentRows, ...Array(emptyRows).fill({})];

  return (
    <div className="overflow-x-auto bg-gray-800 p-5 rounded-lg shadow-lg mt-5">
      <table className="w-full text-sm text-left text-gray-200 dark:text-gray-400">
        <caption className="p-5 text-lg font-semibold text-left text-gray-300 bg-gray-900 dark:text-white dark:bg-gray-900 rounded-lg mb-3">
          Members List
          <p className="mt-1 text-sm font-normal text-gray-500 dark:text-gray-400">
            Browse a list of members with their details.
          </p>
        </caption>
        <thead className="text-xs text-gray-500 uppercase bg-gray-900 dark:bg-gray-800 dark:text-gray-400">
          <tr>
            {headers.map((header) => (
              <th key={header} className="px-6 py-3 text-center bg-gray-900">
                {header.charAt(0).toUpperCase() + header.slice(1)}
              </th>
            ))}
            <th scope="col" className="px-6 py-3 text-center">
              <span className="sr-only">Edit</span>
              Actions
            </th>
          </tr>
        </thead>
        <tbody>
          {rowsToDisplay.map((member, index) => (
            <tr key={index} className="bg-gray-800 border-b dark:bg-gray-900 dark:border-gray-700 ">
              {headers.map((header) => (
                <td key={header} className="px-6 py-4 text-center bg-gray-800  ">
                  {member[header] !== undefined && member[header] !== null
                    ? header === "gender"
                      ? member[header] === true
                        ? "Male"
                        : member[header] === false
                        ? "Female"
                        : "-"
                      : member[header]
                    : "-"}
                </td>
              ))}
              <td className="px-6 py-4 text-center  bg-gray-800">
                <a href="#" className="font-semibold text-gray-300 bg-yellow-900 hover:bg-yellow-500 rounded px-4 py-2 shadow-md transition-all duration-200 ease-in-out hover:no-underline">
                  Edit
                </a>
                <a href="#" className="ml-2 font-semibold text-gray-300 bg-red-900 hover:bg-red-800 rounded px-4 py-2 shadow-md transition-all duration-200 ease-in-out hover:no-underline">
                  Delete
                </a>
              </td>
            </tr>
          ))}
        </tbody>
        <tfoot>
          <tr className="bg-gray-900 border-t dark:bg-gray-900 dark:border-gray-700">
            <td colSpan={headers.length + 1} className="px-6 py-4 text-center">
              Total Members: {data.length}
            </td>
          </tr>
        </tfoot>
      </table>

      <div className="flex justify-center mt-4">
        <button
          className="px-4 py-2 mx-1 bg-gray-700 text-white rounded hover:bg-gray-600"
          onClick={() => paginate(currentPage > 1 ? currentPage - 1 : 1)}
        >
          Previous
        </button>
        {Array.from({ length: totalPages }, (_, index) => (
          <button
            key={index}
            className={`px-4 py-2 mx-1 ${currentPage === index + 1 ? "bg-yellow-500 text-black" : "bg-gray-700 text-white"} rounded hover:bg-gray-600`}
            onClick={() => paginate(index + 1)}
          >
            {index + 1}
          </button>
        ))}
        <button
          className="px-4 py-2 mx-1 bg-gray-700 text-white rounded hover:bg-gray-600"
          onClick={() => paginate(currentPage < totalPages ? currentPage + 1 : totalPages)}
        >
          Next
        </button>
      </div>
    </div>
  );
};

export default GenericTable;
