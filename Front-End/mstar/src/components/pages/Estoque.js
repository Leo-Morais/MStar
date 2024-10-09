import { useState, useEffect } from 'react'; 
import Select from '../form/Select'; // O componente Select que já criamos anteriormente

function Estoque() {
    const [estoques, setEstoques] = useState([]);
    const [mercadorias, setMercadorias] = useState([]); // Lista de mercadorias para o dropdown
    const [editEstoque, setEditEstoque] = useState(null);
    const [newEstoque, setNewEstoque] = useState({
        idMercadoria: '',
        quantidade: '',
        dataAtualizacao: new Date().toISOString().slice(0, 16), // Adiciona DataAtualizacao com a data atual
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

    // Função para buscar todos os estoques
    useEffect(() => {
        fetch('https://localhost:7116/api/v1/Estoque', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        })
        .then((resp) => resp.json())
        .then((data) => setEstoques(data))
        .catch((err) => console.log('Erro ao buscar estoques:', err));
    }, []);

    // Função para deletar um estoque com confirmação
    const handleDelete = (id) => {
        const confirmed = window.confirm("Tem certeza que deseja deletar este estoque?");
        if (confirmed) {
            fetch(`https://localhost:7116/api/v1/Estoque/${id}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
            })
            .then(() => {
                setEstoques(estoques.filter((estoque) => estoque.id !== id));
            })
            .catch((err) => console.log('Erro ao deletar estoque:', err));
        }
    };

    // Função para começar a editar um estoque
    const handleEdit = (estoque) => {
        setEditEstoque(estoque);
        setNewEstoque({
            idMercadoria: estoque.idMercadoria,
            quantidade: estoque.quantidade,
            dataAtualizacao: new Date().toISOString().slice(0, 16), // Define a nova data de atualização
        });
    };

    // Função para salvar a atualização do estoque
    const handleUpdate = (e) => {
        e.preventDefault();
        const updatedEstoque = {
            idMercadoria: newEstoque.idMercadoria,
            quantidade: newEstoque.quantidade,
            dataAtualizacao: new Date().toISOString().slice(0, 16), // Atualiza o campo DataAtualizacao
        };

        fetch(`https://localhost:7116/api/v1/Estoque/${editEstoque.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedEstoque),
        })
        .then((resp) => resp.json())
        .then((data) => {
            setEstoques(estoques.map((estoque) => estoque.id === editEstoque.id ? data : estoque));
            setEditEstoque(null);
        })
        .catch((err) => console.log('Erro ao atualizar estoque:', err));
    };

    // Função para alterar o estado de newEstoque
    const handleInputChange = (e) => {
        setNewEstoque({
            ...newEstoque,
            [e.target.name]: e.target.value
        });
    };

    return (
        <div>
            <h1>Estoque</h1>

            <ul>
                {estoques.map((estoque) => {
                    const mercadoria = mercadorias.find(m => m.id === estoque.idMercadoria);

                    return (
                        <li key={estoque.id}>
                            {editEstoque && editEstoque.id === estoque.id ? (
                                <form onSubmit={handleUpdate}>
                                    <Select
                                        text="Mercadoria"
                                        name="idMercadoria"
                                        options={mercadorias.map(mercadoria => ({ id: mercadoria.id, name: mercadoria.nome }))}
                                        handleOnChange={handleInputChange}
                                        value={newEstoque.idMercadoria}
                                    />
                                    <input
                                        type="number"
                                        name="quantidade"
                                        value={newEstoque.quantidade}
                                        onChange={handleInputChange}
                                        placeholder="Quantidade"
                                    />
                                    <input
                                        type="datetime-local"
                                        name="dataAtualizacao"
                                        value={newEstoque.dataAtualizacao}
                                        onChange={handleInputChange}
                                    />

                                    <button type="submit">Salvar</button>
                                    <button type="button" onClick={() => setEditEstoque(null)}>Cancelar</button>
                                </form>
                            ) : (
                                <>
                                    <p>Mercadoria: {mercadoria ? mercadoria.nome : 'Mercadoria não encontrada'}</p>
                                    <p>Quantidade: {estoque.quantidade}</p>
                                    <p>Data de Atualização: {new Date(estoque.dataAtualizacao).toLocaleString()}</p>
                                    <button onClick={() => handleEdit(estoque)}>Editar</button>
                                    <button onClick={() => handleDelete(estoque.id)}>Deletar</button>
                                </>
                            )}
                        </li>
                    );
                })}
            </ul>
        </div>
    );
}

export default Estoque;
