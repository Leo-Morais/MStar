import { useState, useEffect } from 'react';
import Select from '../form/Select'; // O componente Select que já criamos anteriormente

function Movimentacao() {
    const [movimentacoes, setMovimentacoes] = useState([]);
    const [mercadorias, setMercadorias] = useState([]); // Lista de mercadorias para o dropdown
    const [editMovimentacao, setEditMovimentacao] = useState(null);
    const [newMovimentacao, setNewMovimentacao] = useState({
        idMercadoria: '',
        quantidade: '',
        localMovimentacao: '',
        tipoMovimentacao: 'E' // Valor padrão, pode ser "E" ou "S"
    });

    // Função para buscar todas as mercadorias (para o dropdown)
    useEffect(() => {
        fetch('https://localhost:7116/api/v1/Mercadoria', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        })
        .then((resp) => resp.json())
        .then((data) => setMercadorias(data))
        .catch((err) => console.log('Erro ao buscar mercadorias:', err));
    }, []);

    // Função para buscar todas as movimentações
    useEffect(() => {
        fetch('https://localhost:7116/api/v1/Movimentacao', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        })
        .then((resp) => resp.json())
        .then((data) => setMovimentacoes(data))
        .catch((err) => console.log('Erro ao buscar movimentações:', err));
    }, []);

    // Função para deletar uma movimentação com confirmação
    const handleDelete = (id) => {
        const confirmed = window.confirm("Tem certeza que deseja deletar esta movimentação?");
        if (confirmed) {
            fetch(`https://localhost:7116/api/v1/Movimentacao/${id}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
            })
            .then(() => {
                setMovimentacoes(movimentacoes.filter((movimentacao) => movimentacao.id !== id));
            })
            .catch((err) => console.log('Erro ao deletar movimentação:', err));
        }
    };

    // Função para começar a editar uma movimentação
    const handleEdit = (movimentacao) => {
        setEditMovimentacao(movimentacao);
        setNewMovimentacao({
            idMercadoria: movimentacao.idMercadoria,
            quantidade: movimentacao.quantidade,
            localMovimentacao: movimentacao.localMovimentacao,
            tipoMovimentacao: movimentacao.tipoMovimentacao,
        });
    };

    // Função para salvar a atualização da movimentação
    const handleUpdate = (e) => {
        e.preventDefault();
        const updatedMovimentacao = {
            idMercadoria: newMovimentacao.idMercadoria,
            quantidade: newMovimentacao.quantidade,
            localMovimentacao: newMovimentacao.localMovimentacao,
            tipoMovimentacao: newMovimentacao.tipoMovimentacao,
        };

        fetch(`https://localhost:7116/api/v1/Movimentacao/${editMovimentacao.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedMovimentacao),
        })
        .then((resp) => resp.json())
        .then((data) => {
            setMovimentacoes(movimentacoes.map((movimentacao) => movimentacao.id === editMovimentacao.id ? data : movimentacao));
            setEditMovimentacao(null);
        })
        .catch((err) => console.log('Erro ao atualizar movimentação:', err));
    };

    // Função para alterar o estado de newMovimentacao
    const handleInputChange = (e) => {
        setNewMovimentacao({
            ...newMovimentacao,
            [e.target.name]: e.target.value
        });
    };

    return (
        <div>
            <h1>Movimentação</h1>

            <ul>
                {movimentacoes.map((movimentacao) => {
                    // Encontre a mercadoria correspondente ao ID
                    const mercadoria = mercadorias.find(m => m.id === movimentacao.idMercadoria);

                    return (
                        <li key={movimentacao.id}>
                            {editMovimentacao && editMovimentacao.id === movimentacao.id ? (
                                <form onSubmit={handleUpdate}>
                                    <Select
                                        text="Mercadoria"
                                        name="idMercadoria"
                                        options={mercadorias.map(mercadoria => ({ id: mercadoria.id, name: mercadoria.nome }))} // Acessando o nome da mercadoria
                                        handleOnChange={handleInputChange}
                                        value={newMovimentacao.idMercadoria}
                                    />
                                    <input
                                        type="number"
                                        name="quantidade"
                                        value={newMovimentacao.quantidade}
                                        onChange={handleInputChange}
                                        placeholder="Quantidade"
                                    />
                                    <input
                                        type="text"
                                        name="localMovimentacao"
                                        value={newMovimentacao.localMovimentacao}
                                        onChange={handleInputChange}
                                        placeholder="Local de Movimentação"
                                    />
                                    <select
                                        name="tipoMovimentacao"
                                        value={newMovimentacao.tipoMovimentacao}
                                        onChange={handleInputChange}
                                    >
                                        <option value="E">Entrada</option>
                                        <option value="S">Saída</option>
                                    </select>

                                    <button type="submit">Salvar</button>
                                    <button type="button" onClick={() => setEditMovimentacao(null)}>Cancelar</button>
                                </form>
                            ) : (
                                <>
                                    {/* Exibir o nome da mercadoria em vez do ID */}
                                    <p>Mercadoria: {mercadoria ? mercadoria.nome : 'Mercadoria não encontrada'}</p>
                                    <p>Quantidade: {movimentacao.quantidade}</p>
                                    <p>Local de Movimentação: {movimentacao.localMovimentacao}</p>
                                    <p>Tipo de Movimentação: {movimentacao.tipoMovimentacao === 'E' ? 'Entrada' : 'Saída'}</p>
                                    <button onClick={() => handleEdit(movimentacao)}>Editar</button>
                                    <button onClick={() => handleDelete(movimentacao.id)}>Deletar</button>
                                </>
                            )}
                        </li>
                    );
                })}
            </ul>
        </div>
    );
}

export default Movimentacao;
