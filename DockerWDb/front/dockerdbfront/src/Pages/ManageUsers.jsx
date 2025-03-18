import React, { useState, useEffect, useContext } from "react";
import axios from "axios";
import { AuthContext } from "../Context/AuthContext";
import API_BASE_URL from "../config/apiConfig"; // 🔹 Import API URL

const ManageUsers = () => {
  const { auth } = useContext(AuthContext);
  const [users, setUsers] = useState([]);
  const [editingUser, setEditingUser] = useState(null);
  const [message, setMessage] = useState("");

  const fetchUsers = async () => {
    try {
      const response = await axios.get(`${API_BASE_URL}/user`, { headers: { Authorization: `Bearer ${auth.token}` } }); // 🔹 URL reemplazada
      setUsers(response.data);
    } catch (error) {
      console.error("Error al obtener los usuarios:", error);
      setMessage("Error al obtener los usuarios.");
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  return (
    <div className="manage-users">
      <h1>Administrar Usuarios</h1>
      {message && <p style={{ color: message.includes("Error") ? "red" : "green", textAlign: "center" }}>{message}</p>}
      <div className="user-table-container">
        <table className="user-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Email</th>
              <th>Rol</th>
            </tr>
          </thead>
          <tbody>
            {users.map((user) => (
              <tr key={user.id}>
                <td>{user.id}</td>
                <td>{user.name}</td>
                <td>{user.email}</td>
                <td>{user.role}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default ManageUsers;
