import React, { useState } from 'react';
import { useUpdateUserMutation } from '../../Api/userEditApi';

function UpdateUserForm() {
  const [formData, setFormData] = useState({
    nameSurname: '',
    currentPassword: '',
    newPassword: '',
  });

  const [updateUser, { isLoading }] = useUpdateUserMutation();

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await updateUser(formData).unwrap();
      alert(response.message); // API'den dönen mesajı göster
    } catch (error) {
      console.error('Failed to update user:', error);
      alert('Kullanıcı bilgileri güncellenemedi.');
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  return (
    <div style={{ maxWidth: '400px', margin: 'auto', padding: '20px', border: '1px solid #ccc', borderRadius: '10px' }}>
      <h2>Kullanıcı Bilgilerini Güncelle</h2>
      <form onSubmit={handleSubmit}>
        <div style={{ marginBottom: '15px' }}>
          <label htmlFor="nameSurname" style={{ display: 'block', marginBottom: '5px' }}>Ad Soyad</label>
          <input
            type="text"
            id="nameSurname"
            name="nameSurname"
            value={formData.nameSurname}
            onChange={handleChange}
            placeholder="Ad Soyad girin"
            style={{ width: '100%', padding: '8px', borderRadius: '5px', border: '1px solid #ddd' }}
          />
        </div>
        <div style={{ marginBottom: '15px' }}>
          <label htmlFor="currentPassword" style={{ display: 'block', marginBottom: '5px' }}>Mevcut Şifre</label>
          <input
            type="password"
            id="currentPassword"
            name="currentPassword"
            value={formData.currentPassword}
            onChange={handleChange}
            placeholder="Mevcut şifre"
            style={{ width: '100%', padding: '8px', borderRadius: '5px', border: '1px solid #ddd' }}
          />
        </div>
        <div style={{ marginBottom: '15px' }}>
          <label htmlFor="newPassword" style={{ display: 'block', marginBottom: '5px' }}>Yeni Şifre</label>
          <input
            type="password"
            id="newPassword"
            name="newPassword"
            value={formData.newPassword}
            onChange={handleChange}
            placeholder="Yeni şifre"
            style={{ width: '100%', padding: '8px', borderRadius: '5px', border: '1px solid #ddd' }}
          />
        </div>
        <button
          type="submit"
          style={{
            padding: '10px 20px',
            backgroundColor: '#007bff',
            color: '#fff',
            border: 'none',
            borderRadius: '5px',
            cursor: 'pointer',
          }}
          disabled={isLoading}
        >
          {isLoading ? 'Güncelleniyor...' : 'Güncelle'}
        </button>
      </form>
    </div>
  );
}

export default UpdateUserForm;
