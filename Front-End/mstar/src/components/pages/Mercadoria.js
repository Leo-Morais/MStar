import { useState, useEffect } from 'react';
import Select from '../form/Select'; // O componente Select que já criamos anteriormente

function Mercadoria() {
    const [mercadorias, setMercadorias] = useState([]);
    const [tipos, setTipos] = useState([]); // Lista de tipos de mercadoria para o dropdown
    const [editMercadoria, setEditMercadoria] = useState(null);
    const [newMercadoria, setNewMercadoria] = useState({
        nome: '',
        fabricante: '',
        descricao: '',
        tipoMercadoriaId: ''
    });

    // Função para buscar os tipos de mercadoria (para o dropdown)
    useEffect(() => {
        fetch('https://localhost:7116/api/v1/TipoMercadoria', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        })
        .then((resp) => resp.json())
        .then((data) => setTipos(data))
        .catch((err) => console.log('Erro ao buscar tipos de mercadoria:', err));
    }, []);

    // Função para buscar todas as mercadorias com o nome do tipo de mercadoria
    useEffect(() => {
        fetch('https://localhost:7116/api/v1/Mercadoria', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        })
        .then((resp) => resp.json())
        .then((data) => {
            // Mapear cada mercadoria para incluir o nome do tipo associado
            const mercadoriasComTipo = data.map(mercadoria => {
                const tipo = tipos.find(t => t.id === mercadoria.tipoMercadoriaId);
                return { ...mercadoria, tipoMercadoriaNome: tipo ? tipo.tipo : 'Tipo não encontrado' };
            });
            setMercadorias(mercadoriasComTipo);
        })
        .catch((err) => console.log('Erro ao buscar mercadorias:', err));
    }, [tipos]); // Atualizar quando a lista de tipos for carregada

    // Função para deletar uma mercadoria com confirmação
    const handleDelete = (id) => {
        const confirmed = window.confirm("Tem certeza que deseja deletar esta mercadoria?");
        if (confirmed) {
            fetch(`https://localhost:7116/api/v1/Mercadoria/${id}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
            })
            .then(() => {
                setMercadorias(mercadorias.filter((mercadoria) => mercadoria.id !== id));
            })
            .catch((err) => console.log('Erro ao deletar mercadoria:', err));
        }
    };

    // Função para começar a editar uma mercadoria
    const handleEdit = (mercadoria) => {
        setEditMercadoria(mercadoria);
        setNewMercadoria({
            nome: mercadoria.nome,
            fabricante: mercadoria.fabricante,
            descricao: mercadoria.descricao,
            tipoMercadoriaId: mercadoria.tipoMercadoriaId, // Salvar o id do tipo
        });
    };

    // Função para salvar a atualização da mercadoria
    const handleUpdate = (e) => {
        e.preventDefault();
        const updatedMercadoria = {
            nome: newMercadoria.nome,
            fabricante: newMercadoria.fabricante,
            descricao: newMercadoria.descricao,
            tipoMercadoriaId: newMercadoria.tipoMercadoriaId,
        };
    
        fetch(`https://localhost:7116/api/v1/Mercadoria/${editMercadoria.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedMercadoria),
        })
        .then((resp) => {
            if (!resp.ok) {
                throw new Error('Erro ao atualizar mercadoria');
            }
            return resp.json();
        })
        .then((data) => {
            const updatedMercadorias = mercadorias.map((mercadoria) =>
                mercadoria.id === editMercadoria.id ? {
                    ...mercadoria,
                    nome: data.nome,
                    fabricante: data.fabricante,
                    descricao: data.descricao,
                    tipoMercadoriaId: data.tipoMercadoriaId,
                    tipoMercadoriaNome: tipos.find(t => t.id === data.tipoMercadoriaId)?.tipo || 'Tipo não encontrado',
                } : mercadoria
            );
            setMercadorias(updatedMercadorias);
            setEditMercadoria(null);
        })
        .catch((err) => console.log('Erro ao atualizar mercadoria:', err));
    };

    // Função para alterar o estado de newMercadoria
    const handleInputChange = (e) => {
        setNewMercadoria({
            ...newMercadoria,
            [e.target.name]: e.target.value
        });
    };

    return (
        <div>
            <h1>Mercadoria</h1>

            <ul>
                {mercadorias.map((mercadoria) => (
                    <li key={mercadoria.id}>
                        {editMercadoria && editMercadoria.id === mercadoria.id ? (
                            <form onSubmit={handleUpdate}>
                                <input 
                                    type="text" 
                                    name="nome"
                                    value={newMercadoria.nome}
                                    onChange={handleInputChange} 
                                    placeholder="Nome"
                                />
                                <input 
                                    type="text" 
                                    name="fabricante"
                                    value={newMercadoria.fabricante}
                                    onChange={handleInputChange} 
                                    placeholder="Fabricante"
                                />
                                <input 
                                    type="text" 
                                    name="descricao"
                                    value={newMercadoria.descricao}
                                    onChange={handleInputChange} 
                                    placeholder="Descrição"
                                />
                                
                                {/* Select dropdown para TipoMercadoriaId */}
                                <Select
                                    text="Tipo de Mercadoria"
                                    name="tipoMercadoriaId"
                                    options={tipos.map(tipo => ({ id: tipo.id, name: tipo.tipo }))}
                                    handleOnChange={handleInputChange} 
                                    value={newMercadoria.tipoMercadoriaId}
                                />

                                <button type="submit">Salvar</button>
                                <button type="button" onClick={() => setEditMercadoria(null)}>Cancelar</button>
                            </form>
                        ) : (
                            <>
                                <p>Nome: {mercadoria.nome}</p>
                                <p>Fabricante: {mercadoria.fabricante}</p>
                                <p>Descrição: {mercadoria.descricao}</p>
                                <p>Tipo de Mercadoria: {mercadoria.tipoMercadoriaNome}</p> {/* Agora exibe o nome do tipo */} 
                                <button onClick={() => handleEdit(mercadoria)}>Editar</button>
                                <button onClick={() => handleDelete(mercadoria.id)}>Deletar</button>
                            </>
                        )}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default Mercadoria;
