import { createSlice } from "@reduxjs/toolkit";
import userModel from "../../interfaces/userModel.ts";
import { act } from "react-dom/test-utils";


export const InitialState : userModel = {
   name:""
}


export const authenticationSlice = createSlice({
    name:"authentication",
    initialState:InitialState,
    reducers:{
        setLoggedInUser:(state,action)=> {
            state.name = action.payload.name;
            
        }
    }
})

export const {setLoggedInUser} = authenticationSlice.actions
export const authenticationReducer = authenticationSlice.reducer;