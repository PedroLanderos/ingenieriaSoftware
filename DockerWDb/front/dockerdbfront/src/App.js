import React, { useContext } from "react";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import LoginSignup from "./Pages/LoginSignup";
import PanelDeAdministrador from "./Pages/PanelDeAdministrador";
import AddUser from "./Pages/AddUser";
import MainPage from "./Pages/MainPage"; 
import { AuthContext } from "./Context/AuthContext";

function App() {
  const { auth } = useContext(AuthContext);
  const isAuthenticated = auth.isAuthenticated;
  const isAdmin = auth?.user?.role === "Admin";

  return (
    <BrowserRouter>
      <Routes>
        {/* ✅ Si está autenticado, redirigir a MainPage si no es Admin o al panel si lo es */}
        <Route path="/" element={isAuthenticated ? (isAdmin ? <Navigate to="/panelAdministrador" /> : <Navigate to="/MainPage" />) : <LoginSignup />} />

        {/* ✅ Si intenta acceder a /login manualmente estando autenticado, redirigir a MainPage o al Panel */}
        <Route path="/login" element={!isAuthenticated ? <LoginSignup /> : (isAdmin ? <Navigate to="/panelAdministrador" /> : <Navigate to="/MainPage" />)} />

        {/* ✅ Redirigir a MainPage si no es Admin */}
        <Route path="/panelAdministrador" element={isAdmin ? <PanelDeAdministrador /> : <Navigate to="/MainPage" />} />

        {/* ✅ Ruta para agregar usuarios (accesible solo si es Admin) */}
        <Route path="/AddUser" element={isAdmin ? <AddUser /> : <Navigate to="/MainPage" />} />

        {/* ✅ Página Principal para usuarios normales */}
        <Route path="/MainPage" element={<MainPage />} />

        {/* ✅ Redirección de rutas no encontradas */}
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
