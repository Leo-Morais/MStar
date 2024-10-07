// src/pages/Home.js
import React from 'react';

const Home = () => {
    return (
        <div style={styles.container}>
            <h1>Bem-vindo à Minha Aplicação!</h1>
            <p>Use a barra de navegação para explorar os serviços disponíveis.</p>
        </div>
    );
};

const styles = {
    container: {
        textAlign: 'center',
        padding: '2rem'
    }
};

export default Home;
