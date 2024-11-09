import { configureStore } from '@reduxjs/toolkit';
import authReducer from './../slices/authSlice';
import memberReducer from './../slices/memberSlice';

const store = configureStore({
  reducer: {
    auth: authReducer,
    members: memberReducer,
  },
});

export default store;
