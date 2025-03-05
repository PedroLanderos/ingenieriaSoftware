import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import LoginSignup from './Pages/LoginSignup';
import PanelDeAdministrador from './Pages/PanelDeAdministrador';
import AddUser from './Pages/AddUser';

function App() {
  return (
    <div>
      <BrowserRouter>

        <Routes>
          <Route path='/' element={<LoginSignup />} />
          <Route path='/panelAdministrador' element={<PanelDeAdministrador/>} />
          <Route path='/login' element={<LoginSignup/>} />
          <Route path='/AddUser' element={<AddUser/>} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
