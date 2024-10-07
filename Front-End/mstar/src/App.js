import './App.css';
import {BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import Home from './components/pages/Home';
import TipoMercadoria from './components/pages/TipoMercadoria';
import Mercadoria from './components/pages/Mercadoria';
import NewProject from './components/pages/NewProject';
import Estoque from './components/pages/Estoque';

import Container from './components/layout/Container';
import Navbar from './components/layout/Navbar';
import Footer from './components/layout/Footer';
import Movimentacao from './components/pages/Movimentacao';


function App() {

  return (
    <Router>
      <Navbar />
      <Container customClass="min-height">
        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/mercadoria' element={<Mercadoria />} />
          <Route path='/tipoMercadoria' element={<TipoMercadoria />} />
          <Route path='/newproject' element={<NewProject />} />
          <Route path='/estoque' element={<Estoque />} />
          <Route path='/movimentacao' element={<Movimentacao />} />
        </Routes>
      </Container>
      <Footer />    
    </Router>
  );
}

export default App;