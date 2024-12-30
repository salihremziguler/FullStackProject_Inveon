import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const courseApi = createApi({
  reducerPath: 'courseApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://localhost:7154/api/Catalog/',
  }),
  endpoints: (builder) => ({
    getCourses: builder.query({
      query: () => 'GetCourses', // Tüm kursları getiren endpoint
    }),
    getCourseById: builder.query({
      query: (id) => `GetCourseById/${id}`, // Belirli bir kursu ID'ye göre getiren endpoint
    }),
  }),
});

export const { useGetCoursesQuery, useGetCourseByIdQuery } = courseApi;
export default courseApi;
