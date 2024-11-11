import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import api from '../../Api/api';

export const fetchMembers = createAsyncThunk(
    'members/fetchMembers',  
    async (_, { rejectWithValue }) => {
      try {
        // const token = localStorage.getItem('token');
        const response = await api.get('/Member/GetAll');
        return response.data; 
    } catch (error) {
        return rejectWithValue(error.response?.data || 'Fetch failed.');
    }
      
    }
);

export const updateMember = createAsyncThunk(
  'member/updateMember',
  async ({ id, PhoneNumber }, { dispatch, rejectWithValue }) => {
    try {
      const response = await api.patch('/Member/Update', { id, PhoneNumber  });
      if (response.status === 200) {
        await dispatch(fetchMembers());
        return response.data; 
      }
      return rejectWithValue('Update member failed.');
    } catch (error) {
      return rejectWithValue(error.response?.data || 'Update member failed.');
    }
  }
);

export const deleteMember = createAsyncThunk(
  'member/deleteMember',
  async ({ id }, { dispatch, rejectWithValue }) => {
    try {
      const response = await api.delete(`Member/Delete?Id=${id}`);
      if (response.status === 200) {
        await dispatch(fetchMembers());
        return response.data; 
      }
      return rejectWithValue('Delete member failed.');
    } catch (error) {
      return rejectWithValue(error.response?.data || 'Delete member failed.');
    }
  }
);

export const createMember = createAsyncThunk(
  'member/createMember',
  async ({ formData }, { dispatch, rejectWithValue }) => {
    try {
      const response = await api.post('Member/Create', formData, {
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.status === 200) {
        await dispatch(fetchMembers());
        return;
      }
      return rejectWithValue('Create member failed.');
    } catch (error) {
      const validationErrors = error.response?.data?.errors;
      
      if (validationErrors) {
        const errorMessages = Object.values(validationErrors).flat();
        return rejectWithValue(errorMessages.join(', '));
      }

      return rejectWithValue(error.response?.data || 'Create member failed.');
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
        state.error = action.payload || 'An error occured.';
        state.loading = false;
      });
      builder
      .addCase(updateMember.pending, (state) => {
        state.loading = true;
      })
      .addCase(updateMember.fulfilled, (state, action) => {
        const updatedMember = action.payload;
        state.members = state.members.map((member) =>
          member.id === updatedMember.id ? updatedMember : member
        );
        state.loading = false;
        state.error = null;
      })
      .addCase(updateMember.rejected, (state, action) => {
        state.error = action.payload || 'An error occured.';
        state.loading = false;
      });
      builder
      .addCase(createMember.pending, (state) => {
        state.loading = true;
      })
      .addCase(createMember.fulfilled, (state, action) => {
        // const createdMember = action.payload;
        // state.members = state.members.map((member) =>
        //   createdMember.id === createdMember.id ? createdMember : member
        // );
        state.loading = false;
        state.error = null;
      })
      .addCase(createMember.rejected, (state, action) => {
        state.error = action.payload || 'An error occured.';
        state.loading = false;
      });
  },
});

export default memberSlice.reducer;
