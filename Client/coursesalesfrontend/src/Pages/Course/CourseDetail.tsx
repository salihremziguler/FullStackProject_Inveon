import React from 'react';
import { useParams } from 'react-router-dom';
import { useGetCourseByIdQuery } from '../../Api/courseApi';
import { Loader } from '../../Helper';
import './Styles/CourseDetail.css';

function CourseDetail() {
  const { courseId } = useParams(); // Dinamik parametre
  const { data, isLoading, error } = useGetCourseByIdQuery(courseId); // Hook kullanımı

  if (isLoading) {
    return <Loader />;
  }

  if (error) {
    return <p>Failed to load course details.</p>;
  }

  const course = data?.result || data;

  if (!course) {
    return <p>No course found with the given ID.</p>;
  }

  return (
    <div className='auction-item text-center'>
      <h2>Brand-Model: {course.id || 'N/A'}</h2>
      <p>Category: {course.category || 'N/A'}</p>
      <p>Description: {course.description || 'N/A'}</p>
      <p>Price: ${course.price || 'N/A'}</p>
    </div>
  );
}

export default CourseDetail;
