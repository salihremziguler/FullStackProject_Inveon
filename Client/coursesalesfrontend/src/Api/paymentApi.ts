import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const paymentApi = createApi({
  reducerPath: 'paymentApi',
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
    // Ödeme yap
    makePayment: builder.mutation({
      query: (paymentData) => ({
        url: 'Payment/Pay', 
        method: 'POST',
        body: paymentData, 
      }),
    }),
  }),
});

export const { useMakePaymentMutation } = paymentApi;
