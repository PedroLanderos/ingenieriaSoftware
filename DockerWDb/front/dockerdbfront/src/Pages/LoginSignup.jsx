import React, { useState, useContext } from "react";
import "./CSS/LoginSignup.css";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../Context/AuthContext"; // Importa el contexto

const LoginSignup = () => {
  const [isRegister, setIsRegister] = useState(false);
  const [formData, setFormData] = useState({
    email: "",
    password: "",
    Name: "",
    Role: "Normal", // Asignamos "Normal" como valor por defecto
  });
  const [message, setMessage] = useState("");
  const navigate = useNavigate();
  const { login } = useContext(AuthContext); // Usa la función de login del contexto

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const endpoint = isRegister
      ? "http://localhost:5000/api/user/register" // Endpoint para registro
      : "http://localhost:5000/api/user/login"; // Endpoint para login

    try {
      const response = await axios.post(endpoint, formData, {
        headers: { "Content-Type": "application/json" },
      });

      if (response.status === 200) {
        if (isRegister) {
          setMessage("Usuario registrado exitosamente.");
          setIsRegister(false); // Cambiar a la pantalla de login
        } else {
          const token = response.data.message; // Extraer el token JWT
          const user = {
            name: response.data.name || "Usuario",
            email: formData.email,
            role: response.data.role, // Aquí asumimos que el rol está siendo enviado
          }; // Obtener detalles del usuario (personalízalo según tu API)
          login(token, user); // Llama al contexto para iniciar sesión

          // Verifica el rol del usuario
          if (user.role === "Admin") {
            navigate("/admin-panel"); // Redirige al panel de administrador si el rol es Admin
          } else {
            setMessage("Inicio de sesión exitoso"); // Muestra mensaje si no es admin
          }
        }
      }
    } catch (error) {
      console.error(error);
      setMessage(
        isRegister
          ? "Error al registrar usuario. Verifica los datos."
          : "Error al iniciar sesión. Verifica los datos."
      );
    }
  };

  return (
    <div className="loginSignup">
      <div className="loginSignup-container">
        <h1>{isRegister ? "Registro" : "Iniciar Sesión"}</h1>
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
        <form onSubmit={handleSubmit} className="loginSignup-fields">
          {isRegister && (
            <>
              <input
                type="text"
                name="Name"
                placeholder="Nombre Completo"
                value={formData.Name}
                onChange={handleChange}
                required
              />
            </>
          )}
          <input
            type="email"
            name="email"
            placeholder="Correo Electrónico"
            value={formData.email}
            onChange={handleChange}
            required
          />
          <input
            type="password"
            name="password"
            placeholder="Contraseña"
            value={formData.password}
            onChange={handleChange}
            required
          />
          {isRegister && (
            <select
              name="Role"
              value={formData.Role}
              onChange={handleChange}
              required
            >
              <option value="Normal">Normal</option>
              <option value="Admin">Admin</option>
            </select>
          )}
          <button type="submit">{isRegister ? "Registrar" : "Iniciar Sesión"}</button>
        </form>
        <div className="loginSignup-toggle">
          {isRegister ? (
            <>
              ¿Ya tienes una cuenta?{" "}
              <span onClick={() => setIsRegister(false)}>Inicia Sesión</span>
            </>
          ) : (
            <>
              ¿No tienes una cuenta?{" "}
              <span onClick={() => setIsRegister(true)}>Regístrate</span>
            </>
          )}
        </div>
      </div>
    </div>
  );
};

export default LoginSignup;
