import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import api from '../../Api/api';

// AsyncThunk: Üyeleri fetch etme işlemi
export const fetchMembers = createAsyncThunk(
    'members/fetchMembers',  // Thunk işlemi için isim
    async (_, { rejectWithValue }) => {
      try {
        const token = localStorage.getItem('token');
        const response = await api.get('/Member/GetAll', {
            headers: {
              Authorization: `${token}`  // Token'ı ekliyoruz
            }
            
        });
        console.log(response);
        return response.data; 
    } catch (error) {
        console.log(token);
        return rejectWithValue(error.response?.data || 'Veri alınamadı.');
    }
      
    }
);

const memberSlice = createSlice({
  name: 'members',
  initialState: {
    members: [],
    loading: false,
    error: null,
  },
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchMembers.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchMembers.fulfilled, (state, action) => {
        state.members = action.payload;
        state.loading = false;
        state.error = null;
      })
      .addCase(fetchMembers.rejected, (state, action) => {
        state.error = action.payload || 'Bir hata oluştu.';
        state.loading = false;
      });
  },
});

// Sadece reducer'ı export ediyoruz
export default memberSlice.reducer;
