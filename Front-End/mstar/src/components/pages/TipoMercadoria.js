import { useState, useEffect } from 'react';

function TipoMercadoria() {
    const [tipos, setTipos] = useState([]); // Para armazenar a lista de tipos de mercadoria
    const [editTipo, setEditTipo] = useState(null); // Para armazenar o tipo que está sendo editado
    const [newTipo, setNewTipo] = useState(''); // Para o valor atualizado do tipo

    // Função para buscar os tipos de mercadoria
    useEffect(() => {
        fetch("https://localhost:7116/api/v1/TipoMercadoria", {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
            },
        })
        .then(resp => resp.json())
        .then(data => setTipos(data))
        .catch(err => console.log('Erro ao buscar tipos de mercadoria:', err));
    }, []);

    // Função para deletar um tipo de mercadoria
    const handleDelete = (id) => {
        const confirmDelete = window.confirm("Tem certeza que deseja deletar este tipo de mercadoria?");
        if (!confirmDelete) return;

        fetch(`https://localhost:7116/api/v1/TipoMercadoria/${id}`, {
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json',
            },
        })
        .then(() => {
            setTipos(tipos.filter(tipo => tipo.id !== id));
        })
        .catch(err => console.log('Erro ao deletar tipo de mercadoria:', err));
    };

    // Função para começar a editar um tipo de mercadoria
    const handleEdit = (tipo) => {
        setEditTipo(tipo);
        setNewTipo(tipo.tipo);
    };

    // Função para salvar a atualização do tipo de mercadoria
    const handleUpdate = (e) => {
        e.preventDefault();
        if (!newTipo) {
            alert("O tipo de mercadoria não pode estar vazio.");
            return;
        }
        
        const updatedTipo = { tipo: newTipo };

        fetch(`https://localhost:7116/api/v1/TipoMercadoria/${editTipo.id}`, {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedTipo),
        })
        .then(resp => resp.json())
        .then(data => {
            // Atualizar o tipo na lista
            setTipos(tipos.map(tipo => tipo.id === editTipo.id ? data : tipo));
            setEditTipo(null);
            setNewTipo(''); // Limpa o campo após atualização
        })
        .catch(err => console.log('Erro ao atualizar tipo de mercadoria:', err));
    };

    return (
        <div>
            <h1>Tipo de Mercadoria</h1>

            <ul>
                {tipos.map(tipo => (
                    <li key={tipo.id}>
                        {editTipo && editTipo.id === tipo.id ? (
                            <form onSubmit={handleUpdate}>
                                <input 
                                    type="text" 
                                    value={newTipo}
                                    onChange={(e) => setNewTipo(e.target.value)} 
                                    placeholder="Digite o novo tipo"
                                    required
                                />
                                <button type="submit">Salvar</button>
                                <button type="button" onClick={() => setEditTipo(null)}>Cancelar</button>
                            </form>
                        ) : (
                            <>
                                {tipo.tipo}
                                <button onClick={() => handleEdit(tipo)}>Editar</button>
                                <button onClick={() => handleDelete(tipo.id)}>Deletar</button>
                            </>
                        )}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default TipoMercadoria;
