import { Link } from 'react-router-dom';

const AccessDeniedComp = () => {
  return (
<div className="min-h-screen w-full flex flex-col bg-gray-800 text-white">
<div className="flex-grow flex items-center justify-center">
        <div className="text-center p-8 max-w-md mx-auto">
          <div className="mb-4">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
              className="h-16 w-16 mx-auto text-red-600"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M18 6L6 18M6 6l12 12"
              />
            </svg>
          </div>
          <h1 className="text-4xl font-semibold mb-4">Access Denied</h1>
          <p className="text-lg mb-6">
            You don't have permission to access this page.
          </p>
          <Link to="/home"
            href="/home"
            className="bg-red-600 hover:bg-red-700 text-white font-semibold py-2 px-4 rounded transition-colors duration-200"
          >
            Go Back Home
          </Link>
        </div>
      </div>
    </div>
  );
};

export default AccessDeniedComp;
