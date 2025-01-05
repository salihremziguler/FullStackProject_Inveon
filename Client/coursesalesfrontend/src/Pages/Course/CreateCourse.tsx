import React, { useState } from 'react';
import { useAddCourseWithImageMutation } from '../../Api/courseApi';


import { Loader } from '../../Helper';

function AddCourse() {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [category, setCategory] = useState('');
  const [image, setImage] = useState(null);

  const [addCourseWithImage, { isLoading }] = useAddCourseWithImageMutation();

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!image) {
      alert('Please select an image.');
      return;
    }

    const formData = new FormData();
    formData.append('Name', name);
    formData.append('Description', description);
    formData.append('Price', price);
    formData.append('Category', category);
    formData.append('Image', image);

    try {
      await addCourseWithImage(formData).unwrap();
      alert('Course added successfully!');
      setName('');
      setDescription('');
      setPrice('');
      setCategory('');
      setImage(null);
    } catch (error) {
      console.error('Failed to add course:', error);
      alert('Failed to add course.');
    }
  };

  return (
    <div className="add-course-container">
      <h2 className="add-course-title">Add New Course</h2>
      {isLoading && <Loader />} {/* Loader gösterimi */}
      <form onSubmit={handleSubmit} className="course-form">
        <div className="form-group">
          <label>Course Name</label>
          <input
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Enter course name"
            required
          />
        </div>
        <div className="form-group">
          <label>Description</label>
          <textarea
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Enter course description"
            required
          />
        </div>
        <div className="form-group">
          <label>Price</label>
          <input
            type="number"
            value={price}
            onChange={(e) => setPrice(e.target.value)}
            placeholder="Enter price"
            required
          />
        </div>
        <div className="form-group">
          <label>Category</label>
          <input
            type="text"
            value={category}
            onChange={(e) => setCategory(e.target.value)}
            placeholder="Enter category"
            required
          />
        </div>
        <div className="form-group">
          <label>Image</label>
          <input
            type="file"
            accept="image/*"
            onChange={(e) => setImage(e.target.files[0])}
            required
          />
        </div>
        <button type="submit" className="btn btn-primary" disabled={isLoading}>
          {isLoading ? 'Adding...' : 'Add Course'}
        </button>
      </form>
    </div>
  );
}

export default AddCourse;
