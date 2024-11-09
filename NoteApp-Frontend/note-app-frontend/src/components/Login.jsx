import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { loginUser } from '../store/authSlice';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import "../assets/styles/LoginForm.scss";
import { faEye, faEyeSlash, faUser, faLock } from "@fortawesome/free-solid-svg-icons";


const Login = () => {
  const dispatch = useDispatch();
  const { loading, error } = useSelector((state) => state.auth);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errors, setErrors] = useState({
    email: '',
    password: '',
    credentials: '',
    hasErrors: false
  });


  const handleSubmit = (e) => {
    e.preventDefault();
    setEmail(e.target[0].value);
    setPassword(e.target[1].value);


    validationControl(email, password);
    
    if (errors.hasErrors) {
      console.log(errors);
      return;
    }
    console.log(email,password);
  
    dispatch(loginUser({ email, password }));
  };
  const validationControl = (email, password) => {
    const newErrors = {
      email: '',
      password: '',
      credentials: '',
      hasErrors:false
    };

    if (!email) {
         newErrors.email ="Email cannot be empty.";
         newErrors.hasErrors=true;
     }
    if (!password) {
     newErrors.password =  "Password cannot be empty.";
     newErrors.hasErrors=true;
    }

    if (newErrors.hasErrors) {
      setErrors(newErrors);
    } else {
      setErrors({
        email: '',
        password: '',
        credentials: '',
        hasErrors:false
      });
    }
   
};


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
                  />
                </div>
                {errors.email != ''&& <p className="error-message">{errors.email}</p>}

                <div className="login__row">
                  <FontAwesomeIcon icon={faLock} className="login__icon" />
                  <div className="input-password">
                    <input
                      type="password"
                      className="login__input pass"
                      name="password"
                      placeholder="Password"
                    />
                  </div>
                </div>


                 {errors.password != '' && <p className="error-message">{errors.password}</p>}
                 {/* {(!errors.password === '' && !errors.email === '') && errors.credentials && (<p className="error-message">{errors.credentials}</p>)} */}
                <button
                  type="submit"
                  className="login__submit"
                  >
                    Login
                </button>
                <p className="login__resendpassword">
                  &nbsp;<a href="#">Forgot Password? Re-send.</a>
                </p>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
    
  );
};

export default Login;
