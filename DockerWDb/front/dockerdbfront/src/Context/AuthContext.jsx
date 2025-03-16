import React, { createContext, useState, useEffect } from "react";
import { jwtDecode } from "jwt-decode";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [auth, setAuth] = useState({
    isAuthenticated: false,
    token: null,
    user: null,
  });

  useEffect(() => {
    const token = localStorage.getItem("token");
    const user = JSON.parse(localStorage.getItem("user"));
    if (token && user) {
      setAuth({
        isAuthenticated: true,
        token,
        user,
      });
    }
  }, []);

  const login = (token) => {
    if (!token || typeof token !== "string") {
      console.error("Token inválido al intentar decodificar:", token);
      return;
    }

    try {
      const decodedToken = jwtDecode(token);

      console.log("Token decodificado:", decodedToken); // Verifica el contenido del token

      const user = {
        id: decodedToken.UserId ? parseInt(decodedToken.UserId, 10) : null,
        name: decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] || "Usuario",
        email: decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] || "Sin correo",
        role: decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || "Normal",
      };

      localStorage.setItem("token", token);
      localStorage.setItem("user", JSON.stringify(user));

      setAuth({
        isAuthenticated: true,
        token,
        user,
      });
    } catch (error) {
      console.error("Error al decodificar el token:", error);
      logout();
    }
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    setAuth({
      isAuthenticated: false,
      token: null,
      user: null,
    });
  };

  return (
    <AuthContext.Provider value={{ auth, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
