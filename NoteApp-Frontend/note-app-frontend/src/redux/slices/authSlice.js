import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import api from '../../Api/api';




function decodeTokenFunc(token) {
  if (token && token !== undefined) {
    
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );
    return JSON.parse(jsonPayload);
  }
}

export const loginUser = createAsyncThunk(
  'auth/loginUser',
  async ({ email, password }, { dispatch, rejectWithValue }) => {
    try {
      const response = await api.post('/Authentication/Login', { email, password });
      if (response.status === 200) {
        dispatch(decodeToken(response.data.token || null));

        return response.data;
      }
      return rejectWithValue('Login failed');
    } catch (error) {
      return rejectWithValue(error.response?.data || 'Login failed');
    }
  }
);

export const decodeToken = createAsyncThunk(
  'auth/decodeToken',
  async (token, { rejectWithValue }) => {
    try {
      const claims = decodeTokenFunc(token);
      return claims;
    } catch (error) {
      return rejectWithValue('Token is NOT valid.');
    }
  }
);

const authSlice = createSlice({
  name: 'auth',
  initialState: {
    user: JSON.parse(localStorage.getItem('user')) || null,
    token: localStorage.getItem('token') || null,
    claims: null,
    loading: false,
    error: null,
  },
  reducers: {
    logout: (state) => {
      state.user = null;
      state.token = null;
      state.claims = null;
      localStorage.removeItem('user');
      localStorage.removeItem('token');
      localStorage.removeItem('filteredClaims');
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
        localStorage.setItem('user', JSON.stringify(action.payload.memberDTO));
        localStorage.setItem('token', action.payload.token);

      })
      .addCase(loginUser.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload;
      });
    builder
      .addCase(decodeToken.fulfilled, (state, action) => {
        state.claims = action.payload;
        const filteredClaims = Object.entries(state.claims || {})
          .filter(([key]) => key.startsWith("Can"))
          .map(([key]) => key);

        if (filteredClaims.length > 0) {
          localStorage.setItem('filteredClaims', JSON.stringify(filteredClaims));
        }
      })
      .addCase(decodeToken.rejected, (state, action) => {
        state.error = action.payload;
      });
  },
});


export const { logout } = authSlice.actions;
export default authSlice.reducer;
