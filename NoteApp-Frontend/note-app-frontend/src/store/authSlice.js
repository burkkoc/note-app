import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import api from '../Api/api';

export const loginUser = createAsyncThunk(
  'auth/loginUser',
  async ({ email, password }, { rejectWithValue }) => {
    try {
      const response = await api.post('/Authentication/Login', { email, password });
      if (response.status === 200) {
        return response.data; 
      }
      return rejectWithValue('Login failed');
    } catch (error) {
      // Hata mesajını kontrol et ve gerekirse fallback mesajı ekle
      return rejectWithValue(error.response?.data || 'Giriş başarısız oldu.');
    }
  }
);

const authSlice = createSlice({
  name: 'auth',
  initialState: {
    user: null,
    token: localStorage.getItem('token'),  // Sayfa yenilendiğinde token'ı al
    loading: false,
    error: null,
  },
  reducers: {
    logout: (state) => {
      state.user = null;
      state.token = null;
      localStorage.removeItem('token');  // Çıkış yaparken token'ı temizle
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loginUser.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(loginUser.fulfilled, (state, action) => {
        state.loading = false;
        state.user = action.payload.memberDTO;
        state.token = action.payload.token;
        localStorage.setItem('token', action.payload.token); // Token'ı kaydet
      })
      .addCase(loginUser.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload;
      });
  },
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;
