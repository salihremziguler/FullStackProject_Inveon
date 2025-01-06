import React from 'react';
import { useGetPurchasedCoursesQuery } from '../../Api/purchasedCoursesApi';
import { Loader } from '../../Helper';
import './Styles/PurchasedCourse.css'; // CSS dosyası

function PurchasedCourses() {
  const { data, isLoading, error } = useGetPurchasedCoursesQuery();

  // Verileri kontrol etmek için log
  console.log('Purchased Courses Data:', data);

  if (isLoading) return <Loader />; // Yükleme sırasında Loader göster
  if (error) return <p className="error-message">Kurslar yüklenirken bir hata oluştu: {error.message || 'Bilinmeyen bir hata'}</p>;

  // Gelen verinin dizi olup olmadığını kontrol edin
  const purchasedCourses = Array.isArray(data) ? data : [];

  return (
    <div className="purchased-courses">
      <h1 className="purchased-courses-title">Satın Alınan Kurslar</h1>
      {purchasedCourses.length === 0 ? (
        <p className="no-courses-message">Hiç kurs satın alınmamış.</p>
      ) : (
        <div className="course-list">
          {purchasedCourses.map((course: any, index: number) => (
            <div key={index} className="course-card">
              <h2 className="course-name">{course.courseName}</h2>
              <p className="course-detail"><strong>Satın Alma Tarihi:</strong> {new Date(course.purchaseDate).toLocaleDateString()}</p>
              <p className="course-detail"><strong>Fiyat:</strong> ${course.price}</p>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

export default PurchasedCourses;
