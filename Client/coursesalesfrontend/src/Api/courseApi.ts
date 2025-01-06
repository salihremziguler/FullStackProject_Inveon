import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const courseApi = createApi({
  reducerPath: 'courseApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://localhost:7154/api/Catalog/',
  }),
  endpoints: (builder) => ({
    getCourses: builder.query({
      query: ({ page, size }) => `GetCourses?page=${page}&size=${size}`,
    }),
    getCourseById: builder.query({
      query: (id) => `GetCourseById/${id}`,
    }),
    addCourseWithImage: builder.mutation({
      query: (formData) => ({
        url: 'AddCourseWithImage',
        method: 'POST',
        body: formData,
      }),
    }),
    updateCourse: builder.mutation({
      query: (updatedCourse) => ({
        url: '',
        method: 'PUT',
        body: updatedCourse,
      }),
    }),
    deleteCourse: builder.mutation({
      query: (id) => ({
        url: `${id}`,
        method: 'DELETE',
      }),
    }),
  }),
});

export const {
  useGetCoursesQuery,
  useGetCourseByIdQuery,
  useAddCourseWithImageMutation,
  useUpdateCourseMutation,
  useDeleteCourseMutation,
} = courseApi;
export default courseApi;
