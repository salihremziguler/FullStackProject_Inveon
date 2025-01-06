import React, { useState } from 'react';
import { useAddCourseWithImageMutation } from '../../Api/courseApi';
import styles from './Styles/CreateCourse.module.css'; // CSS Module dosyasını içeri aktarıyoruz.
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
    <div className={styles.container}>
      <h2 className={styles.title}>Add New Course</h2>
      {isLoading && <Loader />} {/* Loader gösterimi */}
      <form onSubmit={handleSubmit} className={styles.form}>
        <div className={styles.group}>
          <label htmlFor="course-name" className={styles.label}>Course Name</label>
          <input
            id="course-name"
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Enter course name"
            className={styles.input}
            required
          />
        </div>

        <div className={styles.group}>
          <label htmlFor="course-description" className={styles.label}>Description</label>
          <textarea
            id="course-description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Enter course description"
            className={styles.textarea}
            required
          />
        </div>

        <div className={styles.group}>
          <label htmlFor="course-price" className={styles.label}>Price</label>
          <input
            id="course-price"
            type="number"
            value={price}
            onChange={(e) => setPrice(e.target.value)}
            placeholder="Enter price"
            className={styles.input}
            required
          />
        </div>

        <div className={styles.group}>
          <label htmlFor="course-category" className={styles.label}>Category</label>
          <input
            id="course-category"
            type="text"
            value={category}
            onChange={(e) => setCategory(e.target.value)}
            placeholder="Enter category"
            className={styles.input}
            required
          />
        </div>

        <div className={styles.group}>
          <label htmlFor="course-image" className={styles.label}>Image</label>
          <input
            id="course-image"
            type="file"
            accept="image/*"
            onChange={(e) => setImage(e.target.files[0])}
            className={styles.input}
            required
          />
        </div>

        <button type="submit" className={styles.button} disabled={isLoading}>
          {isLoading ? 'Adding...' : 'Add Course'}
        </button>
      </form>
    </div>
  );
}

export default AddCourse;
