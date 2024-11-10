import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import api from '../../Api/api';  // Assuming your API utility is set up like this

// AsyncThunk: Fetch all notes from the server
export const fetchAllNotes = createAsyncThunk(
    'notes/fetchAll',  // Action type (name) for fetching notes
    async (_, { rejectWithValue }) => {
        try {
            const response = await api.get('/Note/GetAllNotes');  // API call to fetch all notes
            return response.data;  // Return the response data (list of notes)
        } catch (error) {
            // If the request fails, handle error gracefully
            return rejectWithValue(error.response?.data || 'Failed to fetch notes');
        }
    }
);

const initialState = {
    notes: [],        // Store for notes data
    loading: false,   // To track the loading state
    error: null,      // To track any errors that occur
};

const notesSlice = createSlice({
    name: 'notes',  // Slice name
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            // Pending case when the request is in progress
            .addCase(fetchAllNotes.pending, (state) => {
                state.loading = true;
                state.error = null;  // Reset error on new request
            })
            // Fulfilled case when the request is successful
            .addCase(fetchAllNotes.fulfilled, (state, action) => {
                state.notes = action.payload;  // Set the fetched notes into state
                state.loading = false;          // Set loading to false after success
            })
            // Rejected case when the request fails
            .addCase(fetchAllNotes.rejected, (state, action) => {
                state.loading = false;  // Set loading to false after failure
                state.error = action.payload || 'Something went wrong';  // Set the error message
            });
    },
});

// Export the reducer for the slice
export default notesSlice.reducer;
