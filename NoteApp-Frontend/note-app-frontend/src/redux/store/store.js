import { configureStore } from '@reduxjs/toolkit';
import authReducer from './../slices/authSlice';
import memberReducer from './../slices/memberSlice';
import notesReducer from './../slices/noteSlice';

const store = configureStore({
  reducer: {
    auth: authReducer,
    members: memberReducer,
    notes: notesReducer
  },
});

export default store;
