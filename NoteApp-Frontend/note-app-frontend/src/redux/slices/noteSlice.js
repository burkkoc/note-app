import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import api from "../../Api/api";

export const fetchAllNotes = createAsyncThunk(
  "notes/fetchAll",
  async (_, { rejectWithValue }) => {
    try {
      const response = await api.get("/Note/GetAllNotes");
      return response.data;
    } catch (error) {
      return rejectWithValue(error.response?.data || "Failed to fetch notes");
    }
  }
);

export const updateNote = createAsyncThunk(
  "notes/updateNote",
  async ({ id, Title, Content }, { dispatch, rejectWithValue }) => {
    try {
      const response = await api.patch("/Note/Update", { id, Title, Content });
      if (response.status === 200) {
        await dispatch(fetchAllNotes());
        return response.data;
      }
      return rejectWithValue("Note update failed");
    } catch (error) {
      return rejectWithValue(error.response?.data || "Note update failed");
    }
  }
);

export const deleteNote = createAsyncThunk(
  "member/deleteNote",
  async ({ id }, { dispatch, rejectWithValue }) => {
    try {
      const response = await api.delete(`Note/Delete?Id=${id}`);
      if (response.status === 200) {
        await dispatch(fetchAllNotes());
        return response.data;
      }
      return rejectWithValue("Delete note failed.");
    } catch (error) {
      return rejectWithValue(error.response?.data || "Delete note failed.");
    }
  }
);

export const createNote = createAsyncThunk(
  "member/createNote",
  async ({ formData }, { dispatch, rejectWithValue }) => {
    try {
      const response = await api.post("Note/Create", formData, {
        headers: {
          "Content-Type": "application/json",
        },
      });
      if (response.status === 200) {
        await dispatch(fetchAllNotes());
        return;
      }
      return rejectWithValue("Create note failed.");
    } catch (error) {
      const validationErrors = error.response?.data?.errors;

      if (validationErrors) {
        const errorMessages = Object.values(validationErrors).flat();
        return rejectWithValue(errorMessages.join(", "));
      }

      return rejectWithValue(error.response?.data || "Create note failed.");
    }
  }
);
const initialState = {
  notes: [],
  loading: false,
  error: null,
};

const notesSlice = createSlice({
  name: "notes",
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
        state.error = action.payload || "Something went wrong";
      });
    builder
      .addCase(updateNote.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(updateNote.fulfilled, (state, action) => {
        const updatedNote = action.payload;
        state.notes = state.notes.map((note) =>
          note.id === updatedNote.id ? updatedNote : note
        );
        state.loading = false;
      })
      .addCase(updateNote.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload || "Something went wrong";
      });
    builder
      .addCase(deleteNote.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(deleteNote.fulfilled, (state, action) => {
        state.notes = state.notes.filter((note) => note.id !== action.payload);
        state.loading = false;
      })
      .addCase(deleteNote.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload || "Something went wrong";
      });
    builder
      .addCase(createNote.pending, (state) => {
        state.loading = true;
      })
      .addCase(createNote.fulfilled, (state, action) => {
        const createdNote = action.payload;
        // state.notes = state.notes.map((note) =>
        //   createdNote.id === createdNote.id ? createdNote : note
        // );
        state.loading = false;
        state.error = null;
      })
      .addCase(createNote.rejected, (state, action) => {
        state.error = action.payload || "An error occured.";
        state.loading = false;
      });
  },
});

export default notesSlice.reducer;
