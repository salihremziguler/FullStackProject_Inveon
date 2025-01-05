import React, { useState } from 'react';
import { useUpdateUserMutation } from '../../Api/basketApi';




const UpdateUserComponent = () => {
  const [updateUser] = useUpdateUserMutation();
  const [nameSurname, setNameSurname] = useState('');

  const handleUpdate = async () => {
    try {
      const response = await updateUser({ nameSurname }).unwrap();
      console.log('User updated:', response);
    } catch (error) {
      console.error('Update failed:', error);
    }
  };

  return (
    <div>
      <input
        type="text"
        value={nameSurname}
        onChange={(e) => setNameSurname(e.target.value)}
        placeholder="New Name Surname"
      />
      <button onClick={handleUpdate}>Update User</button>
    </div>
  );
};

export default UpdateUserComponent;
