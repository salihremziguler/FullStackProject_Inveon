import React, { useState } from 'react';
import { useUpdateUserMutation } from '../../Api/userEditApi';

const UpdateUserForm = () => {
  const [nameSurname, setNameSurname] = useState('');
  const [updateUser, { isLoading }] = useUpdateUserMutation();

  const handleUpdate = async () => {
    try {
      const response = await updateUser({ nameSurname }).unwrap();
      alert("Kullanıcı bilgileri güncellendi"); // API'den dönen mesajı göster
      console.log('User updated:', response);
    } catch (error) {
      console.error('Failed to update user:', error);
      alert('Kullanıcı bilgileri güncellenemedi.');
    }
  };

  return (
    <div style={{ maxWidth: '400px', margin: 'auto', padding: '20px', border: '1px solid #ccc', borderRadius: '10px' }}>
      <h2>Kullanıcı Bilgilerini Güncelle</h2>
      <div style={{ marginBottom: '15px' }}>
        <label htmlFor="nameSurname" style={{ display: 'block', marginBottom: '5px' }}>Ad Soyad</label>
        <input
          type="text"
          id="nameSurname"
          value={nameSurname}
          onChange={(e) => setNameSurname(e.target.value)}
          placeholder="Ad Soyad girin"
          style={{ width: '100%', padding: '8px', borderRadius: '5px', border: '1px solid #ddd' }}
        />
      </div>
      <button
        onClick={handleUpdate}
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
    </div>
  );
};

export default UpdateUserForm;
