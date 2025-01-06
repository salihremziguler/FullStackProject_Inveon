import React from 'react';
import { useParams } from 'react-router-dom';
import { useGetCourseByIdQuery } from '../../Api/courseApi';
import { useAddItemToBasketMutation } from '../../Api/basketApi';
import { Loader } from '../../Helper'; // Loader bileşeni
import './Styles/CourseDetail.css';

// ErrorMessage Bileşeni
function ErrorMessage({ message }: { message: string }) {
  return (
    <div className="error-message">
      <p>{message}</p>
    </div>
  );
}

function CourseDetail() {
  const { courseId } = useParams(); // Dinamik parametre
  const { data, isLoading, error } = useGetCourseByIdQuery(courseId); // API çağrısı
  const [addItemToBasket, { isLoading: isAdding }] = useAddItemToBasketMutation();

  // Yüklenme ekranı
  if (isLoading) {
    return <Loader />; 
  }

  // Hata durumunda mesaj
  if (error) {
    return <ErrorMessage message="Kurs detayları yüklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin." />;
  }

  // Kurs verileri
  const course = data?.result || data;

  if (!course) {
    return <ErrorMessage message="Bu ID ile eşleşen bir kurs bulunamadı." />;
  }

  // Sepete ekleme işlemi
  const handleAddToBasket = async () => {
    try {
      await addItemToBasket({ courseId: course.id, quantity: 1 }).unwrap();
      alert('Kurs başarıyla sepete eklendi!');
    } catch (error) {
      console.error('Failed to add course to basket:', error);
      alert('Kurs sepete eklenirken bir hata oluştu.');
    }
  };

  return (
    <div className="course-detail-container">
      <div className="course-detail-card">
        <img
          src={`https://localhost:7154${course.image}`}
          alt={course.name}
          className="course-image"
        />

        <h2 className="course-title">{course.name || 'N/A'}</h2>

        <div className="course-info">
          <p><strong>Category:</strong> {course.category || 'N/A'}</p>
          <p><strong>Description:</strong> {course.description || 'N/A'}</p>
          <p><strong>Price:</strong> ${course.price || 'N/A'}</p>
        </div>

        <button
          className="btn btn-primary add-to-basket-btn"
          onClick={handleAddToBasket}
          disabled={isAdding}
        >
          {isAdding ? <Loader /> : 'Sepete Ekle'}
        </button>
      </div>
    </div>
  );
}

export default CourseDetail;
