import { GetDefaultMiddleware } from './../../node_modules/@reduxjs/toolkit/src/getDefaultMiddleware';
import { Reducer } from './../../node_modules/redux/src/types/reducers';
import { configureStore } from "@reduxjs/toolkit";
import { courseReducer } from "./Redux/courseRedux";
import courseApi from "../Api/courseApi";
import { accountApi } from '../Api/accountApi';
import { userApi } from '../Api/userApi';
import { authenticationReducer } from './Redux/authenticationSlice';
import { basketApi } from '../Api/basketApi';
import { paymentApi } from '../Api/paymentApi';
import { orderApi } from '../Api/orderApi';
import { purchasedCoursesApi } from '../Api/purchasedCoursesApi';
import userEditApi from '../Api/userEditApi';




const store = configureStore({
    reducer:{
        courseStore : courseReducer,
        authenticationStore:authenticationReducer,
        [courseApi.reducerPath]:courseApi.reducer,
        [accountApi.reducerPath] : accountApi.reducer,
        [userApi.reducerPath] : userApi.reducer,
        [basketApi.reducerPath]: basketApi.reducer,
        [paymentApi.reducerPath]: paymentApi.reducer,
        
        [purchasedCoursesApi.reducerPath]: purchasedCoursesApi.reducer,
        [userEditApi.reducerPath]:userEditApi.reducer


        
    },middleware:(getDefaultMiddleware)=>getDefaultMiddleware().concat(courseApi.middleware,accountApi.middleware,userApi.middleware,basketApi.middleware,paymentApi.middleware,purchasedCoursesApi.middleware,userEditApi.middleware)
})



export type RootState = ReturnType<typeof store.getState>;
export default store