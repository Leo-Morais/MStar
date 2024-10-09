import { useState, useEffect } from 'react';
import { Bar } from 'react-chartjs-2';
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from 'chart.js';

// Registrar os componentes necessários para o Chart.js
ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

function MovimentacaoGrafico() {
    const [entradas, setEntradas] = useState(0);
    const [saidas, setSaidas] = useState(0);

    // Função para buscar todas as movimentações e calcular entradas e saídas
    useEffect(() => {
        fetch('https://localhost:7116/api/v1/Movimentacao', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        })
        .then((resp) => resp.json())
        .then((data) => {
            const countEntradas = data.filter(mov => mov.tipoMovimentacao === 'E').length;
            const countSaidas = data.filter(mov => mov.tipoMovimentacao === 'S').length;

            setEntradas(countEntradas);
            setSaidas(countSaidas);
        })
        .catch((err) => console.log('Erro ao buscar movimentações:', err));
    }, []);

    // Dados para o gráfico de barras
    const data = {
        labels: ['Entradas', 'Saídas'],
        datasets: [
            {
                label: 'Quantidade de Movimentações',
                data: [entradas, saidas],
                backgroundColor: ['rgba(75, 192, 192, 0.6)', 'rgba(255, 99, 132, 0.6)'],
                borderColor: ['rgba(75, 192, 192, 1)', 'rgba(255, 99, 132, 1)'],
                borderWidth: 1,
            },
        ],
    };

    // Opções de configuração do gráfico
    const options = {
        responsive: true,
        plugins: {
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Movimentações de Entrada e Saída',
            },
        },
    };

    return (
        <div>
            <h2>Gráfico de Movimentações</h2>
            <Bar data={data} options={options} />
        </div>
    );
}

export default MovimentacaoGrafico;
