import './App.css';
import {BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import Home from './components/pages/Home';
import TipoMercadoria from './components/pages/TipoMercadoria';
import Mercadoria from './components/pages/Mercadoria';
import TipoMercadoriaCadastro from './components/pages/TipoMercadoriaCadastro';
import Estoque from './components/pages/Estoque';

import Container from './components/layout/Container';
import Navbar from './components/layout/Navbar';
import Footer from './components/layout/Footer';
import Movimentacao from './components/pages/Movimentacao';
import MercadoriaCadastro from './components/pages/MercadoriaCadastro';
import MovimentacaoCadastro from './components/pages/MovimentacaoCadastro';
import EstoqueCadastro from './components/pages/EstoqueCadastro';



function App() {

  return (
    <Router>
      <Navbar />
      <Container customClass="min-height">
        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/mercadoria' element={<Mercadoria />} />
          <Route path='/tipoMercadoria' element={<TipoMercadoria />} />
          <Route path='/estoque' element={<Estoque />} />
          <Route path='/movimentacao' element={<Movimentacao />} />
          <Route path='/tipoMercadoriaCadastro' element={<TipoMercadoriaCadastro />} />
          <Route path='/mercadoriaCadastro' element={<MercadoriaCadastro />} />
          <Route path='/movimentacaoCadastro' element={<MovimentacaoCadastro />} />
          <Route path='/estoqueCadastro' element={<EstoqueCadastro />} />
        </Routes>
      </Container>
      <Footer />    
    </Router>
  );
}

export default App;