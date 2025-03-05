import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import LoginSignup from './Pages/LoginSignup';
import PanelDeAdministrador from './Pages/PanelDeAdministrador';

function App() {
  return (
    <div>
      <BrowserRouter>

        <Routes>
          <Route path='/' element={<LoginSignup />} />
          <Route path='/panelAdministrador' element={<PanelDeAdministrador/>} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
