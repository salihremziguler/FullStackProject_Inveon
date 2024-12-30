import { GetDefaultMiddleware } from './../../node_modules/@reduxjs/toolkit/src/getDefaultMiddleware';
import { Reducer } from './../../node_modules/redux/src/types/reducers';
import { configureStore } from "@reduxjs/toolkit";
import { courseReducer } from "./Redux/courseRedux";
import courseApi from "../Api/courseApi";




const store = configureStore({
    reducer:{
        courseStore : courseReducer,
        [courseApi.reducerPath]:courseApi.reducer
        
    },middleware:(getDefaultMiddleware)=>getDefaultMiddleware().concat(courseApi.middleware)
})



export type RootState = ReturnType<typeof store.getState>;
export default store