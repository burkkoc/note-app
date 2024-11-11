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

export const assignMember = createAsyncThunk(
  'auth/assignMember',
  async ({ id, claimName }, { rejectWithValue }) => {
    try {
      const response = await api.post(
        '/Authentication/AssignMember',
        { id, claimName },  
        {
          headers: {
            'Content-Type': 'application/json', 
          },
        }
      );

      if (response.status === 200) {
        return response.data;
      } else {
        return rejectWithValue('Assign operation failed');
      }
    } catch (error) {
     
      return rejectWithValue(error.response?.data || 'Assign operation failed');
    }
  }
);

const initialState = {
  user: JSON.parse(localStorage.getItem('user')) || null,
  token: localStorage.getItem('token') || null,
  claims: JSON.parse(localStorage.getItem('claims')) || null,  
  filteredClaims: JSON.parse(localStorage.getItem('filteredClaims')) || [],  
  loading: false,
  error: null,
  assignStatus: null,  
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    logout: (state) => {
      state.user = null;
      state.token = null;
      state.claims = null;
      state.filteredClaims = [];
      state.assignStatus = null;  
      localStorage.removeItem('user');
      localStorage.removeItem('token');
      localStorage.removeItem('claims');
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
          state.filteredClaims = filteredClaims;
        }
      })
      .addCase(decodeToken.rejected, (state, action) => {
        state.error = action.payload;
      });

    builder
      .addCase(assignMember.pending, (state) => {
        state.loading = true;
        state.error = null;
        state.assignStatus = null;  
      })
      .addCase(assignMember.fulfilled, (state, action) => {
        state.loading = false;
        state.assignStatus = 'Assign operation success';  
        alert(action.payload); 
      })
      .addCase(assignMember.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload;
        state.assignStatus = 'Assign operation failed';  
        alert(action.payload); 
      });
  },
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;
