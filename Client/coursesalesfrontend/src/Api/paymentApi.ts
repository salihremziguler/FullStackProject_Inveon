import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const paymentApi = createApi({
  reducerPath: 'paymentApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://localhost:7154/api/', // API'nin temel URL'si
    prepareHeaders: (headers) => {
      const token = localStorage.getItem('token'); // Token'ı localStorage'dan al
      if (token) {
        headers.set('Authorization', `Bearer ${token}`); // Authorization başlığı ekle
      }
      return headers;
    },
  }),
  endpoints: (builder) => ({
    // Ödeme yap
    makePayment: builder.mutation({
      query: (paymentData) => ({
        url: 'Payment/Pay', // Payment endpoint'i
        method: 'POST',
        body: paymentData, // Ödeme için gerekli veriler
      }),
    }),
  }),
});

export const { useMakePaymentMutation } = paymentApi;
