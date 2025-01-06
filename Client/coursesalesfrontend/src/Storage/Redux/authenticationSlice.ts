import { createSlice } from "@reduxjs/toolkit";
import userModel from "../../interfaces/userModel.ts";

export const InitialState: userModel = {
  name: "",
  role: "", // Role özelliği eklendi
};

export const authenticationSlice = createSlice({
  name: "authentication",
  initialState: InitialState,
  reducers: {
    setLoggedInUser: (state, action) => {
      state.name = action.payload.name;
      state.role = action.payload.role; // Role özelliği güncellendi
    },
  },
});

export const { setLoggedInUser } = authenticationSlice.actions;
export const authenticationReducer = authenticationSlice.reducer;
