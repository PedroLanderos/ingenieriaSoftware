import React, { useState, useContext } from "react";
import "./CSS/LoginSignup.css";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../Context/AuthContext"; // Importa el contexto

const LoginSignup = () => {
  const [isRegister, setIsRegister] = useState(false);
  const [formData, setFormData] = useState({
    Email: "", // Ajustado a como lo espera el backend
    Password: "",
    Name: "",
    Role: "Normal", // Valor por defecto
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

      console.log("Respuesta del servidor:", response.data); // Debugging

      if (response.status === 200) {
        if (isRegister) {
          setMessage("Usuario registrado exitosamente.");
          setIsRegister(false);
        } else {
          const token = response.data.token; // Asegúrate de que el backend devuelve `token`

          if (!token || typeof token !== "string") {
            throw new Error("Token inválido o ausente.");
          }

          login(token); // Iniciar sesión en el contexto
          navigate("/"); // Redirige a la página principal
        }
      }
    } catch (error) {
      console.error("Error en handleSubmit:", error);
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
            <input
              type="text"
              name="Name"
              placeholder="Nombre Completo"
              value={formData.Name}
              onChange={handleChange}
              required
            />
          )}
          <input
            type="email"
            name="Email"
            placeholder="Correo Electrónico"
            value={formData.Email}
            onChange={handleChange}
            required
          />
          <input
            type="password"
            name="Password"
            placeholder="Contraseña"
            value={formData.Password}
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
