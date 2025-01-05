import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const purchasedCoursesApi = createApi({
  reducerPath: 'purchasedCoursesApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://localhost:7154/api/',
    prepareHeaders: (headers) => {
      const token = localStorage.getItem('token');
      if (token) {
        headers.set('Authorization', `Bearer ${token}`);
      }
      return headers;
    },
  }),
  endpoints: (builder) => ({
    getPurchasedCourses: builder.query({
      query: () => 'Payment/PurchasedCourses',
    }),
  }),
});

export const { useGetPurchasedCoursesQuery } = purchasedCoursesApi;
