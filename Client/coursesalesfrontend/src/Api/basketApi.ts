import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const basketApi = createApi({
  reducerPath: 'basketApi',
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
    // Sepet öğelerini getir
    getBasketItems: builder.query({
      query: () => 'Baskets',
    }),
    // Sepete öğe ekle
    addItemToBasket: builder.mutation({
      query: (item) => ({
        url: 'Baskets',
        method: 'POST',
        body: item,
      }),
    }),
    // Sepet öğesini kaldır
    removeBasketItem: builder.mutation({
      query: (basketItemId) => ({
        url: `Baskets/${basketItemId}`,
        method: 'DELETE',
      }),
    }),
    // Kullanıcıyı güncelle
    updateUser: builder.mutation({
      query: (userData) => ({
        url: 'Baskets/update-user', // Güncelleme endpoint'i
        method: 'PUT',
        body: userData, // Güncelleme için gerekli kullanıcı verileri
      }),
    }),
  }),
});

export const {
  useGetBasketItemsQuery,
  useAddItemToBasketMutation,
  useRemoveBasketItemMutation,
  useUpdateUserMutation,
} = basketApi;
