const Footer = () => {
    return (
      <footer className="bg-gray-900 text-gray-300 py-3 mt-10 shadow-inner">
        <div className="container mx-auto flex flex-col md:flex-row justify-between items-center px-6">
          <div className="text-sm text-center md:text-left mb-4 md:mb-0">
            © {new Date().getFullYear()} NoteApp. All rights reserved.
          </div>
          <div className="flex space-x-4">
            
            <a
              href="#"
              className="text-gray-400 hover:text-gray-200 transition-colors duration-200"
            >
              Contact
            </a>
          </div>
        </div>
      </footer>
    );
  };
  
  export default Footer;
  