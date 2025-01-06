import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const basketApi = createApi({
  reducerPath: 'basketApi',
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
   
    getBasketItems: builder.query({
      query: () => 'Baskets',
    }),
    
    addItemToBasket: builder.mutation({
      query: (item) => ({
        url: 'Baskets',
        method: 'POST',
        body: item,
      }),
    }),
   
    removeBasketItem: builder.mutation({
      query: (basketItemId) => ({
        url: `Baskets/${basketItemId}`,
        method: 'DELETE',
      }),
    }),
   
   
  }),
});

export const {
  useGetBasketItemsQuery,
  useAddItemToBasketMutation,
  useRemoveBasketItemMutation,
  
} = basketApi;
