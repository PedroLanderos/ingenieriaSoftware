import React, { useState, useEffect, useContext } from "react";
import axios from "axios";
import { AuthContext } from "../Context/AuthContext";
import "./CSS/ManageUsers.css";

const ManageUsers = () => {
  const { auth } = useContext(AuthContext); // Obtener token del contexto
  const [users, setUsers] = useState([]);
  const [editingUser, setEditingUser] = useState(null);
  const [message, setMessage] = useState("");

  // Obtener los usuarios
  const fetchUsers = async () => {
    try {
      const response = await axios.get("http://localhost:5000/api/user", {
        headers: {
          Authorization: `Bearer ${auth.token}`,
        },
      });
      setUsers(response.data);
    } catch (error) {
      console.error("Error al obtener los usuarios:", error);
      setMessage("Error al obtener los usuarios.");
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  // Manejar el click para editar un usuario
  const handleEditClick = (user) => {
    setEditingUser({
      ...user,
      password: "", // Permitir edición de la contraseña
    });
  };

  // Manejar cambios en los campos de entrada del formulario
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setEditingUser((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  // Enviar la actualización de un usuario
  const handleUpdateSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.put(
        "https://localhost:5000/api/user",
        editingUser,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.token}`,
          },
        }
      );
      if (response.status === 200 || response.status === 201) {
        setMessage("Usuario actualizado exitosamente.");
        setEditingUser(null);
        fetchUsers(); // Actualizar lista de usuarios
      }
    } catch (error) {
      console.error("Error al actualizar el usuario:", error);
      setMessage("Error al actualizar el usuario. Intenta de nuevo.");
    }
  };

  return (
    <div className="manage-users">
      <h1>Administrar Usuarios</h1>
      {message && (
        <p
          style={{
            color: message.includes("Error") ? "red" : "green",
            textAlign: "center",
          }}
        >
          {message}
        </p>
      )}
      {!editingUser ? (
        <div className="user-table-container">
          <table className="user-table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Email</th>
                <th>Rol</th>
                <th>Acciones</th>
              </tr>
            </thead>
            <tbody>
              {users.map((user) => (
                <tr key={user.id}>
                  <td>{user.id}</td>
                  <td>{user.name}</td>
                  <td>{user.email}</td>
                  <td>{user.role}</td>
                  <td>
                    <button onClick={() => handleEditClick(user)}>Editar</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ) : (
        <div className="edit-user-container">
          <h2>Editar Usuario</h2>
          <form onSubmit={handleUpdateSubmit} className="edit-user-form">
            <label>
              Nombre:
              <input
                type="text"
                name="Name"
                value={editingUser.Name}
                onChange={handleInputChange}
                required
              />
            </label>
            <label>
              Email:
              <input
                type="email"
                name="Email"
                value={editingUser.Email}
                onChange={handleInputChange}
                required
              />
            </label>
            <label>
              Contraseña:
              <input
                type="password"
                name="Password"
                value={editingUser.Password}
                onChange={handleInputChange}
                required
              />
            </label>
            <label>
              Rol:
              <select
                name="Role"
                value={editingUser.Role}
                onChange={handleInputChange}
                required
              >
                <option value="Normal">Normal</option>
                <option value="Admin">Admin</option>
              </select>
            </label>
            <button type="submit">Guardar Cambios</button>
            <button type="button" onClick={() => setEditingUser(null)}>
              Cancelar
            </button>
          </form>
        </div>
      )}
    </div>
  );
};

export default ManageUsers;
