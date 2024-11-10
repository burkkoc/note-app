import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import api from '../../Api/api';  

export const fetchAllNotes = createAsyncThunk(
    'notes/fetchAll',  
    async (_, { rejectWithValue }) => {
        try {
            const response = await api.get('/Note/GetAllNotes'); 
            return response.data; 
        } catch (error) {
            return rejectWithValue(error.response?.data || 'Failed to fetch notes');
        }
    }
);

export const updateNote = createAsyncThunk(
    'notes/updateNote',
    async ({ id, Title, Content }, { dispatch, rejectWithValue }) => {
      try {
        const response = await api.patch('/Note/Update', { id, Title, Content  });
        if (response.status === 200) {
          await dispatch(fetchAllNotes());
          return response.data; 
        }
        return rejectWithValue('Note update failed');
      } catch (error) {
        return rejectWithValue(error.response?.data || 'Note update failed');
      }
    }
  );


const initialState = {
    notes: [],       
    loading: false,   
    error: null,     
};

const notesSlice = createSlice({
    name: 'notes',  
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchAllNotes.pending, (state) => {
                state.loading = true;
                state.error = null;  
            })
            .addCase(fetchAllNotes.fulfilled, (state, action) => {
                state.notes = action.payload;  
                state.loading = false;          
            })
            .addCase(fetchAllNotes.rejected, (state, action) => {
                state.loading = false;  
                state.error = action.payload || 'Something went wrong'; 
            });
    },
});

export default notesSlice.reducer;
