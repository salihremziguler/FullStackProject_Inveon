import React, { useEffect, useState } from 'react';
import { useGetCoursesQuery } from '../../Api/courseApi';
import { courseModel } from '../../interfaces/courseModel';
import { Link } from 'react-router-dom';
import Banner from './Banner';

function CourseList() {
  const { data, isLoading } = useGetCoursesQuery(null);
  const [courses, setCourses] = useState<courseModel[]>([]);
  const [newCourse, setNewCourse] = useState({
    name: '',
    description: '',
    price: 0,
    category: '',
  });

  useEffect(() => {
    if (data) {
      if (Array.isArray(data)) {
        setCourses(data);
      } else if (data.result && Array.isArray(data.result)) {
        setCourses(data.result);
      } else {
        console.warn('Unexpected data structure:', data);
      }
    }
  }, [data]);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setNewCourse((prev) => ({ ...prev, [name]: name === 'price' ? parseFloat(value) : value }));
  };

  const handleAddCourse = async (e: React.FormEvent) => {
    e.preventDefault();
    if (newCourse.name && newCourse.description && newCourse.price && newCourse.category) {
      try {
        const response = await fetch('https://localhost:7154/api/Catalog/AddCourse', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(newCourse),
        });

        if (response.ok) {
          const addedCourse = await response.json();
          setCourses((prev) => [...prev, addedCourse]);
          setNewCourse({ name: '', description: '', price: 0, category: '' });
          alert('Course added successfully!');
        } else {
          alert('Failed to add course. Please try again.');
        }
      } catch (error) {
        console.error('Error:', error);
        alert('An error occurred while adding the course.');
      }
    } else {
      alert('Please fill in all fields.');
    }
  };

  return (
    <div className='container'>
      <Banner></Banner>
      <div className='row'>
        {/* Course Add Form */}
        <div className='col-12'>
          <h3>Add a New Course</h3>
          <form onSubmit={handleAddCourse}>
            <div className='form-group'>
              <label>Course Name</label>
              <input
                type='text'
                className='form-control'
                name='name'
                value={newCourse.name}
                onChange={handleInputChange}
                placeholder='Enter course name'
                required
              />
            </div>
            <div className='form-group'>
              <label>Description</label>
              <input
                type='text'
                className='form-control'
                name='description'
                value={newCourse.description}
                onChange={handleInputChange}
                placeholder='Enter description'
                required
              />
            </div>
            <div className='form-group'>
              <label>Price</label>
              <input
                type='number'
                className='form-control'
                name='price'
                value={newCourse.price}
                onChange={handleInputChange}
                placeholder='Enter price'
                required
              />
            </div>
            <div className='form-group'>
              <label>Category</label>
              <input
                type='text'
                className='form-control'
                name='category'
                value={newCourse.category}
                onChange={handleInputChange}
                placeholder='Enter category'
                required
              />
            </div>
            <button type='submit' className='btn btn-primary mt-3'>
              Add Course
            </button>
          </form>
        </div>
      </div>
      <hr />
      <div className='row'>
        {/* Course List */}
        {!isLoading ? (
          courses && courses.length > 0 ? (
            courses.map((course, index) => (
              <div className='col' key={index}>
                <div className='auction-card text-center'>
                  <div className='card-details text-center'>
                    <h2>{course.name}</h2>
                    <p>
                      <strong>Year:</strong> {course.name}
                    </p>
                    <p>
                      <strong>Description:</strong> {course.description}
                    </p>
                    <p>
                      <strong>Price:</strong> ${course.price}
                    </p>
                    <p>
                      <strong>Category:</strong> {course.category}
                    </p>
                  </div>
                  <div>
                    <Link to={`Course/CourseId/${course.id}`}>
                      <button className='btn btn-danger'>Details</button>
                    </Link>
                  </div>
                </div>
              </div>
            ))
          ) : (
            <p>No courses available.</p>
          )
        ) : (
          <p>Loading...</p>
        )}
      </div>
    </div>
  );
}

export default CourseList;
