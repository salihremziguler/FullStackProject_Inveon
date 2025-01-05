import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const userApi = createApi({
    reducerPath:"userApi",
    baseQuery:fetchBaseQuery({
        baseUrl:"https://localhost:7154/api/Users/",
    }),
    endpoints:(builder) => ({
        signUp:builder.mutation({
            query:(userData) => ({
                url:"CreateUser",
                method:"POST",
                headers:{
                    "Content-type":"application/json"
                },
                body:userData
            })
        }),
        signIn:builder.mutation({
            query:(userData) => ({
                url:"Login",
                method:"POST",
                headers:{
                    "Content-type":"application/json"
                },
                body:userData
            })
        })
    })

})


export const {useSignUpMutation,useSignInMutation} = userApi