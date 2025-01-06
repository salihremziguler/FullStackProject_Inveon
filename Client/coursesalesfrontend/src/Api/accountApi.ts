import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const accountApi = createApi({
  reducerPath: "accountApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "https://localhost:7154/api/Auth/",
  }),
  endpoints: (builder) => ({
    // Kayıt olma
    signUp: builder.mutation({
      query: (userData) => ({
        url: "Register",
        method: "POST",
        headers: {
          "Content-type": "application/json",
        },
        body: userData,
      }),
    }),
    // Giriş yapma
    signIn: builder.mutation({
      query: (userData) => ({
        url: "Login",
        method: "POST",
        headers: {
          "Content-type": "application/json",
        },
        body: userData,
      }),
    }),
    // Kullanıcı adı güncelleme
    updateUser: builder.mutation({
      query: (userData) => ({
        url: "update-username", // API'deki `update-username` endpointine uygun hale getirildi
        method: "PUT",
        headers: {
          "Content-type": "application/json",
        },
        body: userData,
      }),
    }),
  }),
});

export const { useSignUpMutation, useSignInMutation, useUpdateUserMutation } = accountApi;
