import React, { useContext } from 'react'; // 🔹 Se agrega el import de useContext
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'; // 🔹 Se agrega Navigate
import LoginSignup from './Pages/LoginSignup';
import PanelDeAdministrador from './Pages/PanelDeAdministrador';
import AddUser from './Pages/AddUser';
import { AuthContext } from "./Context/AuthContext";

function App() {
  const { auth } = useContext(AuthContext);

  return (
    <BrowserRouter>
      <Routes>
        {/* Ruta principal: Login */}
        <Route path='/' element={<LoginSignup />} />

        {/* Ruta protegida: Solo permite acceso a Admins */}
        <Route 
          path="/panelAdministrador" 
          element={auth?.user?.role === "Admin" ? <PanelDeAdministrador /> : <Navigate to="/" />}
        />

        {/* Ruta para Login (por si se accede directamente) */}
        <Route path='/login' element={<LoginSignup />} />

        {/* Ruta para agregar usuarios */}
        <Route path='/AddUser' element={<AddUser />} />

        {/* Redirección de rutas no encontradas */}
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
