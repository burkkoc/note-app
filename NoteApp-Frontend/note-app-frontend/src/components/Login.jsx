import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { loginUser } from '../redux/slices/authSlice';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import "../assets/styles/LoginForm.scss";
import {  faUser, faLock } from "@fortawesome/free-solid-svg-icons";



const Login = () => {
  const { loading, error, user, claims} = useSelector((state) => state.auth);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  

  const handleSubmit = async (e) => {
    
    e.preventDefault();
    const action =await dispatch(loginUser({ email, password }));
    if (action.type === 'auth/loginUser/fulfilled') {
      

      navigate('/home');  
    }
  };

  const handleLogout = () => {
    dispatch(logout());
  };
   
  useEffect(() => {
    if (user) {
      navigate('/home');
    }
  }, [user, navigate]);

  return (

    <div className="login-container">
      <div className="cont">
        <div className="login__cont">
          <div className="login">
            <div className="login__check"></div>
            <div className="login__form">
              <form onSubmit={handleSubmit} className="form-login">
                <div className="login__row">
                  <FontAwesomeIcon icon={faUser} className="login__icon" />
                  <input
                    type="text"
                    className="login__input name"
                    name="email"
                    placeholder="Email"
                    onChange={(e) => setEmail(e.target.value)}
                    required
                  />
                </div>
                {/* {errors.email != ''&& <p className="error-message">{errors.email}</p>} */}

                <div className="login__row">
                  <FontAwesomeIcon icon={faLock} className="login__icon" />
                    <input
                      type="password"
                      className="login__input pass"
                      name="password"
                      placeholder="Password"
                      onChange={(e) => setPassword(e.target.value)}
                      required
                    />
                </div>


                 {/* {errors.password != '' && <p className="error-message">{errors.password}</p>} */}
                <button
                  type="submit"
                  disabled={loading}
                  className="login__submit"
                  >
                    {loading ? 'Logging..' : 'Login'}
                </button>
                    {error && (<p className="error-message">An error occured: {error.message}</p>)}
                <p className="login__resendpassword">
                  &nbsp;<a href="#">Forgot Password? Re-send.</a>
                </p>
                
              </form>
              {user && (
        <div>
          <p>Welcome, {user.email}</p>
          <button onClick={handleLogout}>Logout</button>
        </div>
      )}
            </div>
          </div>
        </div>
      </div>
    </div>
    
  );
};

export default Login;
