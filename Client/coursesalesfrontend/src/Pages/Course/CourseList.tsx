import React, { useEffect, useState } from 'react';
import { useGetCoursesQuery } from '../../Api/courseApi';
import { courseModel } from '../../interfaces/courseModel';
import { Link } from 'react-router-dom';
import Banner from './Banner';
import Pagination from './Pagination'; // Sayfalama Bileşeni
import './Styles/CourseList.css';
import { Loader } from '../../Helper';

function CourseList() {
  const [currentPage, setCurrentPage] = useState(0);
  const [pageSize] = useState(10); // Sayfa başına kurs sayısı
  const { data, isLoading } = useGetCoursesQuery({ page: currentPage, size: pageSize });
  const [courses, setCourses] = useState<courseModel[]>([]);
  const [filteredCourses, setFilteredCourses] = useState<courseModel[]>([]);

  useEffect(() => {
    if (data?.courses) {
      if (Array.isArray(data.courses)) {
        setCourses(data.courses);
        setFilteredCourses(data.courses);
      } else {
        console.warn('Unexpected data structure:', data);
      }
    }
  }, [data]);

  const handleSearch = (searchTerm: string) => {
    const lowercasedSearchTerm = searchTerm.toLowerCase();
    const filtered = courses.filter((course) =>
      course.name.toLowerCase().includes(lowercasedSearchTerm)
    );
    setFilteredCourses(filtered);
  };

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  return (
    <div className="course-list-container">
      <Banner onSearch={handleSearch} />
      <div className="course-grid">
        {isLoading ? (
          <Loader />
        ) : filteredCourses && filteredCourses.length > 0 ? (
          filteredCourses.map((course, index) => (
            <div className="course-card" key={index}>
              <div className="course-image-wrapper">
                <img
                  src={`https://localhost:7154${course.image}`}
                  alt={course.name}
                  className="course-image"
                />
              </div>
              <div className="course-details">
                <h2 className="course-title">{course.name}</h2>
                <p className="course-description">
                  {course.description.length > 100
                    ? `${course.description.substring(0, 100)}...`
                    : course.description}
                </p>
                <p className="course-price">
                  <strong>Price:</strong> ${course.price}
                </p>
                <p className="course-category">
                  <strong>Category:</strong> {course.category}
                </p>
              </div>
              <Link to={`Course/CourseId/${course.id}`} className="details-link">
                <button className="btn btn-primary">Detaylar</button>
              </Link>
            </div>
          ))
        ) : (
          <p className="no-courses-message">No courses available.</p>
        )}
      </div>
      <Pagination
        currentPage={currentPage}
        totalPages={Math.ceil(data?.totalCourseCount / pageSize) || 1}
        onPageChange={handlePageChange}
      />
    </div>
  );
}

export default CourseList;
