import { useState, useEffect } from 'react';

function TipoMercadoria() {
    const [tipos, setTipos] = useState([]);
    const [editTipo, setEditTipo] = useState(null);
    const [newTipo, setNewTipo] = useState('');

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

    const handleEdit = (tipo) => {
        setEditTipo(tipo);
        setNewTipo(tipo.tipo);
    };

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
            setTipos(tipos.map(tipo => tipo.id === editTipo.id ? data : tipo));
            setEditTipo(null);
            setNewTipo('');
        })
        .catch(err => console.log('Erro ao atualizar tipo de mercadoria:', err));
    };

    return (
        <div>
            <h1>Tipos de Mercadoria</h1>

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
                            <div>
                                <span>{tipo.tipo}</span>
                                <div>
                                    <button onClick={() => handleEdit(tipo)}>Editar</button>
                                    <button onClick={() => handleDelete(tipo.id)}>Deletar</button>
                                </div>
                            </div>
                        )}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default TipoMercadoria;
