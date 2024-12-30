import { createSlice } from "@reduxjs/toolkit";


const initialState={
    courses:[]

}

export const courseSlice=createSlice({
    name:"course",
    initialState:initialState,
    reducers:{
        getCourses:(state,action)=>{
            state.courses=action.payload;

        }
    }
}
)


export const {getCourses}=courseSlice.actions
export const courseReducer=courseSlice.reducer;