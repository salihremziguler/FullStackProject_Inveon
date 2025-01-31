import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const userEditApi = createApi({
    reducerPath: 'userEditApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://localhost:7154/api/UserEdit',
    prepareHeaders: (headers) => {
      const token = localStorage.getItem('token'); 
      if (token) {
        headers.set('Authorization', `Bearer ${token}`); 
      }
      return headers;
    },
  }),
  endpoints: (builder) => ({
    updateUser: builder.mutation({
      query: (userData) => ({
        url: 'update-user',
        method: 'PUT',
        body: userData,
      }),
      
    }),
    deleteUser: builder.mutation({
      query: (userId) => ({
        url: `delete-user/${userId}`,
        method: "DELETE",
      })

  })
})
})

export const { useUpdateUserMutation,useDeleteUserMutation  } = userEditApi;
export default userEditApi;
