import React from "react";
import { useDeleteUserMutation } from "../../Api/userEditApi";

type User = {
    id: string; 
    nameSurname: string;
    email: string;
  };

function UserList({ users }: { users: User[] }) {
  const [deleteUser] = useDeleteUserMutation();

  const handleDelete = async (userId:string) => {
    try {
      const response = await deleteUser(userId).unwrap();
      alert(response.message); 
    } catch (error) {
      alert("Kullanıcı silinemedi.");
    }
  };

  return (
    <div>
      <h2>Kullanıcı Listesi</h2>
      <ul>
        {users.map((user) => (
          <li key={user.id}>
            {user.nameSurname} - {user.email}
            <button onClick={() => handleDelete(user.id)}>Sil</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default UserList;
