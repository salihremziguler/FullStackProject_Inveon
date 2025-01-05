import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const courseApi = createApi({
  reducerPath: 'courseApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://localhost:7154/api/Catalog/',
  }),
  endpoints: (builder) => ({
    getCourses: builder.query({
      query: () => 'GetCourses',
    }),
    getCourseById: builder.query({
      query: (id) => `GetCourseById/${id}`,
    }),
    addCourseWithImage: builder.mutation({
      query: (formData) => ({
        url: 'AddCourseWithImage',
        method: 'POST',
        body: formData, // FormData olarak gönderiliyor
      }),
    }),
  }),
});

export const {
  useGetCoursesQuery,
  useGetCourseByIdQuery,
  useAddCourseWithImageMutation,
} = courseApi;
export default courseApi;
