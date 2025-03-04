import React, { useState, useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom'; // Para redirigir si no es admin
import './CSS/PanelDeAdministrador.css';
import AddUser from './AddUser';
import ManageUsers from './ManageUsers';
import { AuthContext } from '../Context/AuthContext'; // Contexto de autenticación

const PanelDeAdministrador = () => {
  const [activeTab, setActiveTab] = useState('');
  const [activeSubmenu, setActiveSubmenu] = useState('');
  const { auth } = useContext(AuthContext); // Obtener la información del usuario autenticado
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true); // Estado para controlar la carga

  useEffect(() => {
    // Verifica si el rol está disponible
    if (auth?.user?.role) {
      if (auth.user.role !== 'Admin') {
        alert('No tienes permiso para acceder a este panel.');
        navigate('/'); // Redirigir si no es Admin
      }
      setLoading(false); // Deja de cargar si el rol está definido
    }
  }, [auth, navigate]);

  // Mientras se verifica el rol, muestra un mensaje de carga
  if (loading) {
    return <div>Cargando...</div>;
  }

  // Si el usuario no es Admin (capa de protección adicional)
  if (auth?.user?.role !== 'Admin') {
    return null;
  }

  const toggleSubmenu = (menu) => {
    setActiveSubmenu((prev) => (prev === menu ? '' : menu));
  };

  const renderContent = () => {
    switch (activeTab) {
      case 'agregarUsuario':
        return <AddUser />;
      case 'administrarUsuarios':
        return <ManageUsers />;
      default:
        return <div>Selecciona una sección del panel.</div>;
    }
  };

  return (
    <div className="admin-panel">
      <div className="sidebar">
        <h1>Panel de Administrador</h1>
        <ul>
          <li
            onClick={() => toggleSubmenu('usuarios')}
            className={activeSubmenu === 'usuarios' ? 'active' : ''}
          >
            Usuarios
            {activeSubmenu === 'usuarios' && (
              <ul className="submenu">
                <li onClick={() => setActiveTab('agregarUsuario')}>Agregar Usuario</li>
                <li onClick={() => setActiveTab('administrarUsuarios')}>Administrar Usuarios</li>
              </ul>
            )}
          </li>
        </ul>
      </div>
      <div className="content">{renderContent()}</div>
    </div>
  );
};

export default PanelDeAdministrador;
